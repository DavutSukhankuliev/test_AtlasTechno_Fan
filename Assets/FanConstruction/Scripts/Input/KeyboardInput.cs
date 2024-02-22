using UnityEngine;
using Zenject;

namespace FanConstruction
{
    public class KeyboardInput : ITickable
    {
        private readonly FanSpawner _spawner;
        private readonly FanController _controller;

        private FanView _fanView;

        public KeyboardInput(FanSpawner spawner, FanController controller)
        {
            _spawner = spawner;
            _controller = controller;
        }

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _fanView = _spawner.Spawn(Vector3.zero);
                _controller.Init(_fanView);
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                _controller.TogglePower<MotorController>();
            }
        }
    }
}