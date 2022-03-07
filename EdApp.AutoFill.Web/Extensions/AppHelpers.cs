using System;

namespace EdApp.AutoFill.Web.Extensions
{
    public static class AppHelpers
    {
        public static string ProjectPath
        {
            get
            {
                string rootPath = AppContext.BaseDirectory;
                const string bin = "bin";
                if (rootPath.Contains(bin))
                {
                    rootPath = rootPath[..rootPath.IndexOf(bin, StringComparison.Ordinal)];
                }

                return rootPath;
            }
        }
    }
}