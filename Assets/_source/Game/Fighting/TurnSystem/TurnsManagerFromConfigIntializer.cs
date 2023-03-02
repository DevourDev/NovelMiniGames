using UnityEngine;

namespace Game.Fighting
{
    public class TurnsManagerFromConfigIntializer : MonoBehaviour
    {
        private void Start()
        {
            var tm = GetComponent<TurnsManager>();
            var cfg = FightingExternalConfig.GetCachedConfig();

            tm.SetStageDuration(TurnStage.Prepare, cfg.PrepareStageDuration);
            tm.SetStageDuration(TurnStage.ActionsSelection, cfg.ActionsSelectionStageDuration);
            tm.SetStageDuration(TurnStage.Actions, cfg.ActionsStageDuration);
            tm.SetStageDuration(TurnStage.WaitActionsEnd, cfg.WaitForActionsEndMinDuration);

            Destroy(this);
        }
    }
}
