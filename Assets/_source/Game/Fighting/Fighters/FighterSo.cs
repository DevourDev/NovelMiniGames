using UnityEngine;

namespace Game.Fighting
{
    [CreateAssetMenu(menuName = "Fighting/Fighter")]
    public class FighterSo : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite _icon;
        [SerializeField] private FighterOnScene _prefab;


        public string Name => _name;
        public Sprite Icon => _icon;


        public FighterOnScene CreateFighter()
        {
            var fighter = Instantiate(_prefab);
            fighter.InitFighterOnScene(this);
            return fighter;
        }
    }
}
