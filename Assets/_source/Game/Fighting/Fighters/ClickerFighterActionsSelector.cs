using System;
using DevourDev.Unity.Helpers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Fighting
{
    public class ClickerFighterActionsSelector : MonoBehaviour
    {
        [System.Serializable]
        private struct FightActionOnClickSlider
        {
            public FightActionSo FightAction;
            public float Position;
        }


        [SerializeField] private float _equilibriumCPS = 3f;
        [SerializeField] private float _normalizingSpeed = 0.25f;
        [SerializeField] private FightActionOnClickSlider[] _actionsOnClickSlider;


        public const float Min = -1f;
        public const float Max = 1f;
        public const float Center = 0f;

        private FighterController _controller;
        private float _pos;
        private float _clickPower;

        private float _lastCleanPos;
        private bool _isDirty;

        private bool _active;

        private FightActionSo _selectedAction;


        public float Position => _pos;
        public FighterController Controller => _controller;

        /// <summary>
        /// new pos, delta
        /// </summary>
        public event System.Action<float, float> OnPositionChanged;


        private void OnDestroy()
        {
            var fightingControls = PlayerControlsProvider.Controls.Fighting;

            fightingControls.ClickSliderLeft.performed -= HandleLeftPerformed;
            fightingControls.ClickSliderRight.performed -= HandleRightPerformed;
            PlayerControlsProvider.RemoveConsumer(PlayerControlsProvider.PlayerControlsActionMap.Fighting);
        }

        public void Init(FighterController controller)
        {
            Array.Sort(_actionsOnClickSlider, (a, b) => a.Position.CompareTo(b.Position));

            var cfg = FightingExternalConfig.GetCachedConfig();

            _equilibriumCPS = cfg.ClickerEquilibriumCPS;
            _normalizingSpeed = cfg.NormalizingSpeed;

            _clickPower = _normalizingSpeed / _equilibriumCPS;
            _controller = controller;
            var fightingControls = PlayerControlsProvider.Controls.Fighting;

            fightingControls.ClickSliderLeft.performed += HandleLeftPerformed;
            fightingControls.ClickSliderRight.performed += HandleRightPerformed;

            PlayerControlsProvider.AddConsumer(PlayerControlsProvider.PlayerControlsActionMap.Fighting);

            var turnManager = CachingAccessors.Get<TurnsManager>();
            turnManager.OnNewTurnStage += HandleNewStage;
        }


        private void CheckMouseClick() //mobile
        {
            if (!Input.GetKeyDown(KeyCode.Mouse0))
                return;

            float pos = Input.mousePosition.x;

            if (pos < Screen.width / 2f)
                ClickLeft();
            else
                ClickRight();
        }

        private void HandleNewStage(TurnsManager turnManager, TurnStage stage)
        {
            switch (stage)
            {
                case TurnStage.ActionsSelection:
                    AllowSelecting();
                    break;
                default:
                    _active = false;
                    break;
            }

        }

        private void AllowSelecting()
        {
            _selectedAction = null;
            _active = true;
        }

        private void HandleLeftPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            ClickLeft();
        }


        private void HandleRightPerformed(InputAction.CallbackContext obj)
        {
            ClickRight();
        }

        private void ClickLeft()
        {
            if (_active)
                ChangePos(-_clickPower);
        }

        private void ClickRight()
        {
            if (_active)
                ChangePos(_clickPower);
        }


        private void Update()
        {
            if (_controller == null)
                return;

            Normalize(_normalizingSpeed * Time.deltaTime);

            //CheckMouseClick(); //mobile

            SelectAction();

            ReDraw();
        }

        private void SelectAction()
        {
            if (!_active)
                return;

            var arr = _actionsOnClickSlider;
            var c = arr.Length;

            float closestD = float.PositiveInfinity;
            int closestIndex = -1;

            for (int i = 0; i < c; i++)
            {
                var act = arr[i];

                float dist = Math.Abs(act.Position - _pos);

                if (dist < closestD)
                {
                    closestD = dist;
                    closestIndex = i;
                }
            }

            if (closestIndex < 0)
                return;

            var action = arr[closestIndex].FightAction;

            if (_selectedAction == action)
                return;

            _selectedAction = action;
            _controller.SelectAction(action);
        }

        private void ReDraw()
        {
            if (!_isDirty)
                return;

            OnPositionChanged?.Invoke(_pos, _pos - _lastCleanPos);
            _lastCleanPos = _pos;
            _isDirty = false;
        }

        private void ResetPosition()
        {
            if (_pos == Center)
                return;

            _pos = Center;
            _isDirty = true;
        }

        private void Normalize(float v)
        {
            if (_pos == Center)
                return;

            float max = Math.Abs(_pos);

            if (v > max)
                v = max;

            if (_pos > Center)
                v = -v;

            ChangePos(v);
        }

        private void ChangePos(float delta)
        {
            float newPos = System.Math.Clamp(_pos + delta, Min, Max);

            if (newPos == _pos)
                return;

            _pos = newPos;
            _isDirty = true;
        }
    }
}
