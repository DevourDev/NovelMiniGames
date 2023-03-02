using DevourDev.Unity;
using TMPro;
using UnityEngine;

namespace Game.Stealth
{
    public class SpyAgentView : MonoBehaviour
    {
        [SerializeField] private SpyAgent _agent;
        [SerializeField] private Vector3 _agentHeadOffset = Vector3.up;
        [SerializeField] private TextMeshPro _stealthPointsText;

        [SerializeField] private TextMeshProUGUI _stealthBonusText;

        [SerializeField] private PoppingText3D _secretSpiedPopOutPrefab;


        private void Start()
        {
            HandleStealthPointsAmountChanged(_agent, _agent.StealthPoints);
            HandleStealthBonusValueChanged(_agent, _agent.StealthBonus);


            _agent.OnSecretSpied += HandleSecretSpied;
            _agent.OnStealthPointsAmountChanged += HandleStealthPointsAmountChanged;
            _agent.OnStealthBonusValueChanged += HandleStealthBonusValueChanged;
        }

        private void HandleStealthBonusValueChanged(SpyAgent arg1, SpyAgent.StealthBonusPrecalculated bonus)
        {
            _stealthBonusText.text = $"stealth bonus: for adding: {bonus.ForAdding:N1}, for removing: {bonus.ForRemoving:N1}";
        }

        private void HandleStealthPointsAmountChanged(SpyAgent agent, float pts)
        {
            _stealthPointsText.text = $"скрытность: {pts:N1}";
        }

        private void HandleSecretSpied(SpyAgent agent, StealthSecret secret)
        {
            var popOut = Instantiate(_secretSpiedPopOutPrefab);
            popOut.transform.position = transform.position + _agentHeadOffset;
            popOut.SetText($"Секрет раскрыт: {secret.Message}");
            popOut.Init();
        }
    }
}
