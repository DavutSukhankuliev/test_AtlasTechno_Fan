using UniRx;
using UnityEngine;

namespace FanComposition
{
    public class InputObjectView : MonoBehaviour
    {
        public ReactiveCommand Interact = new ReactiveCommand();
    }
}