using UnityEngine;

namespace BattleUI_Test
{
    [CreateAssetMenu(fileName = "BTN_MainCommandDefinition", menuName = "BTN_MainCommandDefinition", order = 0)]
    public class BTN_MainCommandDefinition : ScriptableObject
    {
        public Texture2D commandIcon;
        public string commandName;
    }
}