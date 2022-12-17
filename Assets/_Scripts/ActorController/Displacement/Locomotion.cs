using actorController.controller;
using UnityEngine;
using UnityEngine.InputSystem;

namespace actorController.displace
{
    [RequireComponent(typeof(ActorController))]
    public class Locomotion : MonoBehaviour, IDisplace
    {
        ActorController controller;

        [SerializeField] float acceleration = 10f;

        Vector2 locomotionDisplacement = Vector2.zero;


        public Vector2 Displacement { get => locomotionDisplacement; private set { locomotionDisplacement = value; } }
        public float Input { get; private set; }

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
            controller.Displacements.Add(this);
        }

        public void ResetDisplacement()
        {
            locomotionDisplacement = Vector2.zero;
        }

        private float ConverCurrentInputToDisplacementX()
        {
            return acceleration * Input;
        }

        public void OnMoveInput(InputAction.CallbackContext ctx)
        {
            if (ctx.performed || ctx.canceled)
            {
                Input = ctx.ReadValue<float>();
                locomotionDisplacement.x = ConverCurrentInputToDisplacementX();
            }
        }

    }
}
