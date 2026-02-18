# Conventions Index

## Read order
1. 10_Directory_Structure.md
2. 50_Asmdef_and_Dependencies.md
3. 20_Coding_Standards.md
4. 30_Testing.md
5. 60_Data_and_Content.md
6. 40_Logging_and_Debug.md
7. 70_Codex_Instructions.md

## Principles
- Feature単位で完結（Features/<Feature> で探索が完了すること）
- 共有はSharedへ。ただしSharedはMechanismのみ（Policyは入れない）
- 依存は一方向（Core→Runtime→Presentation）
- CoreはUnityEngine参照禁止（原則）