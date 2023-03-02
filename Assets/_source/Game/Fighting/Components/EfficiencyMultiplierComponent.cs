using UnityEngine;

namespace Game.Fighting
{
    public sealed class EfficiencyMultiplierComponent : FighterComponent
    {
        [SerializeField] private float _baseMultiplier = 1f;
        [SerializeField] private FighterLuckComponent _luck;


        public float BaseMultiplier { get => _baseMultiplier; set => _baseMultiplier = value; }


        public float GetMultiplier()
        {
            if (_luck != null)
                return _baseMultiplier * _luck.RollTheDice();

            return _baseMultiplier;
        }
    }
}
