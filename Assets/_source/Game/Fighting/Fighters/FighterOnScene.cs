using UnityEngine;

namespace Game.Fighting
{
    public class FighterOnScene : MonoBehaviour
    {
        private FighterSo _reference;
        private FighterOnScene _target;


        public FighterSo Reference => _reference;


        internal void InitFighterOnScene(FighterSo reference)
        {
            _reference = reference;
            var components = gameObject.GetComponents<FighterComponent>();

            foreach (var comp in components)
            {
                comp.SetFighter(this);
            }
        }

        public void SetTarget(FighterOnScene target) => _target = target;

        public bool TryGetTarget(out FighterOnScene target)
        {
            target = _target;
            return target != null;
        }
    }
}
