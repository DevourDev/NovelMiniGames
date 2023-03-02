using DevourDev.Unity;
using DevourDev.Unity.Helpers;
using UnityEngine;

namespace Game.Fighting
{
    public class FighterLuckComponent : FighterComponent
    {
        [SerializeField] private PoppingText3D _poppingLuckPrefab;
        [SerializeField] private Gradient _luckGradient;

        [SerializeField] private AnimationCurve _luckCurve;

        private System.Random _rnd;


        public void SetLuckCurve(AnimationCurve curve) => _luckCurve = curve;


        private void Awake()
        {
            _rnd = new(UnityEngine.Random.Range(0, int.MaxValue));
        }


        public float RollTheDice()
        {
            var luck = RandomHelpers.NextFloat(_rnd, _luckCurve);
            var pop = Instantiate(_poppingLuckPrefab);
            pop.transform.position = transform.position + Vector3.up;
            pop.SetText($"Удача: {(luck * 100):N0}%");
            pop.SetColor(_luckGradient.Evaluate(luck));
            pop.Init();
            return luck;
        }
    }
}
