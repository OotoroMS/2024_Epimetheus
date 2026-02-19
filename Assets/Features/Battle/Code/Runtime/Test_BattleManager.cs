using System;
using System.Collections.Generic;
using UnityEngine;

public class Test_BattleManager : MonoBehaviour
{
    private List<Character> pl_charas; // プレイヤーのリスト
    private List<Character> enemies; // 敵のリスト
    private List<Character> turnOrder; // 行動順リスト

     void Start()
    {
        InitializeBattle();
    }

    // 戦闘初期化
    void InitializeBattle()
    {
        // プレイヤーと敵を初期化
        pl_charas = CreatePlayerTeam();
        enemies = CreateEnemyTeam(3); // 敵をランダムで3体生成

        // ターン順序決定
        CalculateTurnOrder();

        StartCoroutine(BattleLoop());
    }

    // プレイヤーチームの作成
    private List<Character> CreatePlayerTeam()
    {
        var playerTeam = new List<Character>();
        for (int i = 0; i < 2; i++)
        {
            playerTeam.Add(new Character(Constants.Character.Chrctr_Names[i], true));
        }
        return playerTeam;
    }

    // 敵チームの作成（ランダム生成）
    private List<Character> CreateEnemyTeam(int count)
    {
        List<Character> enemyTeam = new List<Character>();
        for (int i = 0; i < count; i++)
        {
            enemyTeam.Add(new Character($"Enemy{i + 1}", false));
        }
        return enemyTeam;
    }

    // ターン順序を計算
    private void CalculateTurnOrder()
    {
        turnOrder = new List<Character>();
        turnOrder.AddRange(pl_charas);
        turnOrder.AddRange(enemies);

        // 素早さでソート
        turnOrder.Sort((a, b) => b.Parameters["Sp"] - a.Parameters["Sp"]);
    }

    // 戦闘ループ
    private System.Collections.IEnumerator BattleLoop()
    {
        while (true)
        {
            foreach (var character in turnOrder)
            {
                if (character.IsDead) continue;

                // 行動を選択
                if (character.IsPlayer)
                {
                    yield return PlayerTurn(character);
                }
                else
                {
                    EnemyTurn(character);
                }

                // 全滅チェック
                if (CheckGameOver()) yield break;
            }

            // ターン終了後の準備
            CalculateTurnOrder();
        }
    }

    // プレイヤーのターン
    private System.Collections.IEnumerator PlayerTurn(Character player)
    {
        Debug.Log($"{player.Name}のターン");
        yield return new WaitForSeconds(1); // コマンド入力待機（仮）

        // コマンドを実行（仮: 敵に攻撃）
        var target = enemies[UnityEngine.Random.Range(0, enemies.Count)];
        player.Attack(target);

        // 死亡判定
        RemoveDeadCharacters();
    }

    // 敵のターン
    private void EnemyTurn(Character enemy)
    {
        Debug.Log($"{enemy.Name}のターン");

        // ランダムなプレイヤーを攻撃
        var target = pl_charas[UnityEngine.Random.Range(0, pl_charas.Count)];
        enemy.Attack(target);

        // 死亡判定
        RemoveDeadCharacters();
    }

    // 死亡キャラクターの削除
    private void RemoveDeadCharacters()
    {
        pl_charas.RemoveAll(p => p.IsDead);
        enemies.RemoveAll(e => e.IsDead);
    }

    // 全滅チェック
    private bool CheckGameOver()
    {
        if (pl_charas.Count == 0)
        {
            Debug.Log("ゲームオーバー！ 敗北しました。");
            return true;
        }

        if (enemies.Count == 0)
        {
            Debug.Log("勝利！ 敵を全滅させました。");
            return true;
        }

        return false;
    }

    // キャラクタークラス
    public class Character
    {
        public string Name { get; private set; }
        public bool IsPlayer { get; private set; }
        public bool IsDead => Parameters["HP"] <= 0;
        public Params Parameters { get; private set; }

        public Character(string name, bool isPlayer)
        {
            Name = name;
            IsPlayer = isPlayer;

            // 仮の初期パラメータ
            Parameters = new Params();
            Parameters["HP"] = 100;
            Parameters["Sp"] = UnityEngine.Random.Range(10, 30); // ランダムな素早さ
            Parameters["At"] = 15;
            Parameters["De"] = 5;
        }

        public void Attack(Character target)
        {
            int damage = Math.Max(0, Parameters["At"] - target.Parameters["De"]);
            target.Parameters["HP"] -= damage;
            Debug.Log($"{Name}が{target.Name}に{damage}のダメージを与えた");
        }
    }

    // パラメータ管理用クラス
    public class Params
    {
        private Dictionary<string, int> parameters;

        public Params()
        {
            parameters = new Dictionary<string, int>()
            {
                {"HP", 0}, // HP: 0になると死ぬ
                {"MP", 0}, // MP: 呪文使用で消費
                {"At", 0}, // At: 攻撃力。物理攻撃時に影響
                {"De", 0}, // De: 防御力。物理防御時に影響
                {"MA", 0}, // MA: 魔攻力。攻撃魔法の威力、弱体化呪文の成功率や威力に影響。
                {"MD", 0}, // MD: 魔防力。攻撃呪文の被ダメージ量、回復魔法の威力、状態異常呪文の成功率に影響
                {"Sp", 0}, // Sp: 素早さ。戦闘時の行動順に影響
                {"Ft", 0}, // Ft: 運命力。実験的に導入。味方の参入時に使う？ 運と一緒かも
            };
        }

        // インデクサーの定義
        public int this[string key]
        {
            get
            {
                if (parameters.ContainsKey(key))
                {
                    return parameters[key];
                }
                else
                {
                    throw new ArgumentException("Invalid parameter key");
                }
            }
            set
            {
                if (parameters.ContainsKey(key))
                {
                    parameters[key] = value;
                }
                else
                {
                    throw new ArgumentException("Invalid parameter key");
                }
            }
        }
    }
}
