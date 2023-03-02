using UnityEngine;

namespace Game.Fighting
{
    public class DebugEventsSubscriber : MonoBehaviour
    {
        [SerializeField] private FightingGameManager _fightingGameManager;


        private void Start()
        {
            _fightingGameManager.OnStartCountDownUpdated += HandleStartCountDownUpdated;
        }

        private void HandleStartCountDownUpdated(int secondsLeft)
        {
            Debug.Log($"start game countdown: {secondsLeft}");
        }
    }
}
