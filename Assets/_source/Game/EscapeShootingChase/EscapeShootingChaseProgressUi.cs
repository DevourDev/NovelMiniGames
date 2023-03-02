using TMPro;
using UnityEngine;

namespace Game.EscapeShootingChase
{
    public class EscapeShootingChaseProgressUi : MonoBehaviour
    {
        [SerializeField] private EscapeShootingChaseGameManager _gm;

        [SerializeField] private TextMeshProUGUI _timeLeftText;
        [SerializeField] private TextMeshProUGUI _livesLeftText;


        private void Awake()
        {
            _gm.Hero.OnHit += Hero_OnHit;
            
        }

        private void Start()
        {
            Hero_OnHit(_gm.Hero);
        }

        private void Update()
        {
            float timeLeft = _gm.TimeToWinLeft;
            _timeLeftText.text = timeLeft.ToString("N1");
        }

        private void Hero_OnHit(Hero hero)
        {
            _livesLeftText.text = hero.LivesLeft.ToString();
        }
    }
}
