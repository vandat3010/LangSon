using System;
using System.Data;
using System.Reflection;
using System.Reflection.Emit;

namespace Namek.Core
{
    public class DynamicBuilder<T>
    {
        private static readonly MethodInfo getValueMethod = typeof(IDataRecord).GetMethod("get_Item",
            new[] {typeof(int)});

        private static readonly MethodInfo isDBNullMethod = typeof(IDataRecord).GetMethod("IsDBNull",
            new[] {typeof(int)});

        private Load handler;

        private DynamicBuilder() { }

        public T Build(IDataRecord dataRecord)
        {
            return handler(dataRecord);
        }

        public static DynamicBuilder<T> CreateBuilder(IDataRecord dataRecord)
        {
            var dynamicBuilder = new DynamicBuilder<T>();

            var method = new DynamicMethod("DynamicCreate", typeof(T), new[] {typeof(IDataRecord)}, typeof(T), true);
            var generator = method.GetILGenerator();

            var result = generator.DeclareLocal(typeof(T));
            generator.Emit(OpCodes.Newobj, typeof(T).GetConstructor(Type.EmptyTypes));
            generator.Emit(OpCodes.Stloc, result);

            for (var i = 0; i < dataRecord.FieldCount; i++)
            {
                var propertyInfo = typeof(T).GetProperty(dataRecord.GetName(i));
                var endIfLabel = generator.DefineLabel();

                if (propertyInfo != null && propertyInfo.GetSetMethod() != null)
                {
                    generator.Emit(OpCodes.Ldarg_0);
                    generator.Emit(OpCodes.Ldc_I4, i);
                    generator.Emit(OpCodes.Callvirt, isDBNullMethod);
                    generator.Emit(OpCodes.Brtrue, endIfLabel);

                    generator.Emit(OpCodes.Ldloc, result);
                    generator.Emit(OpCodes.Ldarg_0);
                    generator.Emit(OpCodes.Ldc_I4, i);
                    generator.Emit(OpCodes.Callvirt, getValueMethod);
                    generator.Emit(OpCodes.Unbox_Any, dataRecord.GetFieldType(i));
                    generator.Emit(OpCodes.Callvirt, propertyInfo.GetSetMethod());

                    generator.MarkLabel(endIfLabel);
                }
            }

            generator.Emit(OpCodes.Ldloc, result);
            generator.Emit(OpCodes.Ret);

            dynamicBuilder.handler = (Load) method.CreateDelegate(typeof(Load));
            return dynamicBuilder;
        }

        #region Nested type: Load

        private delegate T Load(IDataRecord dataRecord);

        #endregion
    }
}