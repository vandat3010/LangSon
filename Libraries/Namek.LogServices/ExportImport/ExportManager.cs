using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CDKTTCTN.Core.Config;
using CDKTTCTN.Core.Entity;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace CDKTTCTN.Services.ExportImport
{
    public class ExportManager :IExportManager
    {

        /// <summary>
        /// Export entities T to XLSX
        /// </summary>
        /// <param name="entities">Products</param>
        public virtual byte[] ExportToXlsx<T>(IEnumerable<T> entities)
        {
            var propertyFields = typeof(T).GetProperties();
            int i = 0;
            List<PropertyByName<T>> propertiesByNames = new List<PropertyByName<T>> { };

            foreach (var property in propertyFields)
            {
                var propertyName = property.Name;
                //var valueObj = property.GetValue(entities);                
                //set the property                
                var propertiesByName = new PropertyByName<T>(propertyName, x => x.GetType().GetProperty(propertyName));
                propertiesByNames.Add(propertiesByName);
                i++;
            }
            var productList = entities.ToList();

            return ExportToXlsx(propertiesByNames.ToArray(), productList);
        }

        public virtual byte[] ExportStudyResultsToXlsx(IEnumerable<Study_result> products)
        {

            var productList = products.ToList();

            var propertiesByName = new[]
            {
                new PropertyByName<Study_result>(StudyResultCaption.subjects),
                new PropertyByName<Study_result>(StudyResultCaption.semester),
                new PropertyByName<Study_result>(StudyResultCaption.student_code),
                new PropertyByName<Study_result>(StudyResultCaption.student_firstname),
                new PropertyByName<Study_result>(StudyResultCaption.student_lastname),
                new PropertyByName<Study_result>(StudyResultCaption.date_of_birth),
                new PropertyByName<Study_result>(StudyResultCaption.class_code),
                new PropertyByName<Study_result>(StudyResultCaption.mark),
                new PropertyByName<Study_result>(StudyResultCaption.note),
            };
            return ExportToXlsx(propertiesByName, productList);
        }
        
        /// <summary>
        /// Export objects to XLSX
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="properties">Class access to the object through its properties</param>
        /// <param name="itemsToExport">The objects to export</param>
        /// <returns></returns>
        protected virtual byte[] ExportToXlsx<T>(PropertyByName<T>[] properties, IEnumerable<T> itemsToExport)
        {
            using (var stream = new MemoryStream())
            {
                // ok, we can run the real code of the sample now
                using (var xlPackage = new ExcelPackage(stream))
                {
                    // uncomment this line if you want the XML written out to the outputDir
                    //xlPackage.DebugMode = true; 

                    // get handles to the worksheets
                    var worksheet = xlPackage.Workbook.Worksheets.Add(typeof(T).Name);
                    var fWorksheet = xlPackage.Workbook.Worksheets.Add("DataForFilters");
                    fWorksheet.Hidden = eWorkSheetHidden.VeryHidden;

                    //create Headers and format them 
                    var manager = new PropertyManager<T>(properties.Where(p => !p.Ignore));
                    manager.WriteCaption(worksheet, SetCaptionStyle);

                    var row = 2;
                    foreach (var items in itemsToExport)
                    {
                        manager.CurrentObject = items;
                        manager.WriteToXlsx(worksheet, row++, false, fWorksheet: fWorksheet);
                    }

                    xlPackage.Save();
                }
                return stream.ToArray();
            }
        }

        protected virtual void SetCaptionStyle(ExcelStyle style)
        {
            style.Fill.PatternType = ExcelFillStyle.Solid;
            style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(184, 204, 228));
            style.Font.Bold = true;
        }
    }
}
