using System.Collections.Generic;
using UnityEngine;

using actorController.displace;
using actorController.controller;
using Unity.VisualScripting;

namespace actorController.state
{
    [RequireComponent(typeof(ActorController))]
    public class Grouned : MonoBehaviour, IActorState
    {
        ActorController actorController = null;
        IDisplace locomotion = null;

        [SerializeField] float minTravelDistance = 0.01f;
        [Range(0f, 1f)][SerializeField] float accelerationTime;

        [SerializeField] float gravity = 9f;
        [SerializeField] float maxSpeed = 20f;
        [SerializeField] float friction = 0.2f;


        float currentVelocitySmoother = 0f;


        public void StateInitial(ActorController controller)
        {
            Debug.Log("Initial of: " + this.ToString());
            this.actorController = controller;
            locomotion = controller.AllDisplacements[typeof(Locomotion)];
        }

        public void OnStateChange()
        {
            Debug.Log("OnStateChange of: " + this.ToString());
        }

        public void StateUpdate()
        {
            StateChangeCheck();
            locomotion.AddDisplacement();
        }

        public Vector2 CalculateVelocity(List<IDisplace> displaces)
        {
            Vector2 velocity = actorController.CurrentVelocity;
            Vector2 targetVelocity = Vector2.zero;

            targetVelocity = SumOfAllDisplacments(displaces, targetVelocity);
            velocity.x = ApplyMovementSmoothing(velocity, targetVelocity) * Time.deltaTime;

            velocity.y += targetVelocity.y;

            velocity.y -= gravity * Time.deltaTime * 0.1f;

            velocity = ClampVelocity(velocity);

            return velocity;
        }

        private void StateChangeCheck()
        {
            if (IsAirBorn())
                actorController.ChangeState(actorController.States[typeof(AirBorn)]);
        }

        private bool IsAirBorn()
        {
            return actorController.CollisionDetection.CollisionInfo.vertical[0].collider == null;
        }

        private Vector2 ClampVelocity(Vector2 velocity)
        {
            if (IsOverMaxSpeed(ref velocity))
                velocity = maxSpeed * velocity.normalized;
            return velocity;
        }

        private bool IsOverMaxSpeed(ref Vector2 velocity)
        {
            return velocity.magnitude > maxSpeed;
        }

        private float ApplyMovementSmoothing(Vector2 velocity, Vector2 targetVelocity)
        {
            return Mathf.SmoothDamp(velocity.x, targetVelocity.x, ref currentVelocitySmoother, accelerationTime);
        }

        private Vector2 SumOfAllDisplacments(List<IDisplace> displaces, Vector2 targetVelocity)
        {
            foreach (var i in displaces)
            {
                targetVelocity += i.GetCurrentDisplacement();
            }
            return targetVelocity;
        }
    }
}