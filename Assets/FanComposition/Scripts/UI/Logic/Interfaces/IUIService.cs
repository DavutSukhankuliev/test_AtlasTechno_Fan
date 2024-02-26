using UnityEngine;

namespace FanComposition.UI
{
    public interface IUIService
    {
        T Show<T>() where T : UICanvasWindow;
        void Hide<T>() where T : UICanvasWindow;
        T Get<T>() where T : UICanvasWindow;

        void InitWindows(Transform poolInactiveContainer);
        void LoadWindows();
    }
}