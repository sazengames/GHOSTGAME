using GameCreator.Editor.Common;
using GameCreator.Runtime.Extensions.Timeline;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Extensions.Timeline
{
    [CustomEditor(typeof(InstructionsMarker))]
    public class InstructionsMarkerEditor : UnityEditor.Editor
    {
        private VisualElement m_Root;
        
        public override VisualElement CreateInspectorGUI()
        {
            this.m_Root = new VisualElement();

            SerializedProperty instructions = this.serializedObject.FindProperty("m_Instructions");
            PropertyField fieldInstructions = new PropertyField(instructions);

            this.m_Root.Add(new SpaceSmaller());
            this.m_Root.Add(fieldInstructions);
            return this.m_Root;
        }
    }
}