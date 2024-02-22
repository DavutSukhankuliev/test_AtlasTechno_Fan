using UnityEngine;

namespace FanConstruction
{
    public class MotorController : AbstractController
    {
        public void Init(HingeJoint hingeJoint)
        {
            _hingeJoint = hingeJoint;
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
    }
}