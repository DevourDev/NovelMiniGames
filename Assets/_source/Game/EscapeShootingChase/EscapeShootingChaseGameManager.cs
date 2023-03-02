using System.Collections.Generic;
using DevourDev.Unity.Helpers;
using Game.EscapeShootingChase.Config;
using UnityEngine;
using Utils;

namespace Game.EscapeShootingChase
{

    public class EscapeShootingChaseGameManager : MonoBehaviour
    {
        public enum MiniGameResult
        {
            InProgress,
            HeroDied,
            HeroEscaped,
        }

        //ui

        [SerializeField] private EndGamePanelUi _endGamePanelUi;
        [SerializeField] private DisableEventSource _disableEventSource;

        //not ui)_s0_-)))

        [SerializeField] private Hero _hero;

        [SerializeField] private int _heroLivesCount = 3;
        [SerializeField] private AnimationCurve _timeScaleOverHeroLivesLeftCurve;
        [SerializeField] private float _levelDuration = 20f;
        [SerializeField] private Transform _heroFrom;
        [SerializeField] private Transform _heroTo;
        [SerializeField] private AnimationCurve _enemiesCountOverTimeCurve;
        [SerializeField] private AnimationCurve _enemiesShootingScatterOverTimeCurve;
        [SerializeField] private AnimationCurve _enemiesShootingRateOverTimeCurve;
        [SerializeField] private AnimationCurve _enemiesProjectileSpeedOverTimeCurve;
        [SerializeField] private float _curveUpdateRate = 1f;

        [SerializeField] private EscapeEnemy _enemyPrefab;
        [SerializeField] private SceneBounderBase _enemiesBounder;

        private readonly List<EscapeEnemy> _enemies = new();
        private EnemyStats _enemiesStats;

        private float _timeToWinLeft;
        private float _timeToCurveUpdateLeft;

        public MiniGameResult Result { get; private set; }
        public Hero Hero => _hero;
        public float TimeToWinLeft => _timeToWinLeft;



        private void Awake()
        {
            var config = EscapeShootingChaseExternalConfig.GetCachedConfig();

            _heroLivesCount = config.HeroLivesCount;
            _levelDuration = config.LevelDuration;

            _enemiesCountOverTimeCurve.MoveKey(0, new Keyframe(0f, config.EnemiesCountStat));
            _enemiesCountOverTimeCurve.MoveKey(1, new Keyframe(1f, config.EnemiesCountEnd));

            _enemiesShootingScatterOverTimeCurve.MoveKey(0, new Keyframe(0f, config.EnemiesShootingScatterStart));
            _enemiesShootingScatterOverTimeCurve.MoveKey(1, new Keyframe(1f, config.EnemiesShootingScatterEnd));

            _enemiesShootingRateOverTimeCurve.MoveKey(0, new Keyframe(0f, config.EnemiesShootingRateStart));
            _enemiesShootingRateOverTimeCurve.MoveKey(1, new Keyframe(1f, config.EnemiesShootingRateEnd));

            _enemiesProjectileSpeedOverTimeCurve.MoveKey(0, new Keyframe(0f, config.EnemiesProjectilesSpeedStart));
            _enemiesProjectileSpeedOverTimeCurve.MoveKey(1, new Keyframe(1f, config.EnemiesProjectilesSpeedEnd));


            Time.timeScale = 1f;
            _enemiesStats = new EnemyStats();
            _enemiesStats.ShootingTarget = _hero.transform;
            _hero.OnHit += HandleHeroHitted;
            _hero.OnDeath += HandleHeroDied;
            _hero.InitHero(_heroLivesCount);

            ResetTimeToWinCounter();
        }

        private void ResetTimeToWinCounter()
        {
            _timeToWinLeft = _levelDuration;
        }

        private void ResetTimeToCurveUpdateCounter()
        {
            _timeToCurveUpdateLeft = 1f / _curveUpdateRate;
        }


        private int _framesToSkip = 10;

        private void Update()
        {
            if (--_framesToSkip > 0)
                return;

            ProcessWinCountDown();

            if (Result != MiniGameResult.InProgress)
                return;

            ProcessHeroXPosition(Mathf.InverseLerp(_levelDuration, 0, _timeToWinLeft));
            ProcessCurveUpdateCountDown();
        }

        private void ProcessCurveUpdateCountDown()
        {
            if ((_timeToCurveUpdateLeft -= Time.unscaledDeltaTime) > 0)
                return;

            ResetTimeToCurveUpdateCounter();

            UpdateCurves();
        }

        private void UpdateCurves()
        {
            float levelTimeT = Mathf.InverseLerp(_levelDuration, 0, _timeToWinLeft);
            UpdateEnemiesCountCurve(levelTimeT);
            UpdateEnemiesShootingScatterCurve(levelTimeT);
            UpdateEnemiesShootingRateCurve(levelTimeT);
            UpdateEnemiesProjectileSpeedCurve(levelTimeT);
        }

        private void ProcessHeroXPosition(float t)
        {
            float from = _heroFrom.position.x;
            float to = _heroTo.position.x;
            var heroPos = _hero.transform.position;
            heroPos.x = UnityEngine.Mathf.Lerp(from, to, t);
            _hero.transform.position = heroPos;
        }

        private void UpdateEnemiesCountCurve(float t)
        {
            int enemiesCount = (int)_enemiesCountOverTimeCurve.Evaluate(t);
            int delta = enemiesCount - _enemies.Count;

            if (delta == 0)
                return;

            if (delta < 0)
            {
                //removing enemies

                delta = -delta;

                for (int i = 0; i < delta; i++)
                {
                    Destroy(_enemies[^1].gameObject);
                    _enemies.RemoveAt(_enemies.Count - 1);
                }

                return;
            }

            //adding enemies

            for (int i = 0; i < delta; i++)
            {
                _enemies.Add(GenerateEnemy());
            }
        }

        private void UpdateEnemiesShootingScatterCurve(float t)
        {
            _enemiesStats.ShootingScatter = _enemiesShootingScatterOverTimeCurve.Evaluate(t);
        }

        private void UpdateEnemiesShootingRateCurve(float t)
        {
            _enemiesStats.ShootingRate = _enemiesShootingRateOverTimeCurve.Evaluate(t);
        }

        private void UpdateEnemiesProjectileSpeedCurve(float t)
        {
            _enemiesStats.ProjectileSpeed = _enemiesProjectileSpeedOverTimeCurve.Evaluate(t);
        }


        private EscapeEnemy GenerateEnemy()
        {
            var inst = Instantiate(_enemyPrefab);

            Vector2 pos;
            Vector2 min = _enemiesBounder.Min;
            Vector2 max = _enemiesBounder.Max;
            pos.x = UnityEngine.Random.Range(min.x, max.x);
            pos.y = UnityEngine.Random.Range(min.y, max.y);

            inst.transform.position = pos;

            inst.InitEnemy(_enemiesStats);
            var randomMover = inst.GetComponent<RandomMoverComponent>();
            randomMover.Bounder = _enemiesBounder;
            return inst;
        }

        private void ProcessWinCountDown()
        {
            if ((_timeToWinLeft -= Time.unscaledDeltaTime) > 0)
                return;

            if (_hero.IsAlive)
            {
                EndGame(MiniGameResult.HeroEscaped);
            }
        }

        private void HandleHeroDied(Hero hero)
        {
            EndGame(MiniGameResult.HeroDied);
        }

        private void EndGame(MiniGameResult result)
        {
            _disableEventSource.Command(false);
            Result = result;
            var pool = CachingAccessors.Get<ProjectilesPool>();

            var bullets = FindObjectsOfType<Projectile>();

            foreach (var b in bullets)
            {
                pool.Return(b);
            }

            foreach (var enemy in _enemies)
            {
                enemy.enabled = false;
            }

            var titleText = _endGamePanelUi.TitleText;
            var msgText = _endGamePanelUi.MessageText;

            if (Result == MiniGameResult.HeroEscaped)
            {
                titleText.text = $"Congratulations, {System.Environment.UserName}!";
                titleText.color = Color.green * 0.9f;

                msgText.text = "You successfully managed to escape. Enjoy your StoryLine.";
            }
            else
            {
                titleText.text = $"You lost lol.";
                titleText.color = Color.red * 0.9f;

                msgText.text = $"Just {_timeToWinLeft:N1} more seconds and you could've escaped.";
            }

            _endGamePanelUi.gameObject.SetActive(true);
            enabled = false;
        }

        private void HandleHeroHitted(Hero hero)
        {
            float t = Mathf.InverseLerp(_heroLivesCount, 0, hero.LivesLeft);
            Time.timeScale = _timeScaleOverHeroLivesLeftCurve.Evaluate(t);
        }
    }
}
