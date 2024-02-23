using UnityEngine;

namespace FanComposition
{
    public class MotorController : AbstractController
    {
        public override void Init(HingeJoint hingeJoint) => _hingeJoint = hingeJoint;
        
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
    }
}