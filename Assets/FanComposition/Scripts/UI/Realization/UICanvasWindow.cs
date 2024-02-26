using UnityEngine;

namespace FanComposition.UI
{
    public abstract class UICanvasWindow : MonoBehaviour, IUICanvasWindow
    {
        [SerializeField] private RectTransform _rectTransform;

        public abstract void Show();
        public abstract void Hide();
    }
}