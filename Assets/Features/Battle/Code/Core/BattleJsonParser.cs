using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public static class BattleJsonParser
{
    public static CharacterParamsData ParseCharacterParams(string jsonString)
    {
        return JsonConvert.DeserializeObject<CharacterParamsData>(jsonString);
    }

    public static EnemyParamsData ParseEnemyParams(string jsonString)
    {
        return JsonConvert.DeserializeObject<EnemyParamsData>(jsonString);
    }
}

[Serializable]
public class CharacterParamsData
{
    public int[][] InitialParams { get; set; }
    public MiddleParam[] MiddleParams { get; set; }
    public List<List<GetSkills>> GetSkill { get; set; }
    public int[][] InitialEquip { get; set; }

    [Serializable]
    public class MiddleParam
    {
        public int Level { get; set; }
        public int[][] Params { get; set; }
    }

    [Serializable]
    public class GetSkills
    {
        public int Level { get; set; }
        public int[] Skill { get; set; }
    }
}

[Serializable]
public class EnemyParamsData
{
    public EnemyParam[] NormalEnemy { get; set; }

    [Serializable]
    public class EnemyParam
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Exp { get; set; }
        public int[] Params { get; set; }
        public int[] Skill { get; set; }
        public int[] Item { get; set; }
    }
}
