using UnityEngine;

namespace Game.Stealth
{
    public abstract class SpyAgentsDetectorModuleBase<TStealthAgent> : MonoBehaviour
        where TStealthAgent : ISpyAgent
    {
        public abstract int Detect(TStealthAgent[] buffer);
    }
}
