using UnityEngine;
using Zenject;

namespace FanComposition
{
    public class FanInputHandler : ITickable
    {
        private Camera _currentCamera;
        private readonly FanController _controller;
        
        public FanInputHandler(TickableManager tickableManager, FanController controller)
        {
            _controller = controller;
            tickableManager.Add(this);
            
            _currentCamera = Camera.main;
        }
        
        public void Tick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _currentCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var info, 10, LayerMask.GetMask("FanButtons")))
                {
                    var parent = info.collider.transform.parent;
                    parent.GetComponent<InputObjectView>().Interact.Execute();
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _controller.Spawn("StandardFan", Vector3.zero);
            }
        }
    }
}