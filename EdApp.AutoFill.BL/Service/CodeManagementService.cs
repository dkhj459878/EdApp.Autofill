using System;
using System.IO;
using EdApp.AutoFill.BL.Contract.Services;

namespace EdApp.AutoFill.BL.Service
{
    public class CodeManagementService : ICodeManagementService
    {
        public void UpdateText(string fileFullPath, string pattern, string replacement)
        {
            if (!File.Exists(fileFullPath))
            {
                var message = $"File with name and full path {fileFullPath} does not exist.";
                throw new ArgumentOutOfRangeException(nameof(fileFullPath), message);
            }

            var initialText = File.ReadAllText(fileFullPath);


        }
    }
}