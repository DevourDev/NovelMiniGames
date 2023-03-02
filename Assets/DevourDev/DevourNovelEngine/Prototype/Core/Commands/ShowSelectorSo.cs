using System;
using System.Collections.Generic;
using DevourDev.Unity.MultiCulture;
using DevourNovelEngine.Prototype.Characters;
using DevourNovelEngine.Prototype.Commands;
using UnityEngine;

namespace DevourNovelEngine.Prototype.Core.Commands
{
    [CreateAssetMenu(menuName = NovelEngineConstants.MenuNameCommands + "Selector")]
    public class ShowSelectorSo : CommandSo
    {
        [System.Serializable]
        public sealed class SelectorTitle
        {
            [SerializeField] private CharacterReferenceSo _character;
            [SerializeField] private MultiCulturalText _text;


            public SelectorTitle(CharacterReferenceSo character, MultiCulturalText text)
            {
                _character = character;
                _text = text;
            }


            public CharacterReferenceSo Character => _character;
            public MultiCulturalText Text => _text;


            public override bool Equals(object obj)
            {
                return obj is SelectorTitle title &&
                       EqualityComparer<CharacterReferenceSo>.Default.Equals(_character, title._character) &&
                       EqualityComparer<MultiCulturalText>.Default.Equals(_text, title._text);
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(_character, _text);
            }
        }


        [System.Serializable]
        public class SelectorVariant
        {
            [SerializeField] private MultiCulturalText _text;
            [SerializeField] private Sprite _icon;
            [SerializeField] private CommandSo _action;


            public SelectorVariant(MultiCulturalText text, Sprite icon, CommandSo action)
            {
                _text = text;
                _icon = icon;
                _action = action;
            }


            public MultiCulturalText Text => _text;
            public Sprite Icon => _icon;
            public CommandSo Action => _action;


            public override bool Equals(object obj)
            {
                return obj is SelectorVariant variant &&
                       EqualityComparer<MultiCulturalText>.Default.Equals(_text, variant._text) &&
                       EqualityComparer<Sprite>.Default.Equals(_icon, variant._icon) &&
                       EqualityComparer<CommandSo>.Default.Equals(_action, variant._action);
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(_text, _icon, _action);
            }
        }


        [SerializeField] private SelectorTitle _title;
        [SerializeField] private SelectorVariant[] _variants;


        public SelectorTitle Title => _title;
        public SelectorVariant[] Variants => _variants;


        public void Init(SelectorTitle title, SelectorVariant[] variants)
        {
            _title = title;
            _variants = variants;
        }


        public override bool Equals(object obj)
        {
            return obj is ShowSelectorSo so &&
                   base.Equals(obj) &&
                   EqualityComparer<SelectorTitle>.Default.Equals(_title, so._title) &&
                   EqualityComparer<SelectorVariant[]>.Default.Equals(_variants, so._variants);
        }


        public override int GetHashCode()
        {
            return HashCode.Combine(_title, _variants);
        }

    }
}
