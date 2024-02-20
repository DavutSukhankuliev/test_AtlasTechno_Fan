using UnityEngine;

namespace FanConstruction.Fan
{
    public class FanController
    {
        private FanView.Pool _pool;

        public FanController(FanView.Pool pool)
        {
            _pool = pool;
        }

        public void Spawn(Vector3 position)
        {
            var protocol = new SceneObjectProtocol(position);
            var fan = _pool.Spawn(protocol);
        }

        public void Despawn(FanView view)
        {
            _pool.Despawn(view);
        }
    }
}