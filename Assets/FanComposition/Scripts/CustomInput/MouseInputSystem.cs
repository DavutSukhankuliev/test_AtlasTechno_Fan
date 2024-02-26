using UnityEngine;
using Zenject;

namespace FanComposition.CustomInput
{
    public class MouseInputSystem : ITickable
    {
        public float AxisX { get; private set; }
        public float AxisY { get; private set; }
        private float _sensitivity = 1f;
        
        public MouseInputSystem(TickableManager tickableManager)
        {
            tickableManager.Add(this);
            Cursor.lockState = CursorLockMode.Locked;
        }
        
        public void Tick()
        {
            GetAxis();
            ClampAxis();
        }

        private void GetAxis()
        {
            AxisX += Input.GetAxis("Mouse X") * _sensitivity;
            AxisY += Input.GetAxis("Mouse Y") * _sensitivity;
        }

        private void ClampAxis()
        {
            AxisY = Mathf.Clamp(AxisY, -90, 90);
        }
    }
}