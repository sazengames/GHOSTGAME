using GameCreator.Runtime.Extensions.Timeline;
using UnityEditor;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Extensions.Timeline
{
    [CustomEditor(typeof(InstructionsTrack))]
    public class InstructionsTrackEditor : UnityEditor.Editor
    {
        private VisualElement m_Root;
        
        public override VisualElement CreateInspectorGUI()
        {
            this.m_Root = new VisualElement();
            return this.m_Root;
        }
    }
}