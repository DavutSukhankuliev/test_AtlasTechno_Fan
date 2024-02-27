using UniRx;
using UnityEngine;
using Zenject;

namespace FanComposition.CustomInput
{
    public class FanInputSystem : ITickable
    {
        public ReactiveCommand FanSpawnReactiveCommand { get; } = new ReactiveCommand();
        public ReactiveCommand<RaycastHit> FanButtonHoverInReactiveCommand { get; } = new ReactiveCommand<RaycastHit>();
        public ReactiveCommand<RaycastHit> FanButtonInteractReactiveCommand { get; } = new ReactiveCommand<RaycastHit>();
        public ReactiveCommand FanButtonHoverOutReactiveCommand { get; } = new ReactiveCommand();
        
        private const string BUTTON_LAYER_MASK = "FanButtons";
        
        private Camera _currentCamera;
        
        public FanInputSystem(TickableManager tickableManager)
        {
            tickableManager.Add(this);
            
            _currentCamera = Camera.main;
        }
        
        public void Tick()
        {
            Ray ray = _currentCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var info, 10, LayerMask.GetMask(BUTTON_LAYER_MASK)))
            {
                FanButtonHoverInReactiveCommand.Execute(info);
                
                if (Input.GetMouseButtonDown(0))
                {
                    FanButtonInteractReactiveCommand.Execute(info);
                }
            }
            else
            {
                FanButtonHoverOutReactiveCommand.Execute();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                FanSpawnReactiveCommand.Execute();
            }
        }
    }
}