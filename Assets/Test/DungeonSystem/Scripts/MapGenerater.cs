using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    // プレハブをInspectorから設定できるようにします
    public GameObject pathPrefab;
    public GameObject wallPrefab;
    public GameObject greenWallPrefab;
    public GameObject doorPrefab;
    public GameObject itemPrefab;
    public GameObject playerPrefab;

    // マップデータ
    int[,] MAP = new int[,]
    {
        {1,1,1,1,1,1,1,1,1,1},
        {1,0,0,0,2,0,0,0,0,1},
        {1,0,1,0,3,0,1,1,0,1},
        {1,0,1,0,0,0,0,1,0,1},
        {1,0,1,1,1,1,0,1,0,1},
        {1,0,0,0,0,1,0,1,0,1},
        {1,1,1,1,0,1,0,1,0,1},
        {1,0,0,1,0,0,0,0,4,1},
        {1,0,0,0,0,1,1,1,0,1},
        {1,1,1,1,1,1,1,1,1,1}
    };

    // 各セルのサイズを決定します
    public float cellSize = 1.0f;
    void Start()
    {
        GenerateMap();
        Instantiate(playerPrefab, new Vector3(1, 0.5f, 1), Quaternion.identity); // プレイヤーを(1,0,1)の位置に初期配置
    }


    // マップ生成関数
    void GenerateMap()
    {
        for (int y = 0; y < MAP.GetLength(0); y++)
        {
            for (int x = 0; x < MAP.GetLength(1); x++)
            {
                Vector3 position = new Vector3(x * cellSize, 0, y * cellSize); // マップ上の座標を計算

                // マップデータに応じて対応するプレハブを生成
                switch (MAP[y, x])
                {
                    case 0:
                        Instantiate(pathPrefab, position, Quaternion.identity);
                        break;
                    case 1:
                        Instantiate(wallPrefab, position, Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(greenWallPrefab, position, Quaternion.identity);
                        break;
                    case 3:
                        Instantiate(doorPrefab, position, Quaternion.identity);
                        break;
                    case 4:
                        Instantiate(itemPrefab, position, Quaternion.identity);
                        break;
                }
            }
        }
    }
}
