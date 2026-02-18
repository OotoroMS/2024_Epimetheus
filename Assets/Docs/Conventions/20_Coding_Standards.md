# Coding Standards (C# / Unity)

## General
- 目的が読める命名（短縮語を乱用しない）
- 1クラス1責務。巨大クラスは禁止（分割）
- 例外は握りつぶさない（必ずログ or 上位に伝播）

## Core (Unity非依存)
- UnityEngine参照禁止
- 入出力はデータ構造で表現（副作用を最小化）
- RNGはSeededRng（またはIRng）を注入し、再現性を担保

## Runtime (Unity依存)
- MonoBehaviourは薄く：ポーリング/接着/参照解決に限定
- ロジックはCoreへ押し込む（Runtimeで計算しない）

## Presentation
- 表示更新はState/Eventsから導出（直接ロジックを持たない）
- UI ToolkitはUXML/USSをPresentation/UIに固定

## File/Folder
- 1ファイル1型（例外：小さなrecord/enumは同居可）
- 名前は機能＋役割（例：BattleCore, BattleDirector, BattleHudView）