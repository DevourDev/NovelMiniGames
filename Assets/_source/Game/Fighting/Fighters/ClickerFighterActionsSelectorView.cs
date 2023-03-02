using DevourDev.Unity.Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Fighting
{
    public class ClickerFighterActionsSelectorView : MonoBehaviour
    {
        [SerializeField] private ClickerFighterActionsSelector _selector;
        [SerializeField] private Gradient _gradient;
        [SerializeField] private Slider _slider;
        [SerializeField] private Image _handleImg;
        [SerializeField] private Image _bgImg;
        //xtodo: add generic action icons placement


        private void Start()
        {
            _slider.minValue = ClickerFighterActionsSelector.Min;
            _slider.maxValue = ClickerFighterActionsSelector.Max;

            _selector.OnPositionChanged += HandlePositionChanged;
            HandlePositionChanged(_selector.Position, 0);

            var tm = CachingAccessors.Get<TurnsManager>();
            tm.OnNewTurn += Tm_OnNewTurn;
        }

        private void Tm_OnNewTurn(TurnsManager arg1, int arg2)
        {
            arg1.OnNewTurn -= Tm_OnNewTurn;

            _selector.Controller.OnActionSelected += HandleSelectedAction;
            HandleSelectedAction(_selector.Controller.Fighter, _selector.Controller.SelectedAction);
        }

        private void HandleSelectedAction(FighterOnScene fighter, FightActionSo action)
        {
            if (action != null)
                _handleImg.sprite = action.Icon;
        }

        private void HandlePositionChanged(float newPos, float delta)
        {
            _slider.value = newPos;
            _bgImg.color = _gradient.Evaluate(_slider.normalizedValue);
        }
    }
}
