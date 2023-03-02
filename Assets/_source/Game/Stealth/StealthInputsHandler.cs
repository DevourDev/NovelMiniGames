using UnityEngine;

namespace Game.Stealth
{
    public class StealthInputsHandler : MonoBehaviour
    {
        [SerializeField] private StealthGameManager _gm;
        [SerializeField] private SpyAgent _stealthAgent;
        [SerializeField] private Controller2D _controller;
        [SerializeField] private Vector2 _speed = Vector2.one;


        public Vector2 Speed { get => _speed; set => _speed = value; }


        private void Start()
        {
            var controls = PlayerControlsProvider.Controls;

            var stealth = controls.Stealth;
            stealth.Move.performed += Move_performed;
            stealth.Escape.performed += Escape_performed;
            PlayerControlsProvider.AddConsumer(PlayerControlsProvider.PlayerControlsActionMap.Stealth);

        }

        private void OnDestroy()
        {
            var controls = PlayerControlsProvider.Controls;

            var stealth = controls.Stealth;
            stealth.Move.performed -= Move_performed;
            stealth.Escape.performed -= Escape_performed;
            PlayerControlsProvider.RemoveConsumer(PlayerControlsProvider.PlayerControlsActionMap.Stealth);
        }

        private void Move_performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            Vector2 velocity;
            velocity.x = context.ReadValue<float>();
            velocity.y = 0;
            velocity *= _speed;

            _stealthAgent.SetMovingState(velocity != Vector2.zero);
            _controller.SetVelocity(velocity);
        }

        private void Escape_performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            _gm.Escape();
        }
    }
}
