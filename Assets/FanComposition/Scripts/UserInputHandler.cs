using System.Threading;
using Cysharp.Threading.Tasks;
using FanComposition.Fan;
using FanComposition.UI;
using UnityEngine;
using Zenject;

namespace FanComposition
{
    public class UserInputHandler : ITickable
    {
        private const string BUTTON_LAYER_MASK = "FanButtons";
        
        private Camera _currentCamera;
        private CancellationTokenSource _ctr;

        private readonly FanController _controller;
        private readonly IUIService _uiService;
        
        public UserInputHandler(TickableManager tickableManager, FanController controller, IUIService uiService)
        {
            _controller = controller;
            _uiService = uiService;
            tickableManager.Add(this);
            
            _currentCamera = Camera.main;
        }
        
        public void Tick()
        {
            Ray ray = _currentCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var info, 10, LayerMask.GetMask(BUTTON_LAYER_MASK)))
            {
                _ctr ??= new CancellationTokenSource();
                OnHoverIn(info).Forget();
                if (Input.GetMouseButtonDown(0))
                {
                    var parent = info.collider.transform.parent;
                    parent.GetComponent<InputObjectView>().Interact.Execute();
                    OnHoverOut();
                }
            }
            else
            {
                OnHoverOut();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _controller.Spawn("StandardFan", Vector3.zero);
            }
        }

        private async UniTaskVoid OnHoverIn(RaycastHit raycastHit)
        {
            await UniTask.Delay(3000, cancellationToken: _ctr.Token);

            _uiService.Show<UIContext>().TextMeshPro.text 
                = raycastHit.collider.transform.parent.GetComponent<InputObjectView>().Description;
        }

        private void OnHoverOut()
        {
            if (_ctr == null)
            {
                return;
            }
            _uiService.Hide<UIContext>();
            
            _ctr.Cancel();
            _ctr.Dispose();
            _ctr = null;
        }
    }
}