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
            //spawn
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _fanView = _spawner.Spawn(Vector3.zero);
                _controller.Init(_fanView);
            }

            //fan
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _controller.TogglePower<MotorController>();
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                _controller.SetMotor<MotorController>(1000, 2000, true);
            }
            
            //body
            if (Input.GetKeyDown(KeyCode.A))
            {
                _controller.TogglePower<BodyController>();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                _controller.SetMotor<BodyController>(10,10);
            }
            
            if (Input.GetKeyDown(KeyCode.D))
            {
                _controller.ChangeDirection<BodyController>();
            }
            
            if (Input.GetKeyDown(KeyCode.F))
            {
                _controller.SetAngularLimit<BodyController>(-45,45);
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                _controller.ToggleJoint<BodyController>();
            }
            
            //hinge
            if (Input.GetKeyDown(KeyCode.Z))
            {
                _controller.SetAngularLimit<HingeController>(-20,5);
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                _controller.ToggleJoint<HingeController>();
            }
        }
    }
}