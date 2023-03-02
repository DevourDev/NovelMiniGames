using UnityEngine;

namespace Game.Fighting
{
    public abstract class FightActionSo : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite _icon;

        [SerializeField] private string _animParamName;
        [SerializeField] private float _castTime;

        private int? _animParamID;


        public string Name => _name;
        public Sprite Icon => _icon;

        public int AnimParamID
        {
            get
            {
                if (!_animParamID.HasValue)
                    _animParamID = Animator.StringToHash(_animParamName);

                return _animParamID.Value;
            }
        }

        public float PreCastTime => _castTime;


        public abstract void Cast(FighterOnScene fighter);
    }
}
