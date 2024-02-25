using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace FanComposition
{
    public class FanController
    {
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
            FanUnSubscriptionMethod(view);
            _pool.Despawn(view);
            _view.Remove(view);
        }

        private void FanSubscriptionMethod(FanView view)
        {
            view.HingeInteractable.Interacted.Subscribe(_ => OnHingePressedMethod(view));
            view.BodyInteractable.Interacted.Subscribe(_ => OnRotationPressedEvent(view));
            view.FanInteractable.Interacted.Subscribe(_ => OnPowerPressedEvent(view));
        }

        private void FanUnSubscriptionMethod(FanView view)
        {
            
        }
        
        private void OnHingePressedMethod(FanView view)
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
        
        private void OnPowerPressedEvent(FanView view)
        {
            view.Fan.HingeJoint.useMotor = !view.Fan.HingeJoint.useMotor;
            OnRotationPressedEvent(view);
        }

        private void OnRotationPressedEvent(FanView view)
        {
            view.Body.HingeJoint.useMotor = !view.Body.HingeJoint.useMotor;
            OnHingePressedMethod(view);
        }
    }
}