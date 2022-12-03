using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

using actorController.displace;
using actorController.state;

namespace actorController.controller
{
    public class ActorController : MonoBehaviour
    {
        List<IDisplace> displaces = new List<IDisplace>();
        Dictionary<Type, IActorState> states = new Dictionary<Type, IActorState>();

        public Dictionary<Type, IActorState> States { get => states; private set => states = value; }
        public List<IDisplace> Displaces { get => displaces; private set => displaces = value; }

        IActorState currentState;

        private void OnEnable()
        {
            GetComponents<IActorState>().ToList().ForEach((state) => states.Add(state.GetType(), state));

            ChangeState(states[typeof(Grouned)]);
        }

        void Start()
        {

        }

        void Update()
        {
            currentState.StateUpdate(displaces);

            displaces.Clear();
        }

        public void ChangeState(IActorState newState)
        {
            currentState?.OnStateChange();
            currentState = newState;
            currentState.StateInitial(this);
        }

    }

}