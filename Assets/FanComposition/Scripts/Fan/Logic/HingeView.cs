using UnityEngine;

namespace FanComposition
{
    public class HingeView : MonoBehaviour
    {
        [field: SerializeField] public HingeJoint HingeJoint { get; set; }
        [field: SerializeField] public FixedJoint FixedJoint { get; set; }
    }
}