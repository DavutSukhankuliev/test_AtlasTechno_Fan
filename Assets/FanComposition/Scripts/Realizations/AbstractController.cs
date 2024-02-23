using UnityEngine;

namespace FanComposition
{
    public abstract class AbstractController : IMotorable, ISwingable, IAngleLimitable, IFixable
    {
        protected HingeJoint _hingeJoint;
        public abstract void Init(HingeJoint hingeJoint);
        public virtual void PowerOn() { }
        public virtual void PowerOff() { }
        public virtual void SetMotor(float targetVelocity, float force, bool isFreeSpin = false) { }
        public virtual void ChangeDirection() { }
        public virtual void SetAngularLimit(float min, float max) { }
        public virtual void FixJoint() { }
        public virtual void ReleaseJoint() { }
    }
}