namespace Namek.LogServices.Installation
{
    public interface IInstallationService
    {
        void InstallData(string defaultUserEmail, bool installSampleData = true);
    }
}