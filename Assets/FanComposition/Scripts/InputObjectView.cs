using UniRx;
using UnityEngine;

namespace FanComposition
{
    public class InputObjectView : MonoBehaviour
    {
        //[field: SerializeField] public HingeJoint Joint { get; set; }

        public BoolReactiveProperty Interacted { get; } = new BoolReactiveProperty();
    }
}