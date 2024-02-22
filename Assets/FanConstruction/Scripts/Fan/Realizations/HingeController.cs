using UnityEngine;

namespace FanConstruction
{
    public class HingeController : AbstractController
    {
        public void Init(HingeJoint hingeJoint)
        {
            _hingeJoint = hingeJoint;
            _connectedBody = _hingeJoint.connectedBody;
            _fixedJoint = _hingeJoint.GetComponent<FixedJoint>();
            _isFixed = _fixedJoint != null;
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
    }
}