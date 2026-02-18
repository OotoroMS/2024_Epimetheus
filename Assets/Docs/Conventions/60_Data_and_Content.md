# Data & Content

## Data vs Content
- Content：名詞パッケージ（Goblin, Potion, Fireball など）。Prefab/Art/Dataを同居
- Data：個体に紐づかないテーブル/設定/生成元

## Raw / Generated
- Raw：人間が編集するソース（CSV/JSON/YAMLなど）
- Generated：Rawから生成されるUnityアセット（自動生成物）

## Rules
- 2つ以上のFeatureで参照する名詞 → Shared/Content
- 特定Featureで試す名詞 → Features/<Feature>/Content（後で昇格）
- Resourcesはプロトタイプ期のみ許容。所有権はContent/Dataへ寄せる