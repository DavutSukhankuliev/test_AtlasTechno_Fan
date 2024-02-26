using System;
using System.Threading;
using UnityEngine;
using Zenject;

namespace FanComposition.Fan
{
    public class FanView : MonoBehaviour, IDisposable
    {
        [field: SerializeField] public HingeView Hinge { get; private set; }
        [field: SerializeField] public HingeView Body { get; private set; }
        [field: SerializeField] public HingeView Fan { get; private set; }
        
        [field: SerializeField] public InputObjectView HingeInteractable { get; private set; }
        [field: SerializeField] public InputObjectView BodyInteractable { get; private set; }
        [field: SerializeField] public InputObjectView FanInteractable { get; private set; }

        public bool CachedRotation { get; set; }
        public CancellationTokenSource CancellationTokenSource { get; set; }
        
        private IMemoryPool _pool;

        public void Dispose()
        {
            CancellationTokenSource.Dispose();
            CancellationTokenSource = null;
            _pool = null;
        }
        
        private void OnSpawned(IMemoryPool pool)
        {
            _pool = pool;
        }
    
        private void OnDespawned()
        {
            transform.position = Vector3.zero;
            ResetFanParameters();
            gameObject.SetActive(false);
        }
        
        private void ReInit(FanModel protocol, Vector3 position)
        {
            gameObject.SetActive(true);
            transform.position = position;

            SetFanParameters(protocol);

            CachedRotation = Body.HingeJoint.useMotor;
        }

        private void SetFanParameters(FanModel protocol)
        {
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
        }
        
        private void ResetFanParameters()
        {
            Hinge.HingeJoint.limits = new JointLimits();
            Body.HingeJoint.limits = new JointLimits();
            Body.HingeJoint.motor = new JointMotor();
            Fan.HingeJoint.motor = new JointMotor();
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
