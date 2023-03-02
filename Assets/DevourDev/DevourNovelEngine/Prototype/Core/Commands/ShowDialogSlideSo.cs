using System;
using System.Collections.Generic;
using DevourDev.Unity.MultiCulture;
using DevourNovelEngine.Prototype.Characters;
using DevourNovelEngine.Prototype.Commands;
using DevourNovelEngine.Prototype.Ui;
using UnityEngine;

namespace DevourNovelEngine.Prototype.Core.Commands
{
    [CreateAssetMenu(menuName = NovelEngineConstants.MenuNameCommands + "Show Dialog Slide")]
    public class ShowDialogSlideSo : CommandSo, IDialogSlideData<CharacterReferenceSo>
    {
        [SerializeField] private CharacterReferenceSo _character;
        [SerializeField] private MultiCulturalText _text;


        public CharacterReferenceSo Author => _character;
        public MultiCulturalText Text => _text;


        public void Init(CharacterReferenceSo character, MultiCulturalText text)
        {
            _character = character;
            _text = text;
        }

        public override bool Equals(object obj)
        {
            return obj is ShowDialogSlideSo so &&
                   base.Equals(obj) &&
                   EqualityComparer<CharacterReferenceSo>.Default.Equals(_character, so._character) &&
                   EqualityComparer<MultiCulturalText>.Default.Equals(_text, so._text);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_character, _text);
        }

    }


}
