using UnityEngine;

namespace FanComposition
{
    public class HingeController : AbstractController
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

        public override void SetAngularLimit(float min, float max)
        {
            _hingeJoint.useLimits = false;
            JointLimits jointLimits = _hingeJoint.limits;
            jointLimits.min = min;
            jointLimits.max = max;
            _hingeJoint.limits = jointLimits;
            _hingeJoint.useLimits = true;
        }
    }
}