using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace actorController.collsion
{
    public enum Axis
    {
        horizontal = 1,
        vertical
    }

    public struct Direction
    {
        public short horizontal;
        public short vertical;
    }

    public struct BoxCastOrigin
    {
        public Vector2 center;
        public float
            centerLeftDistance,
            centerRightDistance,
            centerTopDistance,
            centerBottomDistance;
    }

    public struct CollisionInfo
    {
        public RaycastHit2D[] horizontal, vertical;
        public Direction direction;
        public float skinWidth;
        public void Reset()
        {
            horizontal = new RaycastHit2D[3];
            vertical = new RaycastHit2D[3];
            direction.horizontal = 0;
            direction.vertical = 0;
        }
    }
}