using UnityEngine;

namespace FanConstruction
{
    public class BodyController : AbstractController
    {
        public void Init(HingeJoint hingeJoint)
        {
            _hingeJoint = hingeJoint;
            _connectedBody = _hingeJoint.connectedBody;
            _fixedJoint = _hingeJoint.GetComponent<FixedJoint>();
            _isFixed = _fixedJoint != null;
        }

        public override void TogglePower() => _hingeJoint.useMotor = !_hingeJoint.useMotor;

        public override void SetMotor(float targetVelocity, float force, bool isFreeSpin = false)
        {
            _hingeJoint.motor = new JointMotor
            {
                targetVelocity = targetVelocity,
                force = force,
                freeSpin = isFreeSpin
            };
        }

        public override void ToggleJoint()
        {
            if (_isFixed)
            {
                Object.Destroy(_fixedJoint);
                _isFixed = false;
            }
            else
            {
                _fixedJoint = _hingeJoint.gameObject.AddComponent<FixedJoint>(); // Нужно сделать unirx с передачей компонента
                _fixedJoint.connectedBody = _connectedBody;
                _isFixed = true;
            }
        }

        public override void ChangeDirection()
        {
            var motor = _hingeJoint.motor;
            SetMotor(-motor.targetVelocity,motor.force,motor.freeSpin);
        }

        public override void SetAngularLimit(float min, float max)
        {
            _hingeJoint.limits = new JointLimits
            {
                min = min,
                max = max
            };
            _hingeJoint.useLimits = true;
        }
    }
}