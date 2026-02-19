using System.IO;
using UnityEngine;

public static class JsonFileLoader
{
    public static string LoadText(string assetRelativePath)
    {
        if (string.IsNullOrWhiteSpace(assetRelativePath))
        {
            Debug.LogError("読み込み対象のパスが空です。");
            return null;
        }

        string fullPath = Path.Combine(Directory.GetParent(Application.dataPath).FullName, assetRelativePath);
        if (!File.Exists(fullPath))
        {
            Debug.LogError($"JSONファイルが見つかりません: {fullPath}");
            return null;
        }

        return File.ReadAllText(fullPath);
    }
}
