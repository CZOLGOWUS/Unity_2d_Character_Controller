using UnityEngine;

namespace actorController.displace
{
    public interface IDisplace
    {
        public void AddDisplacement();
        public Vector2 GetCurrentDisplacement();
        public void ResetDisplacement();
    }
}