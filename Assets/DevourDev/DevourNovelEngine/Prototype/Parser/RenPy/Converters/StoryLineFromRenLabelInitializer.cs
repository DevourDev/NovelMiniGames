using System;
using System.Collections.Generic;
using DevourNovelEngine.Prototype.Core;
using DevourNovelEngine.Prototype.Core.Commands;
using DevourNovelEngine.Prototype.Parser.RenPy.Entities;

namespace DevourNovelEngine.Prototype.Parser.RenPy.Converters
{
    public sealed class StoryLineFromRenLabelInitializer
    {
        private readonly IReadOnlyDictionary<Type, IConverter> _converters;


        public StoryLineFromRenLabelInitializer(IReadOnlyDictionary<Type, IConverter> converters)
        {
            _converters = converters;
        }


        public void InitStoryLine(StoryLineSo emptyStoryLine, RenLabel from)
        {
            var renComms = from.Commands;
            var length = renComms.Count;
            CommandSo[] commands = new CommandSo[length];

            for (int i = 0; i < length; i++)
            {
                var renComm = renComms[i];
                var converted = (CommandSo)_converters[renComm.GetType()].ConvertObject(renComm);

                if (converted == null)
                    UnityEngine.Debug.LogError($"converted command is null! {i}, {from.Commands[i]}");

                commands[i] = converted;
            }

            emptyStoryLine.Init(commands);
        }
    }
}
