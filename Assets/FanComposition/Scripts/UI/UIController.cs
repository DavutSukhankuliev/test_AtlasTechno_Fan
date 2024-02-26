using UnityEngine;

namespace FanComposition.UI
{
    public class UIController
    {
        private readonly IUIRoot _uIRoot;
        private readonly IUIService _uIService;

        public UIController(IUIRoot uIRoot, IUIService uIService)
        {
            _uIRoot = uIRoot;
            _uIService = uIService;
            
            Init();
        }

        public void Init()
        {
            _uIService.LoadWindows();
            var container = _uIRoot.PoolContainer.gameObject;
            var containerRect = container.AddComponent<RectTransform>();
            containerRect.localScale = Vector3.one;
            containerRect.anchorMin = Vector2.zero;
            containerRect.anchorMax = Vector2.one;
            containerRect.pivot = new Vector2(0.5f, 0.5f);
            containerRect.offsetMin = Vector2.zero;
            containerRect.offsetMax = Vector2.zero;

            container.gameObject.SetActive(false);

            _uIService.InitWindows(containerRect);
        }
    }
}