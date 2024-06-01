using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace GameCreator.Runtime.Extensions.Timeline
{
    [Icon(RuntimePaths.GIZMOS + "GizmoActions.png")]
    [CustomStyle("InstructionsMarker")]
    
    public class InstructionsMarker: UnityEngine.Timeline.Marker, INotification
    {
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private InstructionList m_Instructions = new InstructionList();
        
        // MEMBERS: -------------------------------------------------------------------------------

        [NonSerialized] private CopyRunnerInstructionList m_TemplateInstructions;
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public PropertyName id => "Instructions";
        
        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void Run(Args args)
        {
            Transform caller = args.Self.Get<Transform>();
            InstructionList copyRunner = this.MakeCopy(caller).GetRunner<InstructionList>(); 
            
            _ = copyRunner.Run(args);
        }
        
        // PRIVATE METHODS: -----------------------------------------------------------------------
        
        private CopyRunnerInstructionList MakeCopy(Transform caller)
        {
            if (this.m_TemplateInstructions == null)
            {
                this.m_TemplateInstructions = CopyRunnerInstructionList
                    .CreateTemplate<CopyRunnerInstructionList>(this.m_Instructions);
            }
                
            return this.m_TemplateInstructions.CreateRunner<CopyRunnerInstructionList>(
                caller.position, 
                caller.rotation,
                null
            );
        }
    }
}