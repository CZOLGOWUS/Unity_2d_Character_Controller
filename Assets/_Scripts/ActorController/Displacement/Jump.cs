using UnityEngine;
using UnityEngine.InputSystem;

using actorController.controller;

namespace actorController.displace
{
    [RequireComponent(typeof(ActorController))]
    public class Jump : MonoBehaviour, IDisplace
    {

        [SerializeField] private float jumpForce = 50f;

        ActorController controller = null;
        Vector2 displacement;

        public Vector2 Displacement { get => displacement; private set { displacement = value; } }


        private void OnEnable()
        {
            controller = GetComponent<ActorController>();
        }

        public Vector2 GetCurrentDisplacement()
        {
            return Displacement;
        }

        public void AddDisplacement()
        {
            controller.Displaces.Add(this);
        }

        public void ResetDisplacement()
        {
            displacement = Vector2.zero;
        }

        public void OnJumpInput(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                Displacement = Vector2.up * jumpForce;
                AddDisplacement();
                return;
            }

            if (ctx.canceled)
            {
                Displacement = Vector2.zero;
                AddDisplacement();
                return;
            }

        }
    }

}