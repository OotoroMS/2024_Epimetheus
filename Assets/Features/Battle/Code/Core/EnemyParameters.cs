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
    private readonly IBattleLogger logger;

    // コンストラクタ
    public EnemyParameters(char unique_char, int chara_id, int unique_num, JsonLoader.Enemy.Jdata_EnemyParams enemyData, IBattleLogger battleLogger = null){
        logger = battleLogger;
        Chara_ID = chara_id;
        Unique_Num = unique_num;
        Nrml_Parameters = new Dictionary<string, int>();

        if(enemyData == null){
            logger?.LogError("EnemyParams が null です");
            return;
        }

        if(enemyData.NormalEnemy == null || Chara_ID >= enemyData.NormalEnemy.Length){
            logger?.LogError($"EnemyParams の Chara_ID が不正です: {Chara_ID}");
            return;
        }
        
        // 名前の設定
        Name = enemyData.NormalEnemy[Chara_ID].Name + unique_char;

        // レベルの設定
        Level = enemyData.NormalEnemy[Chara_ID].Level;

        // スキルの設定


        // 通常パラメータの設定
        if(enemyData.NormalEnemy[Chara_ID].Params != null){
            for(int i=0; i<Constants.Parameters.Parameter_Names.Length; i++){
                if (i < enemyData.NormalEnemy[Chara_ID].Params.Length){
                    Nrml_Parameters[Constants.Parameters.Parameter_Names[i]] = enemyData.NormalEnemy[Chara_ID].Params[i];
                }else{
                    logger?.LogError($"EnemyParams.jsonのパラメータが足りません: {Constants.Parameters.Parameter_Names[i]}");
                    return;
                }
            }
        }else{
            logger?.LogError("EnemyParams.jsonのパラメータが空です");
            return;
        }

        // 現在のパラメータを通常パラメータに設定
        Crrnt_Parameters = new Dictionary<string, int>(Nrml_Parameters);
    }
}
