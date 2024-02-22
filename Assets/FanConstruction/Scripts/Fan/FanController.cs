using System;
using System.Collections.Generic;
using UnityEngine;

namespace FanConstruction
{
    public class FanController
    {
        private readonly Dictionary<Type, AbstractController> _controllers = new Dictionary<Type, AbstractController>();

        public FanController(
            HingeController hinge, 
            BodyController body, 
            MotorController fan)
        {
            _controllers[typeof(HingeController)] = hinge;
            _controllers[typeof(BodyController)] = body;
            _controllers[typeof(MotorController)] = fan;
        }

        public void Init(FanView view)
        {
            HingeJoint hingeJoint = view.Hinge.GetComponent<HingeJoint>();
            HingeJoint bodyJoint = view.Body.GetComponent<HingeJoint>();
            HingeJoint fanJoint = view.Fan.GetComponent<HingeJoint>();
            
            if (_controllers.ContainsKey(typeof(HingeController)))
            {
                _controllers[typeof(HingeController)].Init(hingeJoint);
            }

            if (_controllers.ContainsKey(typeof(BodyController)))
            {
                _controllers[typeof(BodyController)].Init(bodyJoint);
            }

            if (_controllers.ContainsKey(typeof(MotorController)))
            {
                _controllers[typeof(MotorController)].Init(fanJoint);
            }
        }

        public void TogglePower<T>() where T : AbstractController
        {
            Type controllerType = typeof(T);

            if (_controllers.ContainsKey(controllerType))
            {
                _controllers[controllerType].TogglePower();
            }
            else
            {
                Debug.LogWarning("Unsupported controller type.");
            }
        }
        
        public void SetMotor<T>(float targetVelocity, float force, bool isFreeSpin = false) where T : AbstractController
        {
            Type controllerType = typeof(T);

            if (_controllers.ContainsKey(controllerType))
            {
                _controllers[controllerType].SetMotor(targetVelocity, force, isFreeSpin);
            }
            else
            {
                Debug.LogWarning("Unsupported controller type.");
            }
        }

        public void ChangeDirection<T>() where T : AbstractController
        {
            Type controllerType = typeof(T);
            
            if (_controllers.ContainsKey(controllerType))
            {
                _controllers[controllerType].ChangeDirection();
            }
            else
            {
                Debug.LogWarning("Unsupported controller type.");
            }
        }

        public void SetAngularLimit<T>(float min, float max) where T : AbstractController
        {
            Type controllerType = typeof(T);

            if (_controllers.ContainsKey(controllerType))
            {
                _controllers[controllerType].SetAngularLimit(min, max);
            }
            else
            {
                Debug.LogWarning("Unsupported controller type.");
            }
        }
        
        public void ToggleJoint<T>() where T : AbstractController
        {
            Type controllerType = typeof(T);

            if (_controllers.ContainsKey(controllerType))
            {
                _controllers[controllerType].ToggleJoint();
            }
            else
            {
                Debug.LogWarning("Unsupported controller type.");
            }
        }
    }
}