using System;
using UnityEngine;
using Zenject;

namespace FanComposition
{
    public class FanView : MonoBehaviour, IDisposable
    {
        public HingeView Hinge;
        public HingeView Body;
        public HingeView Fan;
        
        public InputObjectView HingeInteractable;
        public InputObjectView BodyInteractable;
        public InputObjectView FanInteractable;

        public bool CachedRotation { get; set; }

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

        private void ReInit(FanModel protocol)
        {
            gameObject.SetActive(true);
            transform.position = protocol.Position;
            CachedRotation = Body.HingeJoint.useMotor;
        }

        public void Dispose()
        {
            _pool = null;
        }

        public class Pool : MemoryPool<FanModel, FanView>
        {
            protected override void Reinitialize(FanModel protocol, FanView item)
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
