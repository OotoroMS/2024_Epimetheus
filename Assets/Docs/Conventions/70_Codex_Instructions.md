# Codex Instructions

## Before coding
1. Read: Docs/Conventions/00_Index.md
2. Read: Docs/Conventions/10_Directory_Structure.md
3. Read the target Feature's Docs/README.md

## Output requirements
- 変更点の要約（何を・なぜ・どこを）
- 追加/更新したテストの一覧
- 破壊的変更がある場合は移行手順

## Safety rails
- SharedにPolicyを追加しない
- ThirdParty配下は原則変更しない（必要ならAdapterを作る）
- CoreにUnity依存を混ぜない
- まず小さく実装→テスト→改善

## Review checklist
- 依存方向は守れているか
- seed固定で再現できるか
- 境界値/エラー処理があるか