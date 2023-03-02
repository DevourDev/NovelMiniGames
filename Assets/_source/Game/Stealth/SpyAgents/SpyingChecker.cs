using UnityEngine;

namespace Game.Stealth
{
    public class SpyingChecker : MonoBehaviour
    {
        [SerializeField] private StealsSecretsSource _source;
        [SerializeField] private SpyAgent _agent;
        [SerializeField] private float _checkRate = 10f;

        private float _checkCD;


        private void Awake()
        {
            ResetCD();
        }

        private void Update()
        {
            if ((_checkCD -= Time.deltaTime) > 0)
                return;

            ResetCD();
            _source.CheckForSpying(_agent);
        }

        private void ResetCD()
        {
            _checkCD = 1f / _checkRate;
        }

    }
}
