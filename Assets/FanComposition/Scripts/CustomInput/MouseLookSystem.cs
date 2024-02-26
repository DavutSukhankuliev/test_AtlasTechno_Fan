using UnityEngine;
using Zenject;

namespace FanComposition.CustomInput
{
    public class MouseLookSystem : ITickable
    {
        private readonly MouseInputSystem _mouseInputSystem;
        
        private Camera _currentCamera; 

        public MouseLookSystem(MouseInputSystem mouseInputSystem, TickableManager tickableManager)
        {
            _mouseInputSystem = mouseInputSystem;
            tickableManager.Add(this);
            _currentCamera = Camera.main;
        }

        public void Tick()
        {
            ApplyMouseInput();
        }

        private void ApplyMouseInput()
        {
            if (_currentCamera != null)
            {
                _currentCamera.transform.localRotation =
                    Quaternion.Euler(-_mouseInputSystem.AxisY, _mouseInputSystem.AxisX, 0);
            }
        }
    }
}