using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 1.0f;  // 移動速度
    private float rotationSpeed = 90f;  // 1回転あたりの角度
    private float moveLerpSpeed = 4.5f;  // 移動補間速度
    private float rotateLerpSpeed = 270.0f;  // 回転補間速度
    private float cellSize = 1.0f;  // セルサイズ
    private float player_hight = 0.5f;  // プレイヤーの高さ

    private Vector3 targetPosition;  // 移動完了フラグ
    private Quaternion targetRotation;  // 回転完了フラグ
    private bool isMoving = false;  // 移動完了フラグ
    private bool isRotating = false;  // 回転完了フラグ
    public int[] playerGridPosition = new int[2] { 0, 0 };  // プログラム内での座標保管

    // ほかのスクリプトとの連携用
    [SerializeField] private MapDataManager map;

    void Start()
    {
        // mapオブジェクトのができているか確認
        if (map == null)
        {
            Debug.LogError("MapManagerオブジェクトが見つかりません。");
        }

        // 初期位置と回転を設定
        targetPosition = transform.position;
        targetRotation = transform.rotation;
    }

    void Update()
    {
        // ボタン入力処理
        if (!isMoving && !isRotating)
        {
            HandleInput();
        }

        // プレイヤーの移動と回転の更新
        MovePlayer();
        RotatePlayer();
    }

    void HandleInput()
    {
        // wキー押下時
        if (Input.GetKeyDown(KeyCode.W))
        {
            // 当たり判定をとる
            if (!CheckCollision())
            {
                Debug.Log("移動不可");
                return;
            }

            // 移動処理
            AttemptMove();
        }
        // aキー押下時
        else if (Input.GetKeyDown(KeyCode.A))
        {
            AttemptRotate(-rotationSpeed);  // 90度右回転
        }
        // dキー押下時
        else if (Input.GetKeyDown(KeyCode.D))
        {
            AttemptRotate(rotationSpeed);  // 90度左回転
        }
    }

    bool CheckCollision()
    {
        // MapDataManager が存在しないなら移動不可扱い
        if (MapDataManager.Instance == null)
        {
            Debug.LogError("MapDataManager.Instance が null です。シーンに MapDataManager を配置してください。");
            return false;
        }

        int px = playerGridPosition[0];
        int pz = playerGridPosition[1];

        Vector3 f = transform.forward;
        int nextX = px + Mathf.RoundToInt(f.x);
        int nextZ = pz + Mathf.RoundToInt(f.z);

        // 中央管理クラスに問い合わせる
        return MapDataManager.Instance.IsWalkable(nextX, nextZ);
    }
    
    void AttemptMove()
    {
        // 移動方向を取得
        Vector3 direction = transform.forward;  // プレイヤーの前方向
        targetPosition = transform.position + moveSpeed * direction * cellSize;

        // Y座標をplayer_hightに固定
        targetPosition.y = cellSize * player_hight;
        isMoving = true;
    }

    void AttemptRotate(float rotationAmount)
    {
        // 回転量を設定
        targetRotation = Quaternion.Euler(0, transform.eulerAngles.y + rotationAmount, 0);
        isRotating = true;
    }

    void MovePlayer()
    {
        if (transform.position != targetPosition)
        {
            // プレイヤーの移動処理
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveLerpSpeed * Time.deltaTime);

            if (transform.position == targetPosition)
            {
                transform.position = targetPosition;  // 最終位置に設定
                isMoving = false;  // 移動完了フラグを解除
            }

            // プログラム内での座標保管の更新
            playerGridPosition[0] = Mathf.RoundToInt(transform.position.x / cellSize);
            playerGridPosition[1] = Mathf.RoundToInt(transform.position.z / cellSize);
        }
    }

    void RotatePlayer()
    {
        if (transform.rotation != targetRotation)
        {
            // プレイヤーの回転処理
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateLerpSpeed * Time.deltaTime);

            if (transform.rotation == targetRotation)
            {
                isRotating = false;  // 回転完了フラグを解除
            }
        }
    }
}
