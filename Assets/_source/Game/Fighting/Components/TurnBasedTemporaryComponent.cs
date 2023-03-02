using DevourDev.Unity.Helpers;

namespace Game.Fighting
{
    public abstract class TurnBasedTemporaryComponent : TemporaryComponent
    {
        private int _turnsCount;
        private TurnStage _stage;


        public void InitTurnBasedTmpComponent(int turnsCount, TurnStage stage)
        {
            _turnsCount = turnsCount;
            _stage = stage;
            var gm = CachingAccessors.Get<FightingGameManager>();
            var tm = gm.TurnsManager;
            tm.OnNewTurnStage += Tm_OnNewTurnStage;
            Attach();
        }

        private void Tm_OnNewTurnStage(TurnsManager arg1, TurnStage arg2)
        {
            if (arg2 == _stage)
                --_turnsCount;

            if (_turnsCount > 0)
                return;

            Detach();
            Destroy(this);
        }
    }
}
