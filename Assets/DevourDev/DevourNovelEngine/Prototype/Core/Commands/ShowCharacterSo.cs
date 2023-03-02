using System;
using System.Collections.Generic;
using DevourNovelEngine.Prototype.Characters;
using DevourNovelEngine.Prototype.Commands;
using DevourNovelEngine.Prototype.Utils;
using UnityEngine;

namespace DevourNovelEngine.Prototype.Core.Commands
{
    [CreateAssetMenu(menuName = NovelEngineConstants.MenuNameCommands + "Show Character")]
    public class ShowCharacterSo : CommandSo
    {
        [SerializeField] private CharacterReferenceSo _character;
        [SerializeField] private RelativePosition _position;



        public CharacterReferenceSo Character => _character;
        public RelativePosition Position => _position;


        public void Init(CharacterReferenceSo character, RelativePosition position)
        {
            _character = character;
            _position = position;
        }

        public override bool Equals(object obj)
        {
            return obj is ShowCharacterSo so &&
                   base.Equals(obj) &&
                   EqualityComparer<CharacterReferenceSo>.Default.Equals(_character, so._character) &&
                   EqualityComparer<RelativePosition>.Default.Equals(_position, so._position);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_character, _position);
        }

    }

}
