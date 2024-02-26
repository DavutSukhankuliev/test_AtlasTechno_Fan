using UnityEngine;
using Zenject;

namespace FanComposition.CustomInput
{
    public class KeyboardInputSystem : ITickable
    {
        public Vector3 MoveDirection { get; private set; }
        
        private CustomInputConfig _config;
        
        public KeyboardInputSystem(TickableManager tickableManager, CustomInputConfig config)
        {
            _config = config;
            tickableManager.Add(this);
        }
        
        public void Tick()
        {
            MoveDirection = Vector3.zero;
            
            if (Input.GetKey(KeyCode.UpArrow))
            {
                MoveDirection = Vector3.forward * _config.CameraMovementVelocity;
            }
            
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                MoveDirection = Vector3.left * _config.CameraMovementVelocity;
            }
            
            if (Input.GetKey(KeyCode.RightArrow))
            {
                MoveDirection = Vector3.right * _config.CameraMovementVelocity;
            }
            
            if (Input.GetKey(KeyCode.DownArrow))
            {
                MoveDirection = Vector3.back * _config.CameraMovementVelocity;
            }
        }
    }
}