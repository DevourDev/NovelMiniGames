using UnityEngine;

namespace Game.Fighting
{
    [CreateAssetMenu(menuName = "Fighting/Actions/Attack")]
    public class AttackFightAction : FightActionSo
    {
        [SerializeField] private float _damage;


        public override void Cast(FighterOnScene fighter)
        {
            if (!fighter.TryGetTarget(out var target))
                return;

            UnityEngine.Debug.Log("Attacking " + target.name);
            var targetHealth = target.GetComponent<HealthComponent>();
            var efficiency = fighter.GetComponent<EfficiencyMultiplierComponent>();
            targetHealth.DealDamage(_damage * efficiency.GetMultiplier());
        }
    }
}
