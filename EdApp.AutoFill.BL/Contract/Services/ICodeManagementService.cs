namespace EdApp.AutoFill.BL.Contract.Services
{
    public interface ICodeManagementService
    {
        void UpdateText(string fileFullPath, string pattern, string replacement);
    }
}