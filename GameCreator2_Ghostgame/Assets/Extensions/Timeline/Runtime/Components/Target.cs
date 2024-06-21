using GameCreator.Runtime.Common;
using GameCreator.Runtime.Characters;
using UnityEngine;
using UnityEngine.Playables;

namespace GameCreator.Runtime.Extensions.Timeline
{
    [AddComponentMenu("Game Creator/Timeline/Target")]
    [Icon("Assets/Plugins/GameCreator/Extensions/Timeline/Editor/Gizmos/GizmoTarget.png")]
    
    public class Target : MonoBehaviour, INotificationReceiver
    {
        [SerializeField] private PropertyGetGameObject m_Target = GetGameObjectPlayer.Create();
        
        public void OnNotify(Playable origin, INotification notification, object context)
        {
            switch (notification)
            {
                case InstructionsMarker instructionsMarker:
                {
                    GameObject self = this.gameObject;
                    instructionsMarker.Run(new Args(self, this.m_Target.Get(self)));
                    break;
                }
            }
        }
    }
}