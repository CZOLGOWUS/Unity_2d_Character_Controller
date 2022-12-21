using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

using actorController.displace;
using actorController.state;
using actorController.collsion;
using System.Runtime.InteropServices.WindowsRuntime;

namespace actorController.controller
{
    public class ActorController : MonoBehaviour
    {
        List<IDisplace> displacements = new List<IDisplace>();
        Dictionary<Type, IActorState> states = new Dictionary<Type, IActorState>();
        Dictionary<Type, IDisplace> allDisplacements = new Dictionary<Type, IDisplace>();

        IActorState currentState;
        CollisionDetection collisionDetection;

        Vector2 currentVelocity = Vector2.zero;

        #region Parameters
        public Dictionary<Type, IActorState> States { get => states; private set => states = value; }
        public Dictionary<Type, IDisplace> AllDisplacements { get => allDisplacements; private set => allDisplacements = value; }
        public List<IDisplace> Displacements { get => displacements; private set => displacements = value; }

        public Vector2 CurrentVelocity { get => currentVelocity; set => currentVelocity = value; }
        public CollisionDetection CollisionDetection { get => collisionDetection; private set => collisionDetection = value; }
        #endregion

        private void OnEnable()
        {
            States = GetComponents<IActorState>().ToDictionary((state) => state.GetType(), (d) => d);
            AllDisplacements = GetComponents<IDisplace>().ToDictionary((d) => d.GetType(), (d) => d);

            collisionDetection = GetComponent<CollisionDetection>();

            ChangeState(states[typeof(AirBorn)]);
        }

        void Update()
        {
            currentState.StateUpdate();

            Vector2 nextVelocity = currentState.CalculateVelocity(displacements);
            collisionDetection.ApplyCollision(ref nextVelocity);

            transform.Translate(nextVelocity);
            currentVelocity = nextVelocity;
            displacements.Clear();
        }

        public void ChangeState(IActorState newState)
        {
            currentState?.OnStateChange();
            currentState = newState;
            currentState.StateInitial(this);
        }

    }

}