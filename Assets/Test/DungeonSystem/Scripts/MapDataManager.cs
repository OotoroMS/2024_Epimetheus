using System;
using System.Collections.Generic;
using UnityEngine;

public class MapDataManager : MonoBehaviour
{
    public static MapDataManager Instance { get; private set; }

    [Header("マップCSV (Resourcesや直アサインで設定)")]
    [SerializeField] private TextAsset mapCsv;

    [Header("壁として扱うタイルID一覧")]
    [SerializeField] private List<int> wallTileIds = new List<int>();

    // tiles[x, z] でアクセスする
    private int[,] tiles;
    private int width;
    private int height;

    public int Width  => width;
    public int Height => height;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("MapDataManager が複数存在しています。シーンに1つだけ配置してください。", this);
            Destroy(gameObject);
            return;
        }

        Instance = this;
        LoadFromCsv();
    }

    /// <summary>
    /// CSVファイルを読み込んでマップ配列を構築
    /// </summary>
    private void LoadFromCsv()
    {
        if (mapCsv == null)
        {
            Debug.LogError("mapCsv が指定されていません。MapDataManager に CSV をアサインしてください。", this);
            return;
        }

        // 行ごとに分割（空行は無視）
        string[] lines = mapCsv.text.Split(
            new[] { '\r', '\n' },
            StringSplitOptions.RemoveEmptyEntries
        );

        height = lines.Length;
        if (height == 0)
        {
            Debug.LogError("CSVの行数が0です。", this);
            return;
        }

        int tmpWidth = -1;

        // まず幅を確定させつつ整合性チェック
        for (int z = 0; z < height; z++)
        {
            string line = lines[z].Trim();
            if (string.IsNullOrWhiteSpace(line))
            {
                Debug.LogError($"CSVの {z} 行目が空です。", this);
                return;
            }

            string[] cols = line.Split(',');
            if (tmpWidth < 0)
            {
                tmpWidth = cols.Length;
            }
            else if (cols.Length != tmpWidth)
            {
                Debug.LogError($"CSVの列数が不一致です。0行目:{tmpWidth}列 / {z}行目:{cols.Length}列", this);
                return;
            }
        }

        width  = tmpWidth;
        tiles  = new int[width, height];

        // 実際に数値を詰める
        for (int z = 0; z < height; z++)
        {
            string[] cols = lines[z].Trim().Split(',');
            for (int x = 0; x < width; x++)
            {
                if (!int.TryParse(cols[x], out int tileId))
                {
                    Debug.LogError($"CSVの ({x}, {z}) の値 '{cols[x]}' を int に変換できません。", this);
                    return;
                }

                if (tileId <= 0)
                {
                    Debug.LogWarning($"タイルIDが正の整数ではありません ({x}, {z}) = {tileId}。仕様上は正の整数を使う前提です。");
                }

                tiles[x, z] = tileId;
            }
        }

        Debug.Log($"MapDataManager: CSV読み込み完了。サイズ = {width} x {height}");
    }

    public bool IsInBounds(int x, int z)
    {
        return x >= 0 && x < width && z >= 0 && z < height;
    }

    /// <summary>
    /// タイルIDを取得する。成功したら true。
    /// </summary>
    public bool TryGetTileId(int x, int z, out int tileId)
    {
        if (!IsInBounds(x, z) || tiles == null)
        {
            tileId = 0;
            return false;
        }

        tileId = tiles[x, z];
        return true;
    }

    /// <summary>
    /// 壁かどうか
    /// </summary>
    public bool IsWall(int x, int z)
    {
        if (!TryGetTileId(x, z, out int tileId))
        {
            // 範囲外は壁扱いでもいいし、歩行不可扱いなので true返す
            return true;
        }

        // 壁タイルID一覧に含まれていたら壁
        return wallTileIds.Contains(tileId);
    }

    /// <summary>
    /// 歩行可能（壁ではない）かどうか
    /// </summary>
    public bool IsWalkable(int x, int z)
    {
        return !IsWall(x, z);
    }
}
