using UnityEngine;

namespace FanConstruction
{
    public class FanController
    {
        private readonly HingeController _hinge;
        private readonly BodyController _body;
        private readonly MotorController _fan;

        public FanController(HingeController hinge, BodyController body, MotorController fan)
        {
            _hinge = hinge;
            _body = body;
            _fan = fan;
        }

        public void Init(FanView view)
        {
            _hinge.Init(view.Hinge.GetComponent<HingeJoint>());
            _body.Init(view.Body.GetComponent<HingeJoint>());
            _fan.Init(view.Fan.GetComponent<HingeJoint>());
        }

        public void TogglePower<T>() where T : AbstractController
        {
            if (typeof(T) == typeof(HingeController))
            {
                _hinge.TogglePower();
            }
            else if (typeof(T) == typeof(BodyController))
            {
                _body.TogglePower();
            }
            else if (typeof(T) == typeof(MotorController))
            {
                _fan.TogglePower();
            }
            else
            {
                Debug.LogWarning("Unsupported controller type.");
            }
        }
        
        public void SetMotor<T>(float targetVelocity, float force, bool isFreeSpin = false) where T : AbstractController
        {
            if (typeof(T) == typeof(HingeController))
            {
                _hinge.SetMotor(targetVelocity, force, isFreeSpin);
            }
            else if (typeof(T) == typeof(BodyController))
            {
                _body.SetMotor(targetVelocity, force, isFreeSpin);
            }
            else if (typeof(T) == typeof(MotorController))
            {
                _fan.SetMotor(targetVelocity, force, isFreeSpin);
            }
            else
            {
                Debug.LogWarning("Unsupported controller type.");
            }
        }

        public void SetAngularLimit<T>(float min, float max) where T : AbstractController
        {
            if (typeof(T) == typeof(HingeController))
            {
                _hinge.SetAngularLimit(min, max);
            }
            else if (typeof(T) == typeof(BodyController))
            {
                _body.SetAngularLimit(min, max);
            }
            else if (typeof(T) == typeof(MotorController))
            {
                _fan.SetAngularLimit(min, max);
            }
            else
            {
                Debug.LogWarning("Unsupported controller type.");
            }
        }
    }
}