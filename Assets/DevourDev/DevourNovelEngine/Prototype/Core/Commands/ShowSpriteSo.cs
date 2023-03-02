using System;
using System.Collections.Generic;
using DevourNovelEngine.Prototype.Commands;
using DevourNovelEngine.Prototype.Utils;
using UnityEngine;

namespace DevourNovelEngine.Prototype.Core.Commands
{
    [CreateAssetMenu(menuName = NovelEngineConstants.MenuNameCommands + "Show Sprite")]
    public class ShowSpriteSo : CommandSo
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private RelativePosition _position;



        public Sprite Sprite => _sprite;
        public RelativePosition Position => _position;


        public void Init(Sprite sprite, RelativePosition position)
        {
            _sprite = sprite;
            _position = position;
        }

        public override bool Equals(object obj)
        {
            return obj is ShowSpriteSo so &&
                   base.Equals(obj) &&
                   EqualityComparer<Sprite>.Default.Equals(_sprite, so._sprite) &&
                   EqualityComparer<RelativePosition>.Default.Equals(_position, so._position);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine( _sprite, _position);
        }

    }


}
