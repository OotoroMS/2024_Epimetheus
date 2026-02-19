using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace BattleUI_Test
{
    public class BTN_MainCommand : VisualElement
    {
        private Label m_commandName;
        private VisualElement m_commandIcon;

        public new class UxmlFactory : UxmlFactory<BTN_MainCommand, UxmlTraits> { }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            public UxmlAssetAttributeDescription<BTN_MainCommandDefinition> _command = new() { name = "command-definition" };

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                if (_command.TryGetValueFromBag(bag, cc, out BTN_MainCommandDefinition value))
                {
                    var command = ve as BTN_MainCommand;
                    command.m_commandName.text = value.commandName;
                    command.m_commandIcon.style.backgroundImage = value.commandIcon;
                }
            }
        }

        public BTN_MainCommand()
        {
            var treeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Features/Battle/Content/UI/UXML/BTN_MainCommand.uxml");
            var container = treeAsset.Instantiate();
            hierarchy.Add(container);

            m_commandName = container.Q<Label>("Name");
            m_commandIcon = container.Q<VisualElement>("Icon");

            var command = container.Q<VisualElement>("Root");
            command.AddManipulator(new Clickable(()=>Debug.Log("Click")));
        }
    }
}
