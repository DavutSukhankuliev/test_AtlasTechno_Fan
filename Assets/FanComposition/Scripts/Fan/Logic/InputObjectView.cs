using UniRx;
using UnityEngine;

namespace FanComposition.Fan
{
    public class InputObjectView : MonoBehaviour
    {
        public ReactiveCommand Interact { get; } = new ReactiveCommand();
    }
}