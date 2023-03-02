using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Fighting
{
    public sealed class SimpleSelectedActionView : SelectedActionViewBase
    {
        [SerializeField] private Image _img;
        [SerializeField] private TextMeshProUGUI _text;


        public override void Clear()
        {
            _img.sprite = null;
            _text.text = string.Empty;
        }

        protected override void AnnounceFightAction(FighterOnScene fighter, FightActionSo fightAction)
        {
            _img.sprite = fightAction.Icon;
            _text.text = fightAction.Name;
        }
    }
}
