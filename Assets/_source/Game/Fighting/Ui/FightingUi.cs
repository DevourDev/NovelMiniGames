using UnityEngine;

namespace Game.Fighting
{
    public sealed class FightingUi : MonoBehaviour
    {
        [SerializeField] private PoppingDynamicStatDeltasViewBase _playerPoppingDelta;
        [SerializeField] private PoppingDynamicStatDeltasViewBase _enemyPoppingDelta;

        [SerializeField] private DynamicStatSliderViewBase _playerHealth;
        [SerializeField] private DynamicStatSliderViewBase _enemyHealth;

        [SerializeField] private SelectedActionViewBase _playerSelectedAction;
        [SerializeField] private SelectedActionViewBase _enemySelectedAction;


        public void InitUi(FighterOnScene playerFighter)
        {
            _ = playerFighter.TryGetTarget(out var enemyFighter);

            var pHealth = playerFighter.GetComponent<HealthComponent>();
            var eHealth = enemyFighter.GetComponent<HealthComponent>();

            var pController = playerFighter.GetComponent<FighterController>();
            var eController = enemyFighter.GetComponent<FighterController>();

            pHealth.GetHealthValues(out _, out var pHealthStatData);
            eHealth.GetHealthValues(out _, out var eHealthStatData);

            _playerHealth.Init(pHealthStatData);
            _enemyHealth.Init(eHealthStatData);

            _playerSelectedAction.Init(pController);
            _enemySelectedAction.Init(eController);

            _playerPoppingDelta.Init(pHealthStatData, playerFighter.transform);
            _enemyPoppingDelta.Init(eHealthStatData, enemyFighter.transform);
        }
    }
}
