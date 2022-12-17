using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using actorController.displace;
using actorController.controller;

namespace actorController.state
{
    [RequireComponent(typeof(ActorController))]
    public class AirBorn : MonoBehaviour, IActorState
    {
        ActorController controller = null;

        public void StateInitial(ActorController controller)
        {
            Debug.Log("Initial of: " + this.name);
            this.controller = controller;
        }

        public void OnStateChange()
        {
            Debug.Log("OnStateChange of: " + this.name);
        }

        public void StateUpdate()
        {

        }

        public Vector2 CalculateVelocity(List<IDisplace> displaces)
        {
            Debug.Log("Update of: " + this.name);

            foreach (var i in displaces)
            {
                Debug.Log(" displaces: " + i.ToString());
            }

            return Vector2.zero;

        }
    }
}
