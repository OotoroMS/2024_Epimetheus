using UnityEngine;

public class JsonLoader
{
    public class Character
    {
        public CharacterParamsData LoadCharacterParams(string jsonString)
        {
            try
            {
                return BattleJsonParser.ParseCharacterParams(jsonString);
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"デシリアライズ中にエラーが発生しました: {ex.Message}");
                return null;
            }
        }
    }

    public class Enemy
    {
        public EnemyParamsData LoadEnemyParams(string jsonString, int chara_id)
        {
            try
            {
                return BattleJsonParser.ParseEnemyParams(jsonString);
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"デシリアライズ中にエラーが発生しました: {ex.Message}");
                return null;
            }
        }
    }
}
