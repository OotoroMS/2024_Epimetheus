# Testing Conventions

## Scope
- Core：EditModeユニットテストを主戦場
- Runtime/Presentation：PlayModeはスモーク中心（最小限）

## Layout
- Features/<Feature>/Code/Tests/EditMode/
- Features/<Feature>/Code/Tests/PlayMode/
- Shared/Code/Tests/

## Naming
- <ClassName>Tests.cs
- テスト名は Given_When_Then を推奨

## Determinism
- RNGはseed固定
- 時刻/乱数/外部IOは注入で差し替える

## Minimum smoke set (例)
- 起動→主要シーンロード→UI生成→セーブ→ロード