# Dungeon Feature README

## Responsibility
- ダンジョンマップの生成・読込・配置（床/壁/ドア/アイテム）を担当する。
- プレイヤーの移動とマップ上のインタラクション（当たり判定、進行可否）を管理する。
- ダンジョン探索状態の初期化と、シーン上での可視化を提供する。

## Public API / Entry points
- Sceneエントリ: `Assets/Test/DungeonSystem/Scene/Test_Dungeon.unity`（検証シーン）。
- Map生成エントリ: `Assets/Test/DungeonSystem/Scripts/MapGenerater.cs`。
- Mapデータエントリ: `Assets/Test/DungeonSystem/Scripts/MapDataManager.cs`。
- Player制御エントリ: `Assets/Test/DungeonSystem/Scripts/PlayerController.cs`。
- テスト用データ: `Assets/Test/DungeonSystem/Data/TestMap.csv`。

## Invariants
- マップ座標系（行・列）とワールド配置の対応規則を崩さないこと。
- 通行不可タイル（壁など）へプレイヤーを侵入させないこと。
- 1セルに複数の排他的オブジェクト（例: 壁とプレイヤー）を重複配置しないこと。
- マップ読込失敗時は不正な部分生成を行わず、失敗を明示できること。

## Test cases
### EditMode
- CSV読込: 正常/異常フォーマット時のパース結果が期待通りであること。
- 生成ロジック: タイルIDごとに正しいPrefabが選択されること。
- 座標変換: マップ座標→ワールド座標の変換が一貫していること。

### PlayMode
- `Test_Dungeon.unity` 起動時にマップが生成されること。
- 入力に応じてプレイヤーが移動し、壁で停止すること。
- ドア/アイテムセルへの到達時に想定イベントが発火すること。
