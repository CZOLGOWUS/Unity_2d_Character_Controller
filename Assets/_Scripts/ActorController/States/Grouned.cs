using System.Collections.Generic;
using UnityEngine;

using actorController.displace;
using actorController.controller;

namespace actorController.state
{
    [RequireComponent(typeof(ActorController))]
    public class Grouned : MonoBehaviour, IActorState
    {
        ActorController controller = null;

        public void StateInitial(ActorController controller)
        {
            Debug.Log("Initial of: " + this.ToString());
            this.controller = controller;
        }

        public void OnStateChange()
        {
            Debug.Log("OnStateChange of: " + this.ToString());
        }

        public void StateUpdate(List<IDisplace> displaces)
        {
            //Debug.Log("Update of: " + this.ToString());

            foreach (var i in displaces)
            {
                Debug.Log(" displaces: " + i.ToString());
            }

            //controller.ChangeState(controller.States[typeof(AirBorn)]);
        }
    }
}