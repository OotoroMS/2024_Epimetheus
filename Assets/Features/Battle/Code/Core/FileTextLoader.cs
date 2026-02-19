using System.IO;

public static class FileTextLoader
{
    public static string LoadText(string fullPath)
    {
        if (string.IsNullOrWhiteSpace(fullPath))
        {
            return null;
        }

        return File.Exists(fullPath) ? File.ReadAllText(fullPath) : null;
    }
}
