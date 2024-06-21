using System.ComponentModel;
using UnityEngine.Timeline;

namespace GameCreator.Runtime.Extensions.Timeline
{
    [TrackColor(0.45f, 0.8f, 0.9f)]
    [TrackBindingType(typeof(Target))]
    [DisplayName("Game Creator/Instructions Track")]
    public class InstructionsTrack : MarkerTrack
    { }
}