using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Test_BattleManager_v2 : MonoBehaviour
{
    // 変数宣言
    private List<CharacterParameters> pl_chrctrs; // プレイヤーのリスト
    private List<EnemyParameters> enemies; // 敵のリスト
    private List<TurnCommand> commands; // コマンドリスト

    // Start is called before the first frame update
    void Start()
    {
        // =============================プロトタイプのみの処理=============================
        // キャラクターの初期化
           pl_chrctrs = new List<CharacterParameters>();
        for (int i = 0; i < Constants.Character.Chrctr_Names.Length; i++)
        {
            pl_chrctrs.Add(new CharacterParameters(i));
        }
        // ================================================================================

        // 戦闘の初期化
        InitializeBattle();

        // バトルの開始
        commands = new List<TurnCommand>();
        for (int i = 0; i < pl_chrctrs.Count; i++)
        {
            if (pl_chrctrs[i].is_Alive)
            {
                // コマンド入力受付許可とUI表示
                AllowCommandInput(i);
                break;
            }
        }
    }

    // コマンド入力受付許可とUI表示
    public void AllowCommandInput(int chara_id)
    {
        // UIマネージャーに「chara_id」のコマンド選択を表示するよう指示
        this.GetComponent<Test_BattleManagerUI_v2>().ShowCommandUI(chara_id);
    }

    public void CommandInput(int[] command)// 確定したときに呼ばれる
    {
        // コマンドの受け取り
    }

    // 戦闘の初期化
    void InitializeBattle()
    {
        // プレイヤーと敵を初期化
        enemies = CreateEnemyTeam(5); // 敵をランダムで3体生成

        // UIの初期化
        this.GetComponent<Test_BattleManagerUI_v2>().InitializeEnemyUI(enemies); // 敵UIの初期化
        this.GetComponent<Test_BattleManagerUI_v2>().InitializePlayerUI(pl_chrctrs); // プレイヤーUIの初期化
        
    }

    // 敵チームの作成（ランダム生成）
    private List<EnemyParameters> CreateEnemyTeam(int count)
    {
        List<EnemyParameters> enemyTeam = new List<EnemyParameters>();
        for (int i = 0; i < count; i++)
        {
            // 敵の抽選
            int enemyID = 0;    // 仮に0を設定
            // int enemyID = Random.Range(0, Constants.Enemy.Enemy_Names.Length);    // ランダムで敵を選択
            // 同種の敵がいた場合、unique_numをA,B,C...と付与
            char unique_num = 'A';
            for (int j = 0; j < i; j++)
            {
                if (enemyTeam[j].Chara_ID == enemyID)
                {
                    unique_num = (char)(unique_num + 1);
                }
            }
            enemyTeam.Add(new EnemyParameters(unique_num, enemyID, i+Constants.Battle.Prefix_Enemy));    // 敵の生成
        }

        // 敵の生成確認
        foreach (var enemy in enemyTeam)
        {
            Debug.Log(enemy.Name + "の初期パラメータ\n" + "Lv: " + enemy.Level);
        }

        return enemyTeam;
    }

    // ターン順序を計算
    private void CalculateTurnOrder()
    {
        // 素早さでソート
        commands.Sort((a, b) => b.Speed - a.Speed);
    }

    // コマンドリストの作成
    private class TurnCommand{
        // コマンドの情報
        public int Chara_ID { get; private set; } // キャラクターID
        public int Action { get; private set; } // 大行動
        public int Target { get; private set; } // 対象
        public int Skill { get; private set; } // スキル
        public int Speed { get; private set; } // 素早さ

        // コンストラクタ
        public TurnCommand(int chara_id, int action, int target, int skill, int speed){
            Chara_ID = chara_id;
            Action = action;
            Target = target;
            Skill = skill;
            Speed = speed;
        }
    }
}
