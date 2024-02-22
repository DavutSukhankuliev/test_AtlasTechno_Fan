using System;
using UnityEngine;
using Zenject;

namespace FanConstruction
{
    public class FanView : MonoBehaviour, IDisposable
    {
        public Transform Hinge;
        public Transform Body;
        public Transform Fan;
        
        private IMemoryPool _pool;

        private void OnSpawned(IMemoryPool pool)
        {
            _pool = pool;
        }
    
        private void OnDespawned()
        {
            transform.position = Vector3.zero;
            gameObject.SetActive(false);
        }

        private void ReInit(SceneObjectProtocol protocol)
        {
            gameObject.SetActive(true);
            transform.position = protocol.Position;
        }

        public void Dispose()
        {
            _pool = null;
        }

        public class Pool : MemoryPool<SceneObjectProtocol, FanView>
        {
            protected override void Reinitialize(SceneObjectProtocol protocol, FanView item)
            {
                item.ReInit(protocol);
            }

            protected override void OnSpawned(FanView item)
            {
                base.OnSpawned(item);
                item.OnSpawned(this);
            }

            protected override void OnDespawned(FanView item)
            {
                item.OnDespawned();
                base.OnDespawned(item);
            }
        }
    }
}
