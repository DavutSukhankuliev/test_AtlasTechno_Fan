using FanConstruction.Fan;
using UnityEngine;
using Zenject;

namespace FanConstruction.Input
{
    public class KeyboardInput : ITickable
    {
        private readonly FanController _fan;

        public KeyboardInput(FanController fan)
        {
            _fan = fan;
        }

        public void Tick()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.A))
            {
                _fan.Spawn(Vector3.zero);
            }
        }
    }
}