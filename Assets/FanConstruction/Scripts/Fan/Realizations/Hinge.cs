using UnityEngine;

namespace FanConstruction
{
    public abstract class Hinge : MonoBehaviour, IHinge
    {
        [SerializeField] private HingeJoint _hingeJoint;

        public HingeJoint HingeJoint => _hingeJoint;
    }
}