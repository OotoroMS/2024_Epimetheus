using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class JsonLoader
{
    public class Character{
        public Jdata_CharacterParams LoadCharacterParams(string jsonString)
        {
            try
            {
                // デシリアライズ
                Jdata_CharacterParams characterParams = JsonConvert.DeserializeObject<Jdata_CharacterParams>(jsonString);

                return characterParams;
            }
            catch (System.Exception ex)
            {
                UnityEngine.Debug.LogError($"デシリアライズ中にエラーが発生しました: {ex.Message}");
                return null;
            }
        }

        [System.Serializable]
        public class Jdata_CharacterParams
        {
            public int[][] InitialParams { get; set; }
            public MiddleParam[] MiddleParams { get; set; }
            public List<List<GetSkills>> GetSkill { get; set; }
            public int[][] InitialEquip { get; set; }

            [System.Serializable]
            public class MiddleParam
            {
                public int Level { get; set; }
                public int[][] Params { get; set; }
            }

            [System.Serializable]
            public class GetSkills
            {
                public int Level { get; set; }
                public int[] Skill { get; set; }
            }
        }
    }

    public class Enemy{
        public Jdata_EnemyParams LoadEnemyParams(string jsonString, int chara_id)
        {
            try
            {
                // デシリアライズ
                Jdata_EnemyParams enemyParams = JsonConvert.DeserializeObject<Jdata_EnemyParams>(jsonString);

                return enemyParams;
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"デシリアライズ中にエラーが発生しました: {ex.Message}");
                return null;
            }
        }

        [System.Serializable]
        public class Jdata_EnemyParams
        {
            public EnemParams[] NormalEnemy { get; set; }

            [System.Serializable]
            public class EnemParams{
                public string Name { get; set; }
                public int Level { get; set; }
                public int Exp { get; set; }
                public int[] Params { get; set; }
                public int[] Skill { get; set; }
                public int[] Item { get; set; }
            }
        }
    }
}