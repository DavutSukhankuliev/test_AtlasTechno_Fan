using UnityEngine;

namespace FanConstruction
{
    public class MotorController : AbstractController
    {
        public override void Init(HingeJoint hingeJoint)
        {
            _hingeJoint = hingeJoint;
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
    }
}