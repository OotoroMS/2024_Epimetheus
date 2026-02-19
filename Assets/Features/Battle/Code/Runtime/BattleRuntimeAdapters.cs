using UnityEngine;

public sealed class UnityBattleLogger : IBattleLogger
{
    public void Log(string message) => Debug.Log(message);
    public void LogWarning(string message) => Debug.LogWarning(message);
    public void LogError(string message) => Debug.LogError(message);
}

public static class BattleRuntimeDataLoader
{
    public static JsonLoader.Character.Jdata_CharacterParams LoadCharacterParams(string resourcePath, IBattleLogger logger)
    {
        TextAsset jsonText = Resources.Load<TextAsset>(resourcePath);
        if (jsonText == null)
        {
            logger?.LogError($"CharacterParams が見つかりません: {resourcePath}");
            return null;
        }

        try
        {
            var loader = new JsonLoader.Character();
            return loader.LoadCharacterParams(jsonText.text);
        }
        catch (System.Exception ex)
        {
            logger?.LogError($"CharacterParams の読み込みに失敗しました: {ex.Message}");
            return null;
        }
    }

    public static JsonLoader.Enemy.Jdata_EnemyParams LoadEnemyParams(string resourcePath, int charaId, IBattleLogger logger)
    {
        TextAsset jsonText = Resources.Load<TextAsset>(resourcePath);
        if (jsonText == null)
        {
            logger?.LogError($"EnemyParams が見つかりません: {resourcePath}");
            return null;
        }

        try
        {
            var loader = new JsonLoader.Enemy();
            return loader.LoadEnemyParams(jsonText.text, charaId);
        }
        catch (System.Exception ex)
        {
            logger?.LogError($"EnemyParams の読み込みに失敗しました: {ex.Message}");
            return null;
        }
    }
}
