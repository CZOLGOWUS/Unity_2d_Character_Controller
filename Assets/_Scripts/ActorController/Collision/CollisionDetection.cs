using actorController.controller;
using UnityEngine;


namespace actorController.collsion
{
    [RequireComponent(typeof(ActorController), typeof(Collider2D))]
    public class CollisionDetection : MonoBehaviour
    {
        private Collider2D coll;

        [SerializeField] private LayerMask terrain;
        [SerializeField] private float skinWidth = 0.015f;

        private BoxCastOrigin boxCastOrigin = new BoxCastOrigin();
        private CollisionInfo collisionInfo = new CollisionInfo();

        public BoxCastOrigin BoxCastOrigin { get => boxCastOrigin; private set => boxCastOrigin = value; }
        public CollisionInfo CollisionInfo { get => collisionInfo; set => collisionInfo = value; }

        private void OnEnable()
        {
            coll = GetComponent<Collider2D>();
        }

        public void ApplyCollision(Vector2 velocity)
        {
            UpdateCastOrigin();
            ApplyHorizontalCollision(velocity);

        }

        public void ApplyHorizontalCollision(Vector2 velocity)
        {

        }

        public void ApplyVerticalCollision(Vector2 velocity)
        {

        }

        public void UpdateCastOrigin()
        {
            Bounds collBounds = coll.bounds;

            boxCastOrigin.center = collBounds.center;
            boxCastOrigin.centerLeftDistance = collBounds.center.x - collBounds.min.x;
            boxCastOrigin.centerRightDistance = collBounds.max.x - collBounds.center.x;
            boxCastOrigin.centerTopDistance = collBounds.max.y - collBounds.center.y;
            boxCastOrigin.centerBottomDistance = collBounds.center.y - collBounds.min.y;
        }

    }

    internal enum Direction
    {
        up = 1,
        down = -1
    }
}
