using actorController.controller;
using UnityEngine;
using UnityEngine.InputSystem;

namespace actorController.displace
{
    [RequireComponent(typeof(ActorController))]
    public class Locomotion : MonoBehaviour, IDisplace
    {
        [SerializeField]
        float speed = 10f;
        Vector2 displacement = Vector2.zero;
        Vector2 prevDisplacement = Vector2.zero;
        ActorController controller;

        public Vector2 Displacement { get => displacement; private set { displacement = value; } }

        private void OnEnable()
        {
            controller = GetComponent<ActorController>();
        }

        private void Update()
        {
            if (displacement != Vector2.zero)
                AddDisplacement();
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

        public void OnMoveInput(InputAction.CallbackContext ctx)
        {
            if (ctx.performed || ctx.canceled)
            {
                displacement.x = ctx.ReadValue<float>() * speed;
                AddDisplacement();
            }
        }
    }
}
