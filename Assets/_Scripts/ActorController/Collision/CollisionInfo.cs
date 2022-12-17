using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace actorController.collsion
{
    public struct BoxCastOrigin
    {
        public Vector2 center;
        public float centerLeftDistance, centerRightDistance, centerTopDistance, centerBottomDistance;
        float skinWidth;
    }

    public struct CollisionInfo
    {
        public RaycastHit2D? bottom, top, left, right;

        public void Reset()
        {
            top = null;
            bottom = null;
            left = null;
            right = null;
        }
    }
}