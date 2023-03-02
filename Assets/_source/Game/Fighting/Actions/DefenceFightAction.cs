using UnityEngine;

namespace Game.Fighting
{
    [CreateAssetMenu(menuName = "Fighting/Actions/Defence")]
    public class DefenceFightAction : FightActionSo
    {
        [SerializeField] private FighterArmorSo _bonusArmor;
        [SerializeField] private int _turnsCount = 1;
        [SerializeField] private TurnStage _stage = TurnStage.Prepare;


        public override void Cast(FighterOnScene fighter)
        {
            if (!fighter.TryGetTarget(out var target))
                return;

            UnityEngine.Debug.Log("Defencing target:" + target.name);
            var tmpArmor = fighter.gameObject.AddComponent<TmpArmorComponent>();
            tmpArmor.InitTmpArmor(_bonusArmor);
            tmpArmor.InitTurnBasedTmpComponent(_turnsCount, _stage);
        }
    }
}
