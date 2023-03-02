using UnityEngine;

namespace Game.Stealth
{
    public class SpyAgentBonusSourcesDetector : MonoBehaviour
    {
        [SerializeField] private SpyAgent _agent;


        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.TryGetComponent<StealthBonusSource>(out var bs))
                _agent.AddDynamicBonusSource(bs);
        }


        private void OnTriggerExit2D(Collider2D collider)
        {
            if (collider.TryGetComponent<StealthBonusSource>(out var bs))
                _agent.RemoveDynamicBonusSource(bs);
        }
    }
}
