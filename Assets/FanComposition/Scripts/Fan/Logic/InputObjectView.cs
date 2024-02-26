using System;
using UniRx;
using UnityEngine;

namespace FanComposition
{
    public class InputObjectView : MonoBehaviour
    {
        public ReactiveCommand Interact { get; } = new ReactiveCommand();
    }
}