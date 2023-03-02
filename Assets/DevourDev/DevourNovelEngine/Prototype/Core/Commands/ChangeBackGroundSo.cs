using System;
using DevourNovelEngine.Prototype.Commands;
using UnityEngine;

namespace DevourNovelEngine.Prototype.Core.Commands
{
    [CreateAssetMenu(menuName = NovelEngineConstants.MenuNameCommands + "Change BackGround")]
    public class ChangeBackGroundSo : CommandSo
    {
        [SerializeField] private Sprite _sprite;


        public Sprite Sprite => _sprite;


        public void Init(Sprite sprite)
        {
            _sprite = sprite;
        }


        public override int GetHashCode()
        {
            return HashCode.Combine(_sprite.GetInstanceID());
        }

        public override bool Equals(object other)
        {
            return other.GetHashCode() == GetHashCode()
                && other is ChangeBackGroundSo otherC
                && otherC._sprite == _sprite;
        }

    }



}
