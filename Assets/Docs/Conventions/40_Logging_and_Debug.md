# Logging & Debug

## Levels
- Error：継続不能/データ破損
- Warn：想定外だが継続可能
- Info：主要イベント（遷移/ロード/セーブ）
- Debug：詳細（開発ビルドのみ）

## Categories (例)
- BATTLE, DUNGEON, SAVE, UI, SCENE, AUDIO

## Rules
- CoreはILogger（または薄いITracer）を注入（UnityのDebug.Log直打ちを避ける）
- RuntimeはUnityのログ出力可。ただしカテゴリ付けする
- デバッグHUD等はShared/Presentationに置く