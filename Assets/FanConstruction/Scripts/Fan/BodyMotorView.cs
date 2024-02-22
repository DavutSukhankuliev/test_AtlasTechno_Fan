using UnityEngine;

namespace FanConstruction
{
    public class BodyMotorView : Hinge, IFixed
    {
        [SerializeField] private FixedJoint _fixedJoint;
        
        public FixedJoint FixedJoint
        {
            get => _fixedJoint;
            set => _fixedJoint = value;
        }
        
        private BodyMotorView() { }
    }
}