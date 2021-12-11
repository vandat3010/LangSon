namespace Namek.Library.Startup
{
    public interface IStartupTask
    {
        /// <summary>
        ///     The priority of task. Lower means earlier in pipeline
        /// </summary>
        int Priority { get; }

        /// <summary>
        ///     Runs a startup task
        /// </summary>
        void Run();
    }
}