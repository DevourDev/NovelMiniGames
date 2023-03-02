using System.Collections.Generic;
using UnityEngine;

namespace Game.Stealth
{
    public class SpyAgent : MonoBehaviour, ISpyAgent
    {
        public struct StealthBonusPrecalculated
        {
            public float ForAdding;
            public float ForRemoving;


            public StealthBonusPrecalculated(float forAdding, float forRemoving)
            {
                ForAdding = forAdding;
                ForRemoving = forRemoving;
            }


            public float Process(float raw, bool add)
            {
                return add ? raw + raw * ForAdding : raw + raw * ForRemoving;
            }
        }


        private float _maxStealthPoints;
        private float _stealthPointsPerSecond;
        private float _stealthPoints;

        private readonly List<StealthBonusSource> _permanentBonusSources = new();
        private readonly List<StealthBonusSource> _dynamicBonusSources = new();

        private StealthBonusPrecalculated _stealthBonus;

        private readonly HashSet<StealthSecret> _spiedSecrets = new();

        private bool _isMoving;

        public float StealthPoints => _stealthPoints;
        public StealthBonusPrecalculated StealthBonus => _stealthBonus;
        public bool IsMoving => _isMoving;


        public event System.Action<SpyAgent, float> OnStealthPointsAmountChanged;
        public event System.Action<SpyAgent, StealthBonusPrecalculated> OnStealthBonusValueChanged;
        public event System.Action<SpyAgent, StealthSecret> OnSecretSpied;


        private void Awake()
        {
            _stealthBonus = new(1f, 1f);
        }

        public void Init(float maxSP, float initialSP, float spPerSecond)
        {
            _maxStealthPoints = maxSP;
            _stealthPoints = initialSP;
            _stealthPointsPerSecond = spPerSecond;
        }

        public void AddSecret(StealthSecret secret)
        {
            if (!_spiedSecrets.Add(secret))
                return;

            Debug.Log("secret spied: " + secret.Message);
            OnSecretSpied?.Invoke(this, secret);
        }

        public void AddPermanentBonusSource(StealthBonusSource source)
        {
            _permanentBonusSources.Add(source);
            RecalculateStealthBonus();
        }

        public void RemovePermanentBonusSource(StealthBonusSource source)
        {
            _permanentBonusSources.Remove(source);
            RecalculateStealthBonus();
        }

        public void AddDynamicBonusSource(StealthBonusSource source)
        {
            _dynamicBonusSources.Add(source);
            RecalculateStealthBonus();
        }

        public void RemoveDynamicBonusSource(StealthBonusSource source)
        {
            _dynamicBonusSources.Remove(source);
            RecalculateStealthBonus();
        }

        private void RecalculateStealthBonus()
        {
            float forAdding = 1;
            float forRemoving = 1;

            foreach (var bs in _permanentBonusSources)
            {
                forAdding = bs.Process(forAdding, true);
                forRemoving = bs.Process(forRemoving, false);
            }

            foreach (var bs in _dynamicBonusSources)
            {
                forAdding = bs.Process(forAdding, true);
                forRemoving = bs.Process(forRemoving, false);
            }

            _stealthBonus.ForAdding = forAdding;
            _stealthBonus.ForRemoving = forRemoving;

            OnStealthBonusValueChanged?.Invoke(this, _stealthBonus);
        }

        public void SetMovingState(bool v)
        {
            if (_isMoving == v)
                return;

            _isMoving = v;
        }


        private void Update()
        {
            ChangeStealthPoints(_stealthPointsPerSecond * Time.deltaTime);
        }



        public void ChangeStealthPoints(float delta)
        {
            if (delta == 0)
                return;

            bool add = delta > 0;

            delta = _stealthBonus.Process(delta, add);

            float raw = _stealthPoints + delta;

            if (raw > _maxStealthPoints)
                raw = _maxStealthPoints;

            if (raw == _stealthPoints)
                return;

            _stealthPoints = raw;
            OnStealthPointsAmountChanged?.Invoke(this, _stealthPoints);
        }


    }
}
