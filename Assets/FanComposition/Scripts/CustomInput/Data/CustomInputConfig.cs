using UnityEngine;

namespace FanComposition.CustomInput
{
    [CreateAssetMenu(fileName = "CustomInputConfig", menuName = "Configs/CustomInputConfig", order = 0)]
    public class CustomInputConfig : ScriptableObject
    {
        [field: SerializeField, Range(0.1f,3)] public float CameraMouseSensitivity { get; private set; }
        [field: SerializeField, Range(0.1f,5)] public float CameraMovementVelocity { get; private set; }
    }
}