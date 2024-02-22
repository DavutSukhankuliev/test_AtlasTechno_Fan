using UnityEngine;

namespace FanConstruction
{
    public class BodyController : AbstractController
    {
        private Rigidbody _connectedBody;
        private FixedJoint _fixedJoint;
        private bool _isFixed;
        
        public override void Init(HingeJoint hingeJoint)
        {
            _hingeJoint = hingeJoint;
            _connectedBody = _hingeJoint.connectedBody;
            _fixedJoint = _hingeJoint.GetComponent<FixedJoint>();
            _isFixed = _fixedJoint != null;
        }

        public override void TogglePower() => _hingeJoint.useMotor = !_hingeJoint.useMotor;

        public override void SetMotor(float targetVelocity, float force, bool isFreeSpin = false)
        {
            TogglePower();
            JointMotor motor = _hingeJoint.motor;
            motor.targetVelocity = targetVelocity;
            motor.force = force;
            motor.freeSpin = isFreeSpin;
            _hingeJoint.motor = motor;
            TogglePower();
        }

        public override void ChangeDirection()
        {
            JointMotor motor = _hingeJoint.motor;
            SetMotor(-motor.targetVelocity,motor.force,motor.freeSpin);
        }

        public override void SetAngularLimit(float min, float max)
        {
            _hingeJoint.useLimits = false;
            JointLimits jointLimits = _hingeJoint.limits;
            jointLimits.min = min;
            jointLimits.max = max;
            _hingeJoint.limits = jointLimits;
            _hingeJoint.useLimits = true;
        }

        public override void ToggleJoint()
        {
            // todo: strange behaviour. Unable to apply force after Fixed joint was removed while fan is off
            if (_isFixed)
            {
                Object.Destroy(_fixedJoint);
                _isFixed = false;
            }
            else
            {
                _fixedJoint = _hingeJoint.gameObject.AddComponent<FixedJoint>();
                _fixedJoint.connectedBody = _connectedBody;
                _isFixed = true;
            }
        }
    }
}