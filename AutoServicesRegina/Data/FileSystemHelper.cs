using System;

namespace AutoServicesRegina.Data;

public class FileSystemHelper
{
    public static string GetDatabasePath(string databaseName)
    {
        var rootPath = GetRootPath();
        return Path.Combine(rootPath, $"db{Path.DirectorySeparatorChar}{databaseName}");
    }

    public static string GetDatabasePath(string rootPath, string databaseName)
    {
        if(string.IsNullOrEmpty(rootPath))
        {
            rootPath = GetRootPath();
        }

        return Path.Combine(rootPath, $"db{Path.DirectorySeparatorChar}{databaseName}");
    }

    private static string GetRootPath()
    {
        if(AppContext.BaseDirectory.ToLowerInvariant().Contains("\\bin\\debug") ||
            AppContext.BaseDirectory.ToLowerInvariant().Contains("\\bin\\release"))
        {
            var baseDirInfo = new DirectoryInfo(AppContext.BaseDirectory);
       
            return baseDirInfo.Parent?.Parent?.FullName;
        }
        else 
        {
            return AppContext.BaseDirectory;
        }
    }
}



