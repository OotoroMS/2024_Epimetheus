# Directory Structure

このファイルは「プロジェクトの唯一の正」として扱う。

- ルート構造
- 各ディレクトリの責務
- Content Packageの標準形
- 置き場所判断ルール

Assets/
    Docs/                              # プロジェクト全体の仕様・規約（最初に読む）
      Architecture.md                  # 全体構造（Feature一覧、依存、データフロー）
      Conventions/                     # 規約を分割配置（Codex/人間の参照性を上げる）
        00_Index.md                    # 規約の目次・読む順序
        10_Directory_Structure.md      # ディレクトリ構造（このテンプレ本文を転記）
        20_Coding_Standards.md         # コーディング規約（C# / Unity）
        30_Testing.md                  # テスト規約（EditMode/PlayMode/命名/CI前提）
        40_Logging_and_Debug.md        # ログ/デバッグ規約（カテゴリ、レベル、ビルド差）
        50_Asmdef_and_Dependencies.md  # asmdef/依存方向/禁止事項
        60_Data_and_Content.md         # Data/Contentの置き分け、生成物の扱い
        70_Codex_Instructions.md       # Codexへの命令（読む場所、変更範囲、検収）

    Shared/                            # 複数Featureで共有する“仕組み”と“共通コンテンツ”
      Code/                            # 共有コード（Mechanismのみ。Policyを入れない）
        Core/                          # 純C#（UnityEngine参照禁止）。ユニットテスト主戦場
        Runtime/                       # Unity依存の共通サービス（Audio/Scene/Input/AssetLoader等）
        Presentation/                  # 共通UI部品のコード（Dialog/Toast/共通WidgetのVM等）
        Tests/                         # Shared/Codeのテスト（EditMode中心）
      Content/                         # 共有“名詞”パッケージ（敵/アイテム/共通UI素材など）
        Actors/                        # 例：Enemies/Goblin, NPCs/ShopKeeper…
        Items/                         # 例：Potion/…
        Skills/                        # 例：Fireball/…
        UI/                            # 例：共通ボタン画像・アイコン・テーマ素材
        VFX/                           # 例：共通エフェクト
        Audio/                         # 例：共通SE/BGM素材
      Data/                            # 個体に紐づかない“全体データ”
        Settings/                      # 例：オプション設定、キーコンフィグ
        Localization/                  # 例：文字列テーブル
        Raw/                           # 例：CSV/JSONの元データ（手編集/外部生成）
        Generated/                     # 例：Raw→生成したSO/アセット（自動生成物）

    Features/                          # Feature単位で完結させる（人間/Codexが迷わない）
      <FeatureName>/                   # 例：Battle, Dungeon, Title, Map, Save...
        Docs/                          # そのFeatureの仕様（Codexが最初に読む）
          README.md                    # Responsibility/API/Invariants/Test cases
        Code/                          # Featureコード
          Core/                        # 純C#ロジック（計算/状態機械/判定/生成）。Unity依存禁止
          Runtime/                     # MonoBehaviour/Unity API依存の薄い層（ポーリング/接着）
          Presentation/                # UI Toolkit(UXML/USS)の制御、ViewModel、表示更新
          Tests/                       # Featureテスト
            EditMode/                  # Coreのユニットテスト
            PlayMode/                  # 結合スモーク（Scene/Prefab/簡易動作）
        Content/                       # Featureに閉じた“名詞”パッケージ（後でSharedへ昇格可）
          (同階層に Actors/Items/Skills/UI/VFX/Audio 等を必要に応じて)
        Data/                          # Feature固有データ（個体に紐づかないもの）
          Raw/                         # CSV/JSONなど元データ
          Assets/                      # ScriptableObject等のUnityアセット
          Generated/                   # 生成物（Raw→Assets）
        Scenes/                        # Feature所有シーン（Prototype含む）

    _ThirdParty/                       # 外部アセット隔離（原則そのまま使い、改変しない）

  (Unity標準フォルダは原則そのまま：TextMesh Pro / UI Toolkit / Settings 等)