# Battle Feature README

## Responsibility
- ターン制バトルの進行管理（開始/行動選択/解決/終了）を担当する。
- バトル中に表示するUI（コマンド、キャラクター情報、ログ）の更新責務を持つ。
- 戦闘参加者（Player/Enemy）の状態遷移と、勝敗判定のオーケストレーションを行う。

## Public API / Entry points
- Sceneエントリ: `Assets/Test/BattleSystem/Test_Battle.unity`（検証シーン）。
- Managerエントリ: `Assets/Test/BattleSystem/Scripts/Test_BattleManager_v2.cs`。
- UIエントリ: `Assets/Test/BattleSystem/Scripts/Test_BattleManagerUI_v2.cs`。
- UI Toolkitエントリ: `Assets/Test/BattleSystem/UI/Scripts/UIToolkit_Battle_Test.cs`。

## Invariants
- 1ターン内で同一ユニットが複数回行動しないこと。
- 戦闘中のUI表示（HP/コマンド/対象情報）は内部状態と同期していること。
- 勝敗確定後に次ターンへ遷移しないこと。
- 戦闘参加者リストに `null` 参照を含めないこと。

## Test cases
### EditMode
- ターン進行ロジック: 行動順決定とターン終了条件が想定通りであること。
- 勝敗判定: 全Enemy撃破時/Player全滅時の結果が正しいこと。
- 状態更新: ダメージ反映後のHP下限（0未満にならない）を担保すること。

### PlayMode
- `Test_Battle.unity` 起動時にUIが初期化され、コマンド入力が可能であること。
- 行動実行後にHP表示・ログ表示が更新されること。
- 戦闘終了時に終了状態（操作不可/結果表示）へ遷移すること。
