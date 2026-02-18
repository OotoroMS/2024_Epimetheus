# asmdef & Dependencies

## Direction (must)
- Feature/Core → Shared/Core (OK)
- Feature/Runtime → Feature/Core + Shared/* (OK)
- Feature/Presentation → Feature/Core + Shared/* (OK)
- Core → UnityEngine (NG)

## Recommended asmdef
- Shared.Core
- Shared.Runtime (ref Shared.Core)
- Shared.Presentation (ref Shared.Core)
- <Feature>.Core (ref Shared.Core)
- <Feature>.Runtime (ref <Feature>.Core, Shared.Runtime)
- <Feature>.Presentation (ref <Feature>.Core, Shared.Presentation)
- <Feature>.Tests.EditMode (ref <Feature>.Core)
- <Feature>.Tests.PlayMode (ref <Feature>.Runtime)

## Prohibitions
- Sharedにゲーム固有ルール（Policy）を入れない
- ThirdParty配下のコード/アセットを直接改変しない（ラップする）