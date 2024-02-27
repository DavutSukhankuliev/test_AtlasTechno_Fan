using System.Threading;
using Cysharp.Threading.Tasks;
using FanComposition.Fan;
using FanComposition.UI;
using UniRx;
using UnityEngine;

namespace FanComposition.CustomInput
{
    public class FanInputHandler
    {
        private readonly CustomInputConfig _customInputConfig;
        private readonly FanInputSystem _fanInputSystem;
        private readonly FanController _fanController;
        private readonly IUIService _uiService;
        private readonly CompositeDisposable _disposable = new CompositeDisposable();
        
        private CancellationTokenSource _ctr;

        public FanInputHandler(CustomInputConfig customInputConfig, FanInputSystem fanInputSystem, FanController fanController, IUIService uiService)
        {
            _customInputConfig = customInputConfig;
            _fanInputSystem = fanInputSystem;
            _fanController = fanController;
            _uiService = uiService;

            _fanInputSystem.FanSpawnReactiveCommand.Subscribe(_ => OnFanSpawnMethod()).AddTo(_disposable);
            _fanInputSystem.FanButtonHoverInReactiveCommand.Subscribe(OnHoverInMethod).AddTo(_disposable);
            _fanInputSystem.FanButtonHoverOutReactiveCommand.Subscribe(_ => OnHoverOut()).AddTo(_disposable);
            _fanInputSystem.FanButtonInteractReactiveCommand.Subscribe(OnFanButtonInteractMethod).AddTo(_disposable);
        }

        private void OnFanSpawnMethod()
        {
            _fanController.Spawn("StandardFan", Vector3.zero);
        }
        
        private void OnHoverInMethod(RaycastHit raycastHit)
        {
            OnHoverIn(raycastHit).Forget();
        }
        
        private async UniTaskVoid OnHoverIn(RaycastHit raycastHit)
        {
            _ctr ??= new CancellationTokenSource();
            await UniTask.Delay((int)(_customInputConfig.DelayBeforeContextMenuShowsUpSec * 1000), cancellationToken: _ctr.Token);

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
        
        private void OnFanButtonInteractMethod(RaycastHit raycastHit)
        {
            var parent = raycastHit.collider.transform.parent;
            parent.GetComponent<InputObjectView>().Interact.Execute();
            OnHoverOut();
        }
    }
}