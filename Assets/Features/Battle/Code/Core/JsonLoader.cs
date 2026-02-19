using System.Collections.Generic;
using Newtonsoft.Json;

public class JsonLoader
{
    public class Character{
        public Jdata_CharacterParams LoadCharacterParams(string jsonString)
        {
            return JsonConvert.DeserializeObject<Jdata_CharacterParams>(jsonString);
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
            return JsonConvert.DeserializeObject<Jdata_EnemyParams>(jsonString);
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
