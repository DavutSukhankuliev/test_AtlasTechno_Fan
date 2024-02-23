using UnityEngine;

namespace FanComposition
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

        public override void PowerOn() => _hingeJoint.useMotor = true;
        
        public override void PowerOff() => _hingeJoint.useMotor = false;

        public override void SetMotor(float targetVelocity, float force, bool isFreeSpin = false)
        {
            JointMotor motor = _hingeJoint.motor;
            motor.targetVelocity = targetVelocity;
            motor.force = force;
            motor.freeSpin = isFreeSpin;
            _hingeJoint.motor = motor;
        }

        public override void ChangeDirection()
        {
            JointMotor motor = _hingeJoint.motor;
            SetMotor(-motor.targetVelocity, motor.force, motor.freeSpin);
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

        public override void FixJoint()
        {
            if (_isFixed)
                return;

            _fixedJoint = _hingeJoint.gameObject.AddComponent<FixedJoint>();
            _fixedJoint.connectedBody = _connectedBody;
            _isFixed = true;
        }

        public override void ReleaseJoint()
        {
            if (!_isFixed)
                return;

            Object.Destroy(_fixedJoint);
            _isFixed = false;
        }
    }
}