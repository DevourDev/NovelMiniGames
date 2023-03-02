using DevourDev.Unity.Stats;
using UnityEngine;

namespace Game.Fighting
{
    [System.Obsolete("no reason", true)]
    public readonly struct ReadOnlyDynamicStatData
    {
        public readonly float Min;
        public readonly float Max;
        public readonly float Value;


        public ReadOnlyDynamicStatData(float min, float max, float value)
        {
            Min = min;
            Max = max;
            Value = value;
        }

        public ReadOnlyDynamicStatData(DynamicStatData dynamicStatData)
        {
            Min = dynamicStatData.Min;
            Max = dynamicStatData.Max;
            Value = dynamicStatData.Value;
        }
    }

    public sealed class HealthComponent : FighterComponent
    {
        [SerializeField] private StatSo _vitalStat;
        [SerializeField] private DynamicStatsCollectionComponent _dynamicStatsProvider;
        [SerializeField] private DefenceComponent _defence;

        private DynamicStatData _healthStatData;


        public bool Alive { get; private set; }


        /// <summary>
        /// fighter, statData, rawDelta, safeDelta
        /// </summary>
        public event System.Action<FighterOnScene, DynamicStatData, float, float> OnHealthChanged;

        /// <summary>
        /// fighter, statData, rawDelta, safeDelta
        /// </summary>
        public event System.Action<FighterOnScene, DynamicStatData, float, float> OnFighterDeath;


        public void GetHealthValues(out FighterOnScene fighter, out DynamicStatData healthStatData)
        {
            fighter = Fighter;
            healthStatData = _healthStatData;
        }

        public void DealDamage(float incomingDamage)
        {
            TakeDamage(incomingDamage);
        }

        private void TakeDamage(float incomingDamage)
        {
            if (!Alive)
                return;

            float finalDmg = _defence.ProcessDamage(incomingDamage);
            _healthStatData.RemovePossible(finalDmg);
        }
        private void Awake()
        {
            Alive = true;
            _ = _dynamicStatsProvider.TryGetStatData(_vitalStat, out var vitalStatData);
            _healthStatData = vitalStatData;
            vitalStatData.OnCurrentValueChanged += HandleHealthChanged;
            vitalStatData.OnMinReached += HandleMinHealthReached;
        }


        private void HandleMinHealthReached(DynamicStatData sender, float raw, float safe)
        {
            Alive = false;

            OnFighterDeath?.Invoke(Fighter, sender, raw, safe);
        }

        private void HandleHealthChanged(DynamicStatData sender, float raw, float safe)
        {
            OnHealthChanged?.Invoke(Fighter, sender, raw, safe);
        }
    }
}
