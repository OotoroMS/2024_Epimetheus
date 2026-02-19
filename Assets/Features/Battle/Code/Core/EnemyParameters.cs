using UnityEngine;
using System.Collections.Generic;

public class EnemyParameters{
    public string Name { get; private set; }    // 名前
    public int Chara_ID { get; private set; }    // 敵の種類ID
    public int Unique_Num { get; private set; }  // 戦闘中のユニーク番号
    public int Level { get; private set; }     // レベル
    public int Exp { get; private set; }   // 経験値
    public int[] Skill { get; private set; }     // スキル
    public int[] Item { get; private set; }    // ドロップアイテム
    public Dictionary<string, int> Nrml_Parameters { get; private set; }    // 通常パラメータ
    public Dictionary<string, int> Crrnt_Parameters { get; private set; }    // 現在のパラメータ

    // コンストラクタ
    public EnemyParameters(char unique_char, int chara_id, int unique_num){
        Chara_ID = chara_id;
        Unique_Num = unique_num;
        Nrml_Parameters = new Dictionary<string, int>();

        // jsonファイルからパラメータを読み込む
        TextAsset jsonText = Resources.Load<TextAsset>("Battle/EnemyParams");
        if(jsonText == null){
            Debug.LogError("EnemyParams.jsonが見つかりません");
            return;
        }
        // JSONをパースして、デシリアライズする
        var jl_Enemy = new JsonLoader.Enemy();
        var jsonData = jl_Enemy.LoadEnemyParams(jsonText.text, Chara_ID);
        if(jsonData == null){
            Debug.LogError("EnemyParams.jsonのデシリアライズに失敗しました");
            return;
        }
        
        // 名前の設定
        Name = jsonData.NormalEnemy[Chara_ID].Name + unique_char;

        // レベルの設定
        Level = jsonData.NormalEnemy[Chara_ID].Level;

        // スキルの設定


        // 通常パラメータの設定
        if(jsonData.NormalEnemy[Chara_ID].Params != null){
            for(int i=0; i<Constants.Parameters.Parameter_Names.Length; i++){
                if (i < jsonData.NormalEnemy[Chara_ID].Params.Length){
                    Nrml_Parameters[Constants.Parameters.Parameter_Names[i]] = jsonData.NormalEnemy[Chara_ID].Params[i];
                }else{
                    Debug.LogError($"EnemyParams.jsonのパラメータが足りません: {Constants.Parameters.Parameter_Names[i]}");
                    return;
                }
            }
        }else{
            Debug.LogError("EnemyParams.jsonのパラメータが空です");
            return;
        }

        // 現在のパラメータを通常パラメータに設定
        Crrnt_Parameters = new Dictionary<string, int>(Nrml_Parameters);
    }
}