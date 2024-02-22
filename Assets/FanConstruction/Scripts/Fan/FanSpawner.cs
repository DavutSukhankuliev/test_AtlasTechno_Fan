using UnityEngine;

namespace FanConstruction
{
    public class FanSpawner
    {
        private FanView.Pool _pool;
        
        public FanSpawner(FanView.Pool pool)
        {
            _pool = pool;
        }

        public void Spawn(Vector3 position)
        {
            var protocol = new SceneObjectProtocol(position);
            _pool.Spawn(protocol);
        }

        public void Despawn(FanView view)
        {
            _pool.Despawn(view);
        }
    }
}