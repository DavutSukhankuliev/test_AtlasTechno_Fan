using System;

namespace FanComposition
{
    [Serializable]
    public struct FanModel
    {
        public string ID;
        public float HingeMinAngularLimit;
        public float HingeMaxAngularLimit;
        public float BodyMinAngularLimit;
        public float BodyMaxAngularLimit;
        public float CooldownDirectionChangeSeconds;
        public float BodyMotorTargetVelocity;
        public float BodyMotorForce;
        public bool IsBodyMotorFreeSpin;
        public float FanMotorTargetVelocity;
        public float FanMotorForce;
        public bool IsFanMotorFreeSpin;

        public FanModel(
            string id,
            float hingeMinAngularLimit, 
            float hingeMaxAngularLimit, 
            float bodyMinAngularLimit, 
            float bodyMaxAngularLimit, 
            float cooldownDirectionChangeSeconds, 
            float bodyMotorTargetVelocity, 
            float bodyMotorForce, 
            bool isBodyMotorFreeSpin, 
            float fanMotorTargetVelocity, 
            float fanMotorForce, 
            bool isFanMotorFreeSpin)
        {
            ID = id;
            HingeMinAngularLimit = hingeMinAngularLimit;
            HingeMaxAngularLimit = hingeMaxAngularLimit;
            BodyMinAngularLimit = bodyMinAngularLimit;
            BodyMaxAngularLimit = bodyMaxAngularLimit;
            CooldownDirectionChangeSeconds = cooldownDirectionChangeSeconds;
            BodyMotorTargetVelocity = bodyMotorTargetVelocity;
            BodyMotorForce = bodyMotorForce;
            IsBodyMotorFreeSpin = isBodyMotorFreeSpin;
            FanMotorTargetVelocity = fanMotorTargetVelocity;
            FanMotorForce = fanMotorForce;
            IsFanMotorFreeSpin = isFanMotorFreeSpin;
        }
    }
}