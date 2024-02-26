using UnityEngine;
using Zenject;

namespace FanComposition.CustomInput
{
    public class MouseInputSystem : ITickable
    {
        public float AxisX { get; private set; }
        public float AxisY { get; private set; }
        
        private CustomInputConfig _config;
        
        public MouseInputSystem(TickableManager tickableManager, CustomInputConfig config)
        {
            _config = config;
            tickableManager.Add(this);
        }
        
        public void Tick()
        {
            if (Input.GetMouseButton(1))
            {
                GetAxis();
                ClampAxis();
            }
        }

        private void GetAxis()
        {
            AxisX += Input.GetAxis("Mouse X") * _config.CameraMouseSensitivity;
            AxisY += Input.GetAxis("Mouse Y") * _config.CameraMouseSensitivity;
        }

        private void ClampAxis()
        {
            AxisY = Mathf.Clamp(AxisY, -90, 90);
        }
    }
}