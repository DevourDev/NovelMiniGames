using UnityEngine;

namespace Game.Fighting
{
    public class TurnsManagerFromInspectorInitializer : MonoBehaviour
    {
        [System.Serializable]
        private struct StageDuration
        {
            public TurnStage TurnStage;
            public float Duration;
        }


        [SerializeField] private StageDuration[] _stageDurations;


        private void Start()
        {
            var tm = GetComponent<TurnsManager>();

            foreach (var sd in _stageDurations)
            {
                tm.SetStageDuration(sd.TurnStage, sd.Duration);
            }

            Destroy(this);
        }
    }
}
