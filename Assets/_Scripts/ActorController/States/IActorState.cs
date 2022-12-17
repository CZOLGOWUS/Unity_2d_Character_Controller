using System.Collections.Generic;
using UnityEngine;
using actorController.controller;
using actorController.displace;

namespace actorController.state
{
    public interface IActorState
    {
        public void StateInitial(ActorController controller);
        public Vector2 CalculateVelocity(List<IDisplace> displaces);
        public void StateUpdate();
        public void OnStateChange();
    }
}
