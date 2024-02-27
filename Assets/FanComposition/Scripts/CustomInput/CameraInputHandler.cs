using UnityEngine;
using Zenject;

namespace FanComposition.CustomInput
{
    public class CameraInputHandler : ITickable
    {
        private readonly MouseInputSystem _mouseInputSystem;
        private readonly KeyboardInputSystem _keyboardInputSystem;
        
        private Camera _currentCamera; 

        public CameraInputHandler(MouseInputSystem mouseInputSystem, KeyboardInputSystem keyboardInputSystem, TickableManager tickableManager)
        {
            _mouseInputSystem = mouseInputSystem;
            _keyboardInputSystem = keyboardInputSystem;
            tickableManager.Add(this);
            _currentCamera = Camera.main;
        }

        public void Tick()
        {
            ApplyMouseInput();
            ApplyKeyboardInput();
        }

        private void ApplyMouseInput()
        {
            if (_currentCamera != null)
            {
                _currentCamera.transform.localRotation =
                    Quaternion.Euler(-_mouseInputSystem.AxisY, _mouseInputSystem.AxisX, 0);
            }
        }

        private void ApplyKeyboardInput()
        {
            if (_currentCamera != null)
            {
                var moveDirection 
                    = _currentCamera.transform.right * _keyboardInputSystem.MoveDirection.x 
                    + _currentCamera.transform.up * _keyboardInputSystem.MoveDirection.y
                    + _currentCamera.transform.forward * _keyboardInputSystem.MoveDirection.z;
                
                _currentCamera.transform.localPosition += moveDirection * Time.deltaTime;
                
            }
        }
    }
}