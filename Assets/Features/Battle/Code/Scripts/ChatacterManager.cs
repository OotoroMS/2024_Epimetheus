using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

[System.Serializable]
public class ChrctrPrmtrs{
    public string Name { get; private set; }    // キャラクター名
    public int Chara_ID { get; private set; }    // キャラクターID
    public int Level { get; private set; }     // レベル
    public int Exp { get; private set; }   // 経験値
    public string Job { get; private set; }   // 職業
    public bool is_Alive { get; private set; }   // 生存フラグ
    public Dictionary<string, int> Elmnt_Parameters { get; private set; }   // 基礎パラメータ
    public Dictionary<string, int> Eqpmnt_Parameters { get; private set; }   // 装備パラメータ
    public Dictionary<string, int> Nrml_Parameters { get; private set; }  // 通常パラメータ Elmnt_Parameters + Eqpmnt_Parameters
    public Dictionary<string, int> Crrnt_Parameters { get; private set; } // 現在のパラメータ
    public int[] Items { get; private set; }    // 所持アイテム
    public Dictionary<string, int> Equipment { get; private set; }    // 装備品
    public Dictionary<string, int> AbnormalStatus { get; private set; }    // 状態異常
    public bool hold_GoddessIdle { get; private set; }


    // コンストラクタ
    public ChrctrPrmtrs(int ID)
    {
        // キャラクターIDを設定
        Chara_ID = ID;
        // キャラクター名を設定
        Name = Constants.Character.Chrctr_Names[ID];
        // 職業を設定
        Job = Constants.Character.Chrctr_Jobs[ID];
        // 経験値を初期化
        Exp = 0;
        // 生存フラグを設定
        is_Alive = true;
        // 状態異常を初期化
        AbnormalStatus = new Dictionary<string, int>();
        // 女神像を保持しているか
        hold_GoddessIdle = false;
        
        // Resources内の「General/CharacterParams.json」から読み込む
        TextAsset jsonText = Resources.Load<TextAsset>("General/CharacterParams");
        if (jsonText == null)
        {
            Debug.LogError("CharacterParams.jsonが見つかりません");
            return;
        }

        // JSONをパースして、デシリアライズする
        var jl_Character = new JsonLoader.Character();
        var jsonData = jl_Character.LoadCharacterParams(jsonText.text);
        
        // Elmnt_Parametersの初期化
        Elmnt_Parameters = new Dictionary<string, int>();
        for (int i = 0; i < Constants.Parameters.Parameter_Names.Length; i++)
        {
            if (i < jsonData.InitialParams[0].Length)
            {
                Elmnt_Parameters[Constants.Parameters.Parameter_Names[i]] = jsonData.InitialParams[0][i];
            }
            else
            {
                Debug.LogWarning($"Parameter_Namesのインデックス {i} が InitialParams[0] の範囲を超えています。");
            }
        }


        // 初期装備を設定
        Equipment = new Dictionary<string, int>();
        for (int i = 0; i < Constants.Equipment.Equipment_Names.Length; i++)
        {
            if (i < jsonData.InitialEquip[0].Length)
            {
                Equipment[Constants.Equipment.Equipment_Names[i]] = jsonData.InitialEquip[0][i];
            }
            else
            {
                UnityEngine.Debug.LogWarning($"Equipment_Namesのインデックス {i} が InitialEquip[0] の範囲を超えています。");
            }
        }

        // 初期アイテムを設定
        Items = new int[Constants.Items.MaxItems];
        // 所持アイテムを設定
        // ***現在は全て0で初期化
        for (int i = 0; i < Items.Length; i++)
        {
            Items[i] = 0;
        }

        // 加入レベルまでレベルアップさせる
        Level = 1;
        // 初期パラメータ内に加入レベルが含まれているか確認
        while (Level < jsonData.InitialParams[Chara_ID][8])
        {
            LevelUp();
        }

        // 生成したキャラクターのパラメータを表示
        ShowParameters();
    }

    // レベルアップ処理
    public void LevelUp()
    {
        // Resources内の「General/CharacterParams.json」から読み込む
        TextAsset json = Resources.Load<TextAsset>("General/CharacterParams");
        if (json == null)
        {
            Debug.LogError("CharacterParams.jsonが見つかりません");
            return;
        }

        // JSONをパースして、デシリアライズする
        var jloader = new JsonLoader();
        var jl_Character = new JsonLoader.Character();
        var jsonData = jl_Character.LoadCharacterParams(json.text);

        // 次の中間パラメータを取得
        var nextParamsList = jsonData.MiddleParams;
        for(int i=0; i < nextParamsList.Length; i++)
        {
            // 現在のリストと現在のレベルを比較
            var tmp_middleparams = nextParamsList[i];
            if (tmp_middleparams.Level < this.Level)
            {
                // 目標値を発見

                // 現在のパラメータとレベル、目標値のパラメータとレベルから、上昇値を計算
                int[] gap = new int[Elmnt_Parameters.Count];
                int[] up_value = new int[Elmnt_Parameters.Count];
                for(int j=0; j<gap.Length; j++)
                {
                    gap[j] = tmp_middleparams.Params[Chara_ID][j] - Elmnt_Parameters[Constants.Parameters.Parameter_Names[j]];
                    up_value[j] = gap[j] / (tmp_middleparams.Level - this.Level);
                }

                // 現在のパラメータを更新
                for(int j=0; j<Elmnt_Parameters.Count; j++)
                {
                    Elmnt_Parameters[Constants.Parameters.Parameter_Names[j]] += up_value[j];
                }

                break;
            }
            else if(tmp_middleparams.Level == Level)
            {
                // 現在のリストと現在のレベルが一致した場合、目標値に更新
                for(int j=0; j<Elmnt_Parameters.Count; j++)
                {
                    Elmnt_Parameters[Constants.Parameters.Parameter_Names[j]] = tmp_middleparams.Params[this.Chara_ID][j];
                }
                
                break;
            }
        }

        // レベルを上げる
        Level++;

        return;
    }

    // パラメータ更新処理
    public void Update_NrmlPrms()
    {
        // 通常パラメータを更新
        Nrml_Parameters = new Dictionary<string, int>();
        foreach (var key in Elmnt_Parameters.Keys)
        {
            Nrml_Parameters[key] = Elmnt_Parameters[key] + Eqpmnt_Parameters[key];
        }
    }

    // 装備変更処理 後に修正
    public void Change_Equipment(string type, int itemID)
    {

    }

    // [デバッグ用] パラメータ表示
    public void ShowParameters()
    {
        string log = $"\nChara_ID {Chara_ID}: {Name}, {Level}, {Job},\n";
        foreach (var key in Elmnt_Parameters.Keys)
        {
            log += $"\t{key}: {Elmnt_Parameters[key]}, ";
        }
        Debug.Log(log);
    }
}