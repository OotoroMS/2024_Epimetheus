using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class Test_BattleManagerUI_v2 : MonoBehaviour
{

    #region UI要素の定義
    [System.Serializable]
    class UIdefinition_Enemy
    {
        public Button BTN_MainCommand;
        public TMP_Text TXT_MainCommand;
        public Image IMG_MainCommand;
    }
    
    [System.Serializable]
    class UIdefinition_Player
    {
        public Button BTN_MainCommand;
        public TMP_Text TXT_MainCommand;
        public Image IMG_MainCommand;
    }
    #endregion
    // UIオブジェクト取得(Inspectorから設定)
    [SerializeField] private GameObject enemyUIPrefab; // 敵UIのプレハブ
    [SerializeField] private Transform enemyUIParent; // 敵UIの親オブジェクト

    [SerializeField] private GameObject playerUIPrefab; // プレイヤーUIのプレハブ
    [SerializeField] private Transform playerUIParent; // プレイヤーUIの親オブジェクト

    [SerializeField] private GameObject mainCommandUI; // メインコマンドUIのプレハブ
    [SerializeField] private GameObject targetCommandUI; // ターゲットコマンドUIのプレハブ
    [SerializeField] private GameObject actionCommandUI; // アクションコマンドUIのプレハブ
    [SerializeField] private GameObject actionCommandUI_Enemy; // 敵のアクションコマンドUIのプレハブ

    //# BattleManagerから呼び出し
    //## UIの初期化
    //### 敵UIの初期化
    public void InitializeEnemyUI(List<EnmyPemtrs> enemies)
    {
        // 敵UIの初期化
        foreach (Transform child in enemyUIParent)
        {
            Destroy(child.gameObject); // 既存の敵UIを削除
        }

        int enemyCount = enemies.Count; // 敵の数を取得
        float spacing = 200f; // UI同士の間隔（ピクセル単位、要調整）

        // 敵UIを生成
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject enemyUI = Instantiate(enemyUIPrefab, enemyUIParent); // 敵UIのプレハブをインスタンス化
            EnemyUI enemyUIComponent = enemyUI.GetComponent<EnemyUI>(); // EnemyUIコンポーネントを取得
            int tmp_i = i; // クロージャ対策
            enemyUIComponent.BTN_Target_Enemy.onClick.AddListener(() => CLK_Target_Enemy(tmp_i)); // ボタンのクリックイベントにリスナーを追加
            enemyUIComponent.TXT_Target_Enemy.text = enemies[i].Name; // 敵の名前を表示
            enemyUIComponent.IMG_Target_Enemy.sprite = Resources.Load<Sprite>("Enemy/" + enemies[i].Chara_ID); // 敵の画像を表示

            // 配置処理
            RectTransform rt = enemyUI.GetComponent<RectTransform>();
            float x = (i - (enemyCount - 1) / 2f) * spacing; // 中心基準のX座標
            rt.anchoredPosition = new Vector2(x, 0f); // Yは0に固定（必要なら調整）
        }
    }
    //### プレイヤーUIの初期化
    public void InitializePlayerUI(List<ChrctrPrmtrs> players)
    {
        // ガード（Inspector未設定などの二次被害を止める）
        if (players == null || players.Count == 0)
        {
            Debug.LogError("InitializePlayerUI: players が null もしくは空です");
            return;
        }
        if (playerUIPrefab == null || playerUIParent == null)
        {
            Debug.LogError("InitializePlayerUI: playerUIPrefab もしくは playerUIParent が未設定です（Inspector を確認）");
            return;
        }

        // プレイヤーUIの初期化
        foreach (Transform child in playerUIParent)
        {
            Destroy(child.gameObject); // 既存のプレイヤーUIを削除
        }

        for (int i = 0; i < players.Count; i++)
        {
            GameObject playerUI = Instantiate(playerUIPrefab, playerUIParent);
            // ★ PlayerUI コンポーネント必須
            var playerUIComponent = playerUI.GetComponent<PlayerUI>();
            if (playerUIComponent == null)
            {
                Debug.LogError("InitializePlayerUI: PlayerUI コンポーネントが playerUIPrefab に付いていません。Prefab を確認してください。");
                continue;
            }
            // ★ 中の参照も念のため確認
            if (playerUIComponent.BTN_Target_Player == null ||
                playerUIComponent.TXT_Target_Player == null ||
                playerUIComponent.IMG_Target_Player == null)
            {
                Debug.LogError("InitializePlayerUI: PlayerUI 内の参照(BTN/TXT/IMG)が未割り当てですPlayerUI スクリプトの SerializeField をPrefabで割り当ててください。");
                continue;
            }

            int idx = i; // ループ変数キャプチャ対策
            playerUIComponent.BTN_Target_Player.onClick.AddListener(() => CLK_Target_Player(idx));
            playerUIComponent.TXT_Target_Player.text = players[i].Name;
            playerUIComponent.IMG_Target_Player.sprite = Resources.Load<Sprite>("Player/" + players[i].Chara_ID);

            // 配置処理
            RectTransform rt = playerUI.GetComponent<RectTransform>();
            float spacing = 200f; // UI同士の間隔（ピクセル単位、要調整）
            float x = (i - (players.Count - 1) / 2f) * spacing; // 中心基準のX座標
            rt.anchoredPosition = new Vector2(x, 0f); // Yは0に固定（必要なら調整）
        }
    }
    //### メインコマンドUIの初期化
    public void InitializeMainCommandUI()
    {
        // メインコマンドUIの初期化
        foreach (Transform child in mainCommandUI.transform)
        {
            Destroy(child.gameObject); // 既存のメインコマンドUIを削除
        }

        // メインコマンドUIを生成
        Button mainCommandButton = mainCommandUI.GetComponent<Button>(); // メインコマンドUIのボタンを取得
        mainCommandButton.onClick.AddListener(() => CLK_MainCommand()); // ボタンのクリックイベントにリスナーを追加
    }
    //## コマンド選択UIを表示するメソッド
    public void ShowCommandUI(int chara_id){
        // chara_idのキャラクターのコマンド選択UIを表示
    }

    //## ボタンの入力を受け取る
    public void CLK_MainCommand(){
        //自身のメインコマンドの種類を判断し、デバッグ出力
        Debug.Log("MainCommandがクリックされました");
    }

    public void CLK_Target_Enemy(int index){
        Debug.Log("Target_Enemy" + index + "がクリックされました");
    }

    public void CLK_Target_Player(int index){
        Debug.Log("Target_Player" + index + "がクリックされました");
    }
}