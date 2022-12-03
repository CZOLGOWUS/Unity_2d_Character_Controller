using System.Collections.Generic;

using actorController.controller;
using actorController.displace;

namespace actorController.state
{
    public interface IActorState
    {
        public void StateInitial(ActorController controller);
        public void StateUpdate(List<IDisplace> displaces);
        public void OnStateChange();
    }
}
