using System;
using System.Threading;
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
        public CancellationTokenSource CancellationTokenSource { get; set; }

        private IMemoryPool _pool;

        private void OnSpawned(IMemoryPool pool)
        {
            _pool = pool;
        }
    
        private void OnDespawned()
        {
            transform.position = Vector3.zero;
            Hinge.HingeJoint.limits = new JointLimits();
            Body.HingeJoint.limits = new JointLimits();
            Body.HingeJoint.motor = new JointMotor();
            Fan.HingeJoint.motor = new JointMotor();
            gameObject.SetActive(false);
        }

        private void ReInit(FanModel protocol, Vector3 position)
        {
            gameObject.SetActive(true);
            transform.position = position;
            Hinge.HingeJoint.limits = new JointLimits()
            {
                min = protocol.HingeMinAngularLimit,
                max = protocol.HingeMaxAngularLimit
            };
            Body.HingeJoint.limits = new JointLimits()
            {
                min = protocol.BodyMinAngularLimit,
                max = protocol.BodyMaxAngularLimit
            };
            Body.HingeJoint.motor = new JointMotor()
            {
                targetVelocity = protocol.BodyMotorTargetVelocity,
                force = protocol.BodyMotorForce,
                freeSpin = protocol.IsBodyMotorFreeSpin,
            };
            Fan.HingeJoint.motor = new JointMotor()
            {
                targetVelocity = protocol.FanMotorTargetVelocity,
                force = protocol.FanMotorForce,
                freeSpin = protocol.IsFanMotorFreeSpin,
            };
            
            CachedRotation = Body.HingeJoint.useMotor;
        }

        public void Dispose()
        {
            CancellationTokenSource.Dispose();
            CancellationTokenSource = null;
            _pool = null;
        }

        public class Pool : MemoryPool<FanModel, Vector3, FanView>
        {
            protected override void Reinitialize(FanModel protocol, Vector3 position, FanView item)
            {
                item.ReInit(protocol, position);
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
