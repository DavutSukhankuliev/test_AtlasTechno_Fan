using UnityEngine;

namespace FanComposition.Fan
{
    [CreateAssetMenu(fileName = "FanButtonsDescription", menuName = "Configs/FanButtonsDescription", order = 0)]
    public class FanButtonsDescription : ScriptableObject
    {
        [field: SerializeField] public string Hinge { get; private set; }
        [field: SerializeField] public string RotationButton { get; private set; }
        [field: SerializeField] public string PowerButton { get; private set; }
    }
}