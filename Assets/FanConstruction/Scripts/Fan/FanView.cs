using System;
using UnityEngine;
using Zenject;

namespace FanConstruction.Fan
{
    public class FanView : MonoBehaviour, IDisposable
    {
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
                item.OnDespawned();
                base.OnSpawned(item);
            }

            protected override void OnDespawned(FanView item)
            {
                base.OnDespawned(item);
                item.OnSpawned(this);
            }
        }
    }
}
