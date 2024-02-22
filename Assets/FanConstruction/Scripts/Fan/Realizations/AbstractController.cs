using UnityEngine;

namespace FanConstruction
{
    public abstract class AbstractController : IMotorController, ISwingController, IAngleLimitController, IFixedController
    {
        protected HingeJoint _hingeJoint;
        
        public abstract void Init(HingeJoint hingeJoint);
        
        public virtual void TogglePower() { }
        public virtual void SetMotor(float targetVelocity, float force, bool isFreeSpin = false) { }
        public virtual void ChangeDirection() { }
        public virtual void SetAngularLimit(float min, float max) { }
        public virtual void ToggleJoint() { }
    }
}