using System;
using System.Collections.Generic;
using DevourNovelEngine.Prototype.Characters;
using DevourNovelEngine.Prototype.Commands;
using DevourNovelEngine.Prototype.Utils;
using UnityEngine;

namespace DevourNovelEngine.Prototype.Core.Commands
{
    [CreateAssetMenu(menuName = NovelEngineConstants.MenuNameCommands + "Hide Character")]
    public class HideCharacterSo : CommandSo
    {
        [SerializeField] private CharacterReferenceSo _character;
        [SerializeField] private RelativePosition _position;


        public CharacterReferenceSo Character => _character;
        public RelativePosition RelativePosition => _position;


        public void Init(CharacterReferenceSo character, RelativePosition relativePosition)
        {
            _character = character;
            _position = relativePosition;
        }


        public override bool Equals(object obj)
        {
            return obj is HideCharacterSo so &&
                   base.Equals(obj) &&
                   EqualityComparer<CharacterReferenceSo>.Default.Equals(_character, so._character);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_character.GetHashCode());
        }

    }
}
