using GameCreator.Runtime.Extensions.Timeline;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Extensions.Timeline
{
    [CustomEditor(typeof(Target))]
    public class TargetEditor : UnityEditor.Editor
    {
        private VisualElement m_Root;
        
        public override VisualElement CreateInspectorGUI()
        {
            this.m_Root = new VisualElement();

            SerializedProperty propertySelf = this.serializedObject.FindProperty("m_Self");
            SerializedProperty propertyTarget = this.serializedObject.FindProperty("m_Target");

            PropertyField fieldSelf = new PropertyField(propertySelf);
            PropertyField fieldTarget = new PropertyField(propertyTarget);

            this.m_Root.Add(fieldSelf);
            this.m_Root.Add(fieldTarget);
            
            return this.m_Root;
        }
    }
}