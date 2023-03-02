using System.IO;
using DevourDev.Unity.Helpers;
using UnityEngine;

namespace Game.Fighting
{
    public sealed class FightingExternalConfig
    {
        private const string _fileName = "FightingConfig.txt";
        private static FightingExternalConfig _cachedCfg;


        public static FightingExternalConfig GetConfig()
        {
            if (File.Exists(_fileName))
            {
                _ = ExternalConfigHelpers
                .TryOpenConfig<FightingExternalConfig>
                (_fileName, out var config);

                return config;
            }
            else
            {
                var config = new FightingExternalConfig();
                _ = ExternalConfigHelpers.TryWriteConfig(_fileName, config);
                return config;
            }
        }


        public static FightingExternalConfig GetCachedConfig()
        {
            _cachedCfg ??= GetConfig();
            return _cachedCfg;
        }


        public float PlayerHealth = 220f;
        public float EnemyHealth = 500f;
        
        public Vector2 PlayerLuckCurve = new(0, 1f); 
        public Vector2 EnemyLuckCurve = new(0.666f, 0.777f);

        public float PlayerEfficiencyMultiplier = 1f;
        public float EnemyEfficiencyMultiplier = 0.9f;

        public float ClickerEquilibriumCPS = 3f;
        public float NormalizingSpeed = 0.25f;

        public float PrepareStageDuration = 3f;
        public float ActionsSelectionStageDuration = 4f;
        public float ActionsStageDuration = 2f;
        public float WaitForActionsEndMinDuration = 4f;
    }
}
