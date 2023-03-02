using DevourDev.Unity;
using UnityEngine;

namespace Game.Fighting
{
    public sealed class SimpleHealingAndDamage : PoppingDynamicStatDeltasViewBase
    {
        [SerializeField] private PoppingText3D _healPopPrefab;
        [SerializeField] private PoppingText3D _damagePopPrefab;


        protected override void HandleDamage(float absRaw, float absSafe)
        {
            var pop = Instantiate(_damagePopPrefab);
            pop.transform.position = OriginPoint.position;
            pop.SetText(absRaw.ToString("N0"));
            pop.Init(1, 1);
        }

        protected override void HandleHeal(float absRaw, float absSafe)
        {
            var pop = Instantiate(_damagePopPrefab);
            pop.transform.position = OriginPoint.position;
            pop.SetText(absRaw.ToString("N0"));
            pop.Init(1, 1);
        }
    }
}
