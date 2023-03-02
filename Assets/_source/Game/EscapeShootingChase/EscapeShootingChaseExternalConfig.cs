using System.IO;
using DevourDev.Unity.Helpers;

namespace Game.EscapeShootingChase.Config
{
    public sealed class EscapeShootingChaseExternalConfig
    {
        private const string _fileName = "EscapeShootingChaseConfig.txt";
        private static EscapeShootingChaseExternalConfig _cachedCfg;


        public static EscapeShootingChaseExternalConfig GetConfig()
        {
            if (File.Exists(_fileName))
            {
                _ = ExternalConfigHelpers
                .TryOpenConfig<EscapeShootingChaseExternalConfig>
                (_fileName, out var config);

                return config;
            }
            else
            {
                var config = new EscapeShootingChaseExternalConfig();
                _ = ExternalConfigHelpers.TryWriteConfig(_fileName, config);
                return config;
            }
        }


        public static EscapeShootingChaseExternalConfig GetCachedConfig()
        {
            _cachedCfg ??= GetConfig();
            return _cachedCfg;
        }


        public int HeroLivesCount = 5;
        public float HeroSpeed = 2f;

        public float LevelDuration = 30f;
        public float EnemiesCountStat = 2f;
        public float EnemiesCountEnd = 9f;

        public float EnemiesShootingScatterStart = 3f;
        public float EnemiesShootingScatterEnd = 1f;

        public float EnemiesShootingRateStart = 0.3f;
        public float EnemiesShootingRateEnd = 0.9f;

        public float EnemiesProjectilesSpeedStart = 3.5f;
        public float EnemiesProjectilesSpeedEnd = 8.5f;

        public float EnemiesSpeedMin = 1f;
        public float EnemiesSpeedMax = 2f;

        public float EnemiesOnOnePosStayingTimeMin = 2f;
        public float EnemiesOnOnePosStayingTimeMax = 4f;

        public bool EnableMegaEnemies = true;
        public float FirstMegaEnemyEntranceTime = 0.4f;
        public float LastMegaEnemyEntranceTime = 0.8f;
        public float MegaEnemiesCountStart = 1f;
        public float MegaEnemiesCountEnd = 3f;

        public float MegaEnemiesProjectilesSpeedMin = 2f;
        public float MegaEnemiesProjectilesSpeedMax = 4f;

        public float MegaEnemiesShootingRateMin = 0.1f;
        public float MegaEnemiesShootingRateMax = 0.4f;

        public float MegaEnemiesProjectilesScaleMultiplierMin = 3f;
        public float MegaEnemiesProjectilesScaleMultiplierMax = 6f;

        public bool MegaProjectilesInstaKill = false;
        public int MegaProjectilesDamage = 3;
        public bool MegaProjectilesDestroyDefaultProjectiles = true;

        public bool CameraShakeEnabled = true;
        public float CameraShakeAmplXStart = 0.3f;
        public float CameraShakeAmplXEnd = 1f;
        public float CameraShakeAmplYStart = 0.2f;
        public float CameraShakeAmplYEnd = 0.6f;
        public float CameraShakeFrequencyStart = 1f;
        public float CameraShakeFrequencyEnd = 2.1f;
        public int CameraShakeReturningRateMin = 2;
        public int CameraShakeReturningRateMax = 5;

    }
}
