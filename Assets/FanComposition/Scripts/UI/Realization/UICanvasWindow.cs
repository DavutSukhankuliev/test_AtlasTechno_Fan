using System;
using UnityEngine;

namespace FanComposition.UI
{
    public abstract class UICanvasWindow : MonoBehaviour, IUICanvasWindow
    {
        [SerializeField] private RectTransform _rectTransform;

        public EventHandler ShowEvent { get; set; }
        public EventHandler HideEvent { get; set; }
        public abstract void Show();
        public abstract void Hide();

        public virtual void OnShowEnd() { }
        public virtual void OnHideEnd() { }

    }
}