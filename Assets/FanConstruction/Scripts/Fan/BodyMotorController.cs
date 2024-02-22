using Unity.VisualScripting;
using UnityEngine;

namespace FanConstruction
{
    public class BodyMotorController
    {
        private BodyMotorView _view;
        private Rigidbody _connectedBody;
        private bool _isFixed;

        public BodyMotorController(BodyMotorView view)
        {
            _view = view;
            _connectedBody = _view.HingeJoint.connectedBody;
        }


        public void TogglePower() => _view.HingeJoint.useMotor = !_view.HingeJoint.useMotor;

        public void SetMotor(float targetVelocity, float force, bool isFreeSpin = false)
        {
            _view.HingeJoint.useMotor = false;
            _view.HingeJoint.motor = new JointMotor()
            {
                targetVelocity = targetVelocity,
                force = force,
                freeSpin = isFreeSpin
            };
        }

        public void ChangeDirection()
        {
            var motor = _view.HingeJoint.motor;
            SetMotor(-motor.targetVelocity,motor.force,motor.freeSpin);
        }

        public void SetAngularLimit(float min, float max)
        {
            _view.HingeJoint.limits = new JointLimits()
            {
                min = min,
                max = max
            };
            _view.HingeJoint.useLimits = true;
        }

        public void ToggleJoint()
        {
            if (_isFixed)
            {
                Object.Destroy(_view.FixedJoint);
                _isFixed = false;
            }
            else
            {
                _view.FixedJoint = _view.AddComponent<FixedJoint>();
                _view.FixedJoint.connectedBody = _connectedBody;
                _isFixed = true;
            }
        }
    }
}