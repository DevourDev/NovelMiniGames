using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DevourNovelEngine.Prototype.Parser.RenPy.Converters;
using DevourNovelEngine.Prototype.Parser.RenPy.Entities;
using UnityEngine;
using UnityEngine.WSA;

namespace DevourNovelEngine.Prototype.Parser
{
    public interface IToDevourNovelScriptConverter<TFrom> : IConverter<TFrom, string>
    {
    }

    internal sealed class RenDialogToDnsConverter : IToDevourNovelScriptConverter<RenShowDialogSlide>
    {
        public string Convert(RenShowDialogSlide from)
        {
            string output = string.Empty;

            if (from.Author != null)
                output += $"{from.Author.Symbol}: ";

            output += $"\"{from.Text}\"";
            return output;
        }
    }

    internal sealed class RenShowImageToDnsConverter : IToDevourNovelScriptConverter<RenShowImage>
    {
        public string Convert(RenShowImage from)
        {
            string output = "_показать ";

            output += $"\"{from.Image.Name}\"";

            switch (from.Position)
            {
                case RenPosition.AtLeft:
                    output += $" слева";
                    break;
                case RenPosition.AtRight:
                    output += $" справа";
                    break;
                default:
                    break;
            }

            return output;
        }
    }

    internal sealed class RenHideImageToDnsConverter : IToDevourNovelScriptConverter<RenHideImage>
    {
        public string Convert(RenHideImage from)
        {
            string output = $"_скрыть \"{from.Image.Name}\"";

            switch (from.Position)
            {
                case RenPosition.AtLeft:
                    output += $" слева";
                    break;
                case RenPosition.AtRight:
                    output += $" справа";
                    break;
                default:
                    break;
            }

            return output;
        }
    }

    internal sealed class RenSetBackGroundToDnsConverter : IToDevourNovelScriptConverter<RenSetBackGround>
    {
        public string Convert(RenSetBackGround from)
        {
            string output = $"_фон \"{from.Image.Name}\"";
            return output;
        }
    }

    internal sealed class RenJumpToDnsConverter : IToDevourNovelScriptConverter<RenJump>
    {
        public string Convert(RenJump from)
        {
            return ConvertJump(from);
        }

        public static string ConvertJump(RenJump renJump)
        {
            return $"_перейти на {renJump.Label.Symbol}";
        }
    }

    internal sealed class RenShowSelectorToDnsConverter : IToDevourNovelScriptConverter<RenSelector>
    {
        public string Convert(RenSelector from)
        {
            var newLine = Environment.NewLine;

            string title = null;

            if (from.Title != null)
            {
                if (from.Title.Character != null)
                    title = $"{from.Title.Character.Symbol}: ";

                title += $"\"{from.Title.Text}\"";
            }

            string output = $"_выбор:{newLine}";

            if (title != null)
                output += $"    заголовок: {title},{newLine}";

            for (int i = 0; i < from.Variants.Count; i++)
            {
                RenSelector.SelectorVariant variant = from.Variants[i];

                string commandString = RenJumpToDnsConverter.ConvertJump((RenJump)variant.Command);
                string eol = i + 1 < from.Variants.Count ? ";" + newLine : ".";

                output += $"        вариант: \"{variant.Text}\":{newLine}" +
                          $"            {commandString}{eol}";
            }

            return output;
        }
    }

    public sealed class DevourNovelScriptWriter
    {
        private readonly Dictionary<Type, IConverter> _converters;


        public DevourNovelScriptWriter()
        {
            _converters = new();

            AddConverter(new RenDialogToDnsConverter());
            AddConverter(new RenShowImageToDnsConverter());
            AddConverter(new RenHideImageToDnsConverter());
            AddConverter(new RenSetBackGroundToDnsConverter());
            AddConverter(new RenJumpToDnsConverter());
            AddConverter(new RenShowSelectorToDnsConverter());
        }


        public void AddConverter<T>(IToDevourNovelScriptConverter<T> converter)
        {
            _converters.Add(converter.FromType, converter);
        }


        public async Task WriteToFileAsync(string folder, string name, RenParser.Result results)
        {
            string filePath = Path.Combine(folder, $"{name}.DevourNovelScript");
            var fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
            var sw = new StreamWriter(fs);

            try
            {
                //write characters
                sw.WriteLine("// Characters:");
                sw.WriteLine(sw.NewLine);

                foreach (var character in results.RenChars)
                {
                    string charName = character.Name;
                    Color charColor = character.Color;
                    string charColorName = $"new Color({charColor.r:N3}, {charColor.g:N3}, {charColor.b:N3})";
                    string cmd = $"_define {character.Symbol} = new Character(\"{charName})\"," +
                        $" {charColorName})";
                    await sw.WriteLineAsync(cmd);
                }

                sw.WriteLine(sw.NewLine);

                //write labels

                foreach (var storyLine in results.RenLabels)
                {
                    await sw.WriteLineAsync($"_label {storyLine.Symbol}");
                    //write actions

                    foreach (var command in storyLine.Commands)
                    {
                        if (_converters.TryGetValue(command.GetType(), out var converter))
                            await sw.WriteLineAsync((string)converter.ConvertObject(command));
                    }
                }
            }
            finally
            {
                sw.Close();
            }
           

        }
    }
}
