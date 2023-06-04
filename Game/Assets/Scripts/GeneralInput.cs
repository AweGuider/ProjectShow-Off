using UnityEngine;
using UnityEngine.InputSystem;

namespace KartGame.KartSystems
{

    public class GeneralInput : BaseInput
    {
        public Vector2 Turning;
        public bool Acceleration;
        public bool Breaking;

        public override InputData GenerateInput()
        {
            return new InputData
            {
                Accelerate = Acceleration,
                Brake = Breaking,
                TurnInput = Turning.x
            };
        }

        public void OnMove(InputAction.CallbackContext context) => Turning = context.ReadValue<Vector2>();
        public void OnAccelerate(InputAction.CallbackContext context) => Acceleration = context.action.triggered;
        public void OnDecelerate(InputAction.CallbackContext context) => Breaking = context.action.triggered;
    }
}
