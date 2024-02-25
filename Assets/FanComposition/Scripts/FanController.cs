using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace FanComposition
{
    public class FanController
    {
        private CompositeDisposable _disposable = new CompositeDisposable();
        private FanView.Pool _pool;
        private List<FanView> _view = new List<FanView>();

        public FanController(FanView.Pool pool)
        {
            _pool = pool;
        }

        public FanView Spawn(Vector3 position)
        {
            var view = _pool.Spawn(new FanModel(position));
            _view.Add(view);
            FanSubscriptionMethod(view);
            return view;
        }

        public void Despawn(FanView view)
        {
            _pool.Despawn(view);
            _view.Remove(view);
        }

        private void FanSubscriptionMethod(FanView view)
        {
            view.HingeInteractable.Interact.Subscribe(_ => OnHingePressedEvent(view)).AddTo(_disposable);
            view.BodyInteractable.Interact.Subscribe(_ => OnRotationPressedEvent(view)).AddTo(_disposable);
            view.FanInteractable.Interact.Subscribe(_ => OnPowerPressedEvent(view)).AddTo(_disposable);
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
                view.Body.HingeJoint.useMotor = true;
                Object.Destroy(view.Body.FixedJoint);
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
    }
}