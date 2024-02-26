using UniRx;
using UnityEngine;

namespace FanComposition.Fan
{
    public class InputObjectView : MonoBehaviour
    {
        public string Description { get; set; }
        public ReactiveCommand Interact { get; } = new ReactiveCommand();
    }
}