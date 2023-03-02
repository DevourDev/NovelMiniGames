using System;
using System.Collections.Generic;
using DevourNovelEngine.Prototype.Commands;
using DevourNovelEngine.Prototype.Utils;
using UnityEngine;

namespace DevourNovelEngine.Prototype.Core.Commands
{
    [CreateAssetMenu(menuName = NovelEngineConstants.MenuNameCommands + "Hide Sprite")]
    public class HideSpriteSo : CommandSo
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private RelativePosition _position;


        public Sprite Sprite => _sprite;
        public RelativePosition Position => _position;


        public void Init(Sprite sprite, RelativePosition relativePosition)
        {
            _sprite = sprite;
            _position = relativePosition;
        }


        public override bool Equals(object obj)
        {
            return obj is HideSpriteSo so &&
                   base.Equals(obj) &&
                   EqualityComparer<Sprite>.Default.Equals(_sprite, so._sprite);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_sprite);
        }
    }
}
