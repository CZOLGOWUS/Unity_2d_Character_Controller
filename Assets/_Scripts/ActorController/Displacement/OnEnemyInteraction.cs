using actorController.controller;
using actorController.state;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace actorController.displace
{
    [RequireComponent(typeof(ActorController))]
    public class OnEnemyInteraction : MonoBehaviour, IDisplace
    {
        ActorController actorController;
        BoxCollider2D boxColl;
        Collider2D prevCollider;

        [SerializeField] float jumpForce;

        Vector2 displacement;


        public Vector2 Displacement { get => displacement; private set { displacement = value; } }

        private void OnEnable()
        {
            actorController = GetComponent<ActorController>();
            boxColl = GetComponent<BoxCollider2D>();
        }

        public void AddDisplacement()
        {
            actorController.Displacements.Add(this);
        }

        public Vector2 GetCurrentDisplacement()
        {
            return Displacement;
        }

        public void ResetDisplacement()
        {
            displacement = Vector2.zero;
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (prevCollider != null && prevCollider.transform.gameObject == collision.transform.gameObject)
                return;

            if (collision.transform.tag == "Enemy")
            {
                if (actorController.CurrentState.GetType() == typeof(AirBorn))
                {
                    displacement = Vector2.up * (jumpForce + Mathf.Abs(actorController.CurrentVelocity.y));
                    AddDisplacement();
                    prevCollider = collision;
                    return;
                }

                GameManager.OnPlayerDeath();

            }
        }
    }
}
