using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace FanComposition.Fan
{
    public class FanController
    {
        private FanView.Pool _pool;
        private List<FanView> _view = new List<FanView>();
        
        private readonly CompositeDisposable _disposable = new CompositeDisposable();
        private readonly FanConfig _fanConfig;
        private readonly FanButtonsDescription _description;

        public FanController(FanView.Pool pool, FanConfig fanConfig, FanButtonsDescription description)
        {
            _pool = pool;
            _fanConfig = fanConfig;
            _description = description;
        }

        public void Spawn(string fanId, Vector3 position)
        {
            var view = _pool.Spawn(_fanConfig.Get(fanId), position);
            _view.Add(view);
            FanSubscriptionMethod(view, _fanConfig.Get(fanId).CooldownDirectionChangeSeconds);
        }

        public void Despawn(FanView view)
        {
            view.CancellationTokenSource.Cancel();
            _pool.Despawn(view);
            _view.Remove(view);
        }

        private void FanSubscriptionMethod(FanView view, float cooldownInSeconds)
        {
            view.HingeInteractable.Interact.Subscribe(_ => OnHingePressedEvent(view)).AddTo(_disposable);
            view.HingeInteractable.Description = _description.Hinge;
            view.BodyInteractable.Interact.Subscribe(_ => OnRotationPressedEvent(view)).AddTo(_disposable);
            view.BodyInteractable.Description = _description.RotationButton;
            view.FanInteractable.Interact.Subscribe(_ => OnPowerPressedEvent(view)).AddTo(_disposable);
            view.FanInteractable.Description = _description.PowerButton;
            
            view.CancellationTokenSource ??= new CancellationTokenSource();
            ChangeDirectionAsyncCycle(view, (int)(cooldownInSeconds * 1000), view.CancellationTokenSource.Token).Forget();
        }
        private void OnHingePressedEvent(FanView view)
        {
            if (view.Hinge.FixedJoint == null)
            {
                view.Hinge.FixedJoint = view.Hinge.gameObject.AddComponent<FixedJoint>();
                view.Hinge.FixedJoint.connectedBody = view.Hinge.HingeJoint.connectedBody;
            }
            else
            {
                Object.Destroy(view.Hinge.FixedJoint);
            }
        }
        
        private void OnRotationPressedEvent(FanView view)
        {
            if (!view.Fan.HingeJoint.useMotor)
            {
                view.CachedRotation = !view.CachedRotation;
                return;
            }

            if (view.CachedRotation)
            {
                view.Body.HingeJoint.useMotor = false;
                view.Body.FixedJoint = view.Body.gameObject.AddComponent<FixedJoint>();
                view.Body.FixedJoint.connectedBody = view.Body.HingeJoint.connectedBody;
                view.CachedRotation = false;
            }
            else
            {
                Object.Destroy(view.Body.FixedJoint);
                view.Body.HingeJoint.useMotor = true;
                view.CachedRotation = true;
            }
        }
        
        private void OnPowerPressedEvent(FanView view)
        {
            view.Fan.HingeJoint.useMotor = !view.Fan.HingeJoint.useMotor;

            if (!view.CachedRotation)
            {
                return;
            }
            
            if (view.Fan.HingeJoint.useMotor)
            {
                Object.Destroy(view.Body.FixedJoint);
                view.Body.HingeJoint.useMotor = true;
            }
            else
            {
                view.Body.FixedJoint = view.Body.gameObject.AddComponent<FixedJoint>();
                view.Body.FixedJoint.connectedBody = view.Body.HingeJoint.connectedBody;
            }
        }

        private async UniTaskVoid ChangeDirectionAsyncCycle(FanView view, int millisecondsStable, CancellationToken cancellationToken = default)
        {
            HingeJoint bodyHingeJoint = view.Body.HingeJoint;
            while (true)
            {
                if (!bodyHingeJoint)
                {
                    return;
                }
                
                if (bodyHingeJoint.angle >= bodyHingeJoint.limits.max
                    || bodyHingeJoint.angle <= bodyHingeJoint.limits.min)
                {
                    await UniTask.Delay(millisecondsStable, cancellationToken: cancellationToken);
                    ChangeDirection(bodyHingeJoint);
                    await UniTask.Delay(millisecondsStable, cancellationToken: cancellationToken);
                }

                await UniTask.Yield(cancellationToken: cancellationToken);
            }
        }

        private void ChangeDirection(HingeJoint joint)
        {
            JointMotor motor = joint.motor;
            SetMotor(joint, -motor.targetVelocity, motor.force, motor.freeSpin);
        }
        
        private void SetMotor(HingeJoint joint, float targetVelocity, float force, bool isFreeSpin = false)
        {
            JointMotor motor = joint.motor;
            motor.targetVelocity = targetVelocity;
            motor.force = force;
            motor.freeSpin = isFreeSpin;
            joint.motor = motor;
        }
    }
}