using UnityEngine;

namespace Game.Fighting
{
    public abstract class SelectedActionViewBase : MonoBehaviour
    {
        public void Init(FighterController controller)
        {
            controller.OnActionSelected += HandleActionSelected;

            if (controller.SelectedAction != null)
                AnnounceFightAction(controller.Fighter, controller.SelectedAction);
        }

        private void HandleActionSelected(FighterOnScene fighter, FightActionSo fightAction)
        {
            AnnounceFightAction(fighter, fightAction);
        }

        public abstract void Clear();

        protected abstract void AnnounceFightAction(FighterOnScene figter, FightActionSo fightAction);
    }
}
