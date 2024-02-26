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
            var container = _uIRoot.PoolContainer.GetComponent<RectTransform>();
            container.localScale = Vector3.one;
            container.anchorMin = Vector2.zero;
            container.anchorMax = Vector2.one;
            container.pivot = new Vector2(0.5f, 0.5f);
            container.offsetMin = Vector2.zero;
            container.offsetMax = Vector2.zero;

            container.gameObject.SetActive(false);

            _uIService.InitWindows(container);
        }
    }
}