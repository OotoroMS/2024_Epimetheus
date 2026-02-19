using System.Collections.Generic;

public static class Constants
{
    // キャラクター関連の定数
    public static class Character
    {
        public static readonly string[] Chrctr_Names = { "エルピス", "ラトアス", "キッソス", "パノス" };

        public static readonly string[] Chrctr_Jobs = { "勇者志望", "元兵士", "一人前魔法使い", "王宮神官" };

        public static readonly Dictionary<string, int> Chrctr_NameID = new Dictionary<string, int>
        {
            { "エルピス", 0 },
            { "ラトアス", 1 },
            { "キッソス", 2 },
            { "パノス", 3 }
        };

        public static readonly Dictionary<string, int> Chrctr_JobID = new Dictionary<string, int>
        {
            { "勇者志望", 0 },
            { "元兵士", 1 },
            { "一人前魔法使い", 2 },
            { "王宮神官", 3 }
        };
    }

    // パラメータ関連の定数
    public static class Parameters
    {
        public static readonly string[] Parameter_Names = { "HP", "MP", "At", "De", "MA", "MD", "Sp", "Ft" };
        public static readonly Dictionary<string, int> Parameter_ID = new Dictionary<string, int>
        {
            { "HP", 0 },
            { "MP", 1 },
            { "At", 2 },
            { "De", 3 },
            { "MA", 4 },
            { "MD", 5 },
            { "Sp", 6 },
            { "Ft", 7 }
        };
    }

    // 装備関連の定数
    public static class Equipment
    {
        public static readonly string[] Equipment_Names = { "Weapon", "Sheild", "Armor", "Accessory" };
        public static readonly Dictionary<string, int> Equipment_ID = new Dictionary<string, int>
        {
            { "Weapon", 0 },
            { "Sheild", 1 },
            { "Armor", 2 },
            { "Accessory", 3 }
        };
    }

    // Item関連の定数
    public static class Items
    {
        public const int MaxItems = 6;
    }

    // パス関連の定数
    public static class Paths
    {
        public const string PlayerDataPath = "CharacterData/Player";
        public const string EnemyDataPath = "CharacterData/Enemy";
        public const string UIPath = "UI/ButtonPrefab";
    }

    // UI関連の定数
    public static class UI
    {
        public const string HealthBar = "UI/HealthBar";
        public const string ManaBar = "UI/ManaBar";
    }

    // 戦闘関連の定数
    public static class Battle
    {
        public const int MaxPartySize = 4;
        public const int MaxEnemies = 5;
        public const int Prefix_Enemy = 10;
    }

    // ゲーム全般の定数
    public static class Game
    {
        public const float DefaultVolume = 0.5f;
        public const string GameTitle = "Project Epimetheus";
    }
}
