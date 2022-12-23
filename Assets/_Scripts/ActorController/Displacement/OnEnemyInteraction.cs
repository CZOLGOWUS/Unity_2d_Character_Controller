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
            if (collision.transform.tag == "Enemy")
            {
                if (actorController.CurrentState.GetType() == typeof(AirBorn))
                {
                    RaycastHit2D hit = Physics2D.BoxCast(
                        collision.bounds.center,
                        collision.bounds.size + Vector3.right,
                        0f,
                        Vector2.up,
                        2f);


                    if (hit.transform.tag == "Player")
                    {
                        displacement = Vector2.up * jumpForce;
                        AddDisplacement();
                    }
                    return;
                }

                GameManager.OnPlayerDeath();

            }
        }
    }
}
