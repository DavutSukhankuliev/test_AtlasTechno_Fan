using UnityEngine;

namespace FanComposition
{
    public struct SceneObjectProtocol
    {
        public Vector3 Position;

        public SceneObjectProtocol(
            Vector3 position = new Vector3())
        {
            Position = position;
        }
    }
}