using System;
using System.Collections;
using DevourDev.Unity.Helpers;
using Game.Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Fighting
{
    public class FightingGameManager : MonoBehaviour
    {
        public enum MiniGameResult
        {
            InProgress,
            Win,
            Lose
        }


        [SerializeField] private ArmorCalculationMethod _armorCalculationMethod;
        [SerializeField] private TurnsManager _turnsManager;
        [SerializeField] private int _startDelay = 5;


        [SerializeField] private FighterSo _playerFighter;
        [SerializeField] private FighterSo _enemyFighter;

        [SerializeField] private FightingUi _ui;
        [SerializeField] private ClickerFighterActionsSelector _clicker;

        [SerializeField] private Transform[] _fightersPoints;

        private FighterOnScene _playerInst;

        public ArmorCalculationMethod ArmorCalculationMethod => _armorCalculationMethod;
        public TurnsManager TurnsManager => _turnsManager;

        public MiniGameResult Result { get; private set; }

        /// <summary>
        /// seconds left
        /// </summary>
        public event System.Action<int> OnStartCountDownUpdated;


        private IEnumerator Start()
        {
            yield return new WaitForSeconds(2f);
            StartGameMode();
        }

        public void StartGameMode()
        {
            var playerFighterInst = _playerFighter.CreateFighter();
            _playerInst = playerFighterInst;
            var enemyFighterInst = _enemyFighter.CreateFighter();

            playerFighterInst.transform.position = _fightersPoints[0].position;
            enemyFighterInst.transform.position = _fightersPoints[1].position;

            enemyFighterInst.transform.localScale = new Vector3(-1, 1, 1);

            playerFighterInst.SetTarget(enemyFighterInst);
            enemyFighterInst.SetTarget(playerFighterInst);

            playerFighterInst.GetComponent<HealthComponent>().OnFighterDeath += HandleDeath;
            enemyFighterInst.GetComponent<HealthComponent>().OnFighterDeath += HandleDeath;

            ConfigureFighters(playerFighterInst, enemyFighterInst);

            _ui.InitUi(playerFighterInst);
            _clicker.Init(playerFighterInst.GetComponent<FighterController>());

            if (playerFighterInst.TryGetComponent<FighterAi>(out var ai))
            {
                Destroy(ai);
            }

            StartCoroutine(StartDelayed(_startDelay));
        }

        private void ConfigureFighters(FighterOnScene playerFighterInst, FighterOnScene enemyFighterInst)
        {
            var cfg = FightingExternalConfig.GetCachedConfig();

            playerFighterInst.GetComponent<DynamicStatsCollectionComponent>()
                .SetStatInternal(0, cfg.PlayerHealth, cfg.PlayerHealth, true);

            enemyFighterInst.GetComponent<DynamicStatsCollectionComponent>()
                .SetStatInternal(0, cfg.EnemyHealth, cfg.EnemyHealth, true);


            playerFighterInst.GetComponent<FighterLuckComponent>()
                .SetLuckCurve(AnimationHelpers.CurveStartEnd(cfg.PlayerLuckCurve.x, cfg.PlayerLuckCurve.y));

            enemyFighterInst.GetComponent<FighterLuckComponent>()
               .SetLuckCurve(AnimationHelpers.CurveStartEnd(cfg.EnemyLuckCurve.x, cfg.EnemyLuckCurve.y));


            playerFighterInst.GetComponent<EfficiencyMultiplierComponent>()
                .BaseMultiplier = cfg.PlayerEfficiencyMultiplier;

            enemyFighterInst.GetComponent<EfficiencyMultiplierComponent>()
                .BaseMultiplier = cfg.EnemyEfficiencyMultiplier;
        }

        private void HandleDeath(FighterOnScene arg1, DevourDev.Unity.Stats.DynamicStatData arg2, float arg3, float arg4)
        {
            if (Result != MiniGameResult.InProgress)
                return;

            Result = arg1 == _playerInst ? MiniGameResult.Lose : MiniGameResult.Win;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return;
            var smh = CachingAccessors.Get<SceneManagementHelper>();
            smh.ReloadScene();
        }

        private IEnumerator StartDelayed(int secondsToStart)
        {
            var waitForSecond = new WaitForSecondsRealtime(1f);

            for (int i = 0; i < secondsToStart; i++)
            {
                OnStartCountDownUpdated?.Invoke(secondsToStart - i);
                yield return waitForSecond;
            }

            StartGameModeInternal();
        }

        private void StartGameModeInternal()
        {
            OnStartCountDownUpdated?.Invoke(0);
            _turnsManager.StartTurnsManager();
        }
    }
}
