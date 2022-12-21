using actorController.controller;
using Mono.Cecil;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace actorController.collsion
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class CollisionDetection : MonoBehaviour
    {
        private BoxCollider2D coll;

        [SerializeField] private LayerMask terrain;
        [SerializeField] private float skinWidth = 0.015f;

        private BoxCastOrigin boxCastOrigin = new BoxCastOrigin();
        private CollisionInfo collisionInfo = new CollisionInfo();

        public BoxCastOrigin BoxCastOrigin { get => boxCastOrigin; private set => boxCastOrigin = value; }
        public CollisionInfo CollisionInfo { get => collisionInfo; set => collisionInfo = value; }

        private void OnEnable()
        {
            coll = GetComponent<BoxCollider2D>();

            collisionInfo.skinWidth = this.skinWidth;

            collisionInfo.Reset();
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

        public void ApplyCollision(ref Vector2 velocity)
        {
            collisionInfo.Reset();
            UpdateCastOrigin();

            UpdateCharacterDirection(ref velocity);
            ApplyHorizontalCollision(ref velocity);
            ApplyVerticalCollision(ref velocity);
        }


        private void UpdateCharacterDirection(ref Vector2 velocity)
        {
            collisionInfo.direction.vertical = (short)Math.Sign(velocity.y);
            collisionInfo.direction.horizontal = (short)Math.Sign(velocity.x);
        }

        public void ApplyHorizontalCollision(ref Vector2 velocity)
        {
            GatherHorizontalCollisionInfo(velocity);

            if (!IsCollidingHorizontaly())
                return;

            velocity = SnapToHorizontalCollisionHitPoint(velocity);
        }

        private void GatherHorizontalCollisionInfo(Vector2 velocity)
        {
            Vector2 boxcastSize = new Vector2(
                skinWidth,
                coll.bounds.size.y - 2 * skinWidth);
            float castDistance = coll.bounds.size.x + Mathf.Abs(velocity.x) + skinWidth;

            Physics2D.BoxCastNonAlloc(
                boxCastOrigin.center + (collisionInfo.direction.horizontal * Vector2.left) * coll.bounds.extents.x,
                boxcastSize,
                0f,
                Vector2.right * collisionInfo.direction.horizontal,
                collisionInfo.horizontal,
                castDistance,
                terrain);
        }

        private bool IsCollidingHorizontaly()
        {
            return collisionInfo.horizontal[0].collider != null;
        }

        private Vector2 SnapToHorizontalCollisionHitPoint(Vector2 velocity)
        {
            RaycastHit2D closestHit = CalculateClosestHit(collisionInfo.horizontal);

            velocity.x = transform.InverseTransformPoint(closestHit.point).x - (coll.bounds.extents.x + skinWidth) * collisionInfo.direction.horizontal;
            return velocity;
        }

        private void ApplyVerticalCollision(ref Vector2 velocity)
        {
            GatherVerticalCollisionInfo(velocity);

            if (!IsCollidingVerticly())
                return;

            velocity = SnapToVerticalCollisionHitPoint(velocity);
        }

        private void GatherVerticalCollisionInfo(Vector2 velocity)
        {
            Vector2 boxcastSize = new Vector2(
                coll.bounds.size.x - 2f * skinWidth,
                skinWidth);
            float castDistance = (coll.bounds.size.y) + Mathf.Abs(velocity.y) + skinWidth;

            Physics2D.BoxCastNonAlloc(
                boxCastOrigin.center + (collisionInfo.direction.vertical * Vector2.down) * (Vector2)coll.bounds.extents,
                boxcastSize,
                0f,
                Vector2.up * collisionInfo.direction.vertical,
                collisionInfo.vertical,
                castDistance,
                terrain
                );
        }

        private Vector2 SnapToVerticalCollisionHitPoint(Vector2 velocity)
        {
            RaycastHit2D closestHit = CalculateClosestHit(collisionInfo.vertical);

            velocity.y = transform.InverseTransformPoint(closestHit.point).y + (coll.bounds.extents.y + skinWidth) * (-collisionInfo.direction.vertical);
            return velocity;
        }

        private bool IsCollidingVerticly()
        {
            return collisionInfo.vertical[0].collider != null;
        }

        private RaycastHit2D CalculateClosestHit(RaycastHit2D[] hits)
        {
            RaycastHit2D closestHit = hits[0];
            for (short i = 1; i < hits.Length; i++)
            {
                if (hits[i].collider)
                    closestHit = hits[i].distance < closestHit.distance ? hits[i] : closestHit;
            }

            return closestHit;
        }

    }
}
