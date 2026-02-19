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
        string jsonText = FileTextLoader.LoadText(fullPath);
        if (jsonText == null)
        {
            Debug.LogError($"JSONファイルが見つかりません: {fullPath}");
        }

        return jsonText;
    }
}
