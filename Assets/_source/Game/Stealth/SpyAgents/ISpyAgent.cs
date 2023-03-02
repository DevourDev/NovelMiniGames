using System;

namespace Game.Stealth
{
    public interface ISpyAgent
    {
        float StealthPoints { get; }

        event Action<SpyAgent, float> OnStealthPointsAmountChanged;

        void ChangeStealthPoints(float delta);
    }
}