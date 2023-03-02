using DevourDev.Unity.Helpers;
using Game.EscapeShootingChase.Config;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.EscapeShootingChase
{
    public class EscapeShootingChaseInputsHandler : MonoBehaviour
    {
        [SerializeField] private Controller2D _controller;
        [SerializeField] private Vector2 _speed = Vector2.one;

        private bool _started;

        public Vector2 Speed { get => _speed; set => _speed = value; }


        private void OnEnable()
        {
            if (!_started)
                return;

            var controls = PlayerControlsProvider.Controls;
            var upDown = controls.UpDown;
            upDown.Move.performed += MovePerformedPC;
            PlayerControlsProvider.AddConsumer(PlayerControlsProvider.PlayerControlsActionMap.UpDown);
        }

        private void Start()
        {
            var config = EscapeShootingChaseExternalConfig.GetConfig();
            _speed.y = config.HeroSpeed;

            _started = true;
            OnEnable();
        }

        private void OnDisable()
        {
            var controls = PlayerControlsProvider.Controls;
            var upDown = controls.UpDown;
            upDown.Move.performed -= MovePerformedPC;
            PlayerControlsProvider.RemoveConsumer(PlayerControlsProvider.PlayerControlsActionMap.UpDown);
            _controller.SetVelocity(Vector2.zero);
        }


        private void MovePerformedPC(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            SendMoveCommand(context.ReadValue<float>());
        }

        private void SendMoveCommand(float v)
        {
            Vector2 velocity;
            velocity.x = 0;
            velocity.y = v;
            velocity *= _speed;
            _controller.SetVelocity(velocity);
        }
    }
}
