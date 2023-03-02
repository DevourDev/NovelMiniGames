using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevourDev.Unity.MultiCulture;
using DevourNovelEngine.Prototype.Characters;
using DevourNovelEngine.Prototype.Core;
using DevourNovelEngine.Prototype.Parser.Converters;
using DevourNovelEngine.Prototype.Parser.RenPy.Analyzers;
using DevourNovelEngine.Prototype.Parser.RenPy.Analyzers.Definables;
using DevourNovelEngine.Prototype.Parser.RenPy.Converters;
using DevourNovelEngine.Prototype.Parser.RenPy.Entities;
using DevourNovelEngine.Prototype.Parser.RenPy.Managers;

namespace DevourNovelEngine.Prototype.Parser
{
    public class RenToUnityConverter
    {
        public class UnityParsedResult
        {
            public readonly Dictionary<RenCharacter, CharacterReferenceSo> Characters;
            public readonly Dictionary<RenLabel, StoryLineSo> StoryLines;

            public UnityParsedResult(Dictionary<RenCharacter, CharacterReferenceSo> item1, Dictionary<RenLabel, StoryLineSo> item2)
            {
                Characters = item1;
                StoryLines = item2;
            }

            public override bool Equals(object obj)
            {
                return obj is UnityParsedResult other &&
                       EqualityComparer<Dictionary<RenCharacter, CharacterReferenceSo>>.Default.Equals(Characters, other.Characters) &&
                       EqualityComparer<Dictionary<RenLabel, StoryLineSo>>.Default.Equals(StoryLines, other.StoryLines);
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Characters, StoryLines);
            }
        }

        public async Task<UnityParsedResult> ConvertResultsAsync(RenParser.Result results, RenImagesManager renImagesManager)
        {
            Dictionary<RenCharacter, CharacterReferenceSo> convertedCharacters = new();

            RenCharacterConverter renCharConverter = new();

            foreach (var renChar in results.RenChars)
            {
                convertedCharacters.Add(renChar, renCharConverter.Convert(renChar));
            }

            Dictionary<RenLabel, StoryLineSo> convertedStoryLines = new();

            var storyLineConverter = new RenLabelConverter();

            foreach (var renLabel in results.RenLabels)
            {
                convertedStoryLines.Add(renLabel, storyLineConverter.Convert(renLabel));
            }


            Dictionary<Type, IConverter> converters = new();
            IConverter renDialogConverter = new RenDialogSlideConverter(convertedCharacters);
            RenJumpConverter renJumpConverter = new(convertedStoryLines);
            IConverter renSelectorConverter = new RenSelectorConverter(convertedCharacters, renJumpConverter);

            //NEW
            IConverter renReturnConverter = new RenReturnConverter();

            RenImageConverter renImageConverter = new(renImagesManager);

            IConverter renShowImageConverter = new RenShowImageConverter(renImageConverter);
            IConverter renHideImageConverter = new RenHideImageConverter(renImageConverter);
            IConverter renSetBgConverter = new RenSetBackGroundConverter(renImageConverter);

            converters.Add(renDialogConverter.FromType, renDialogConverter);
            converters.Add(((IConverter)renJumpConverter).FromType, renJumpConverter);
            converters.Add(renSelectorConverter.FromType, renSelectorConverter);

            converters.Add(renReturnConverter.FromType, renReturnConverter);
            converters.Add(renShowImageConverter.FromType, renShowImageConverter);
            converters.Add(renHideImageConverter.FromType, renHideImageConverter);
            converters.Add(renSetBgConverter.FromType, renSetBgConverter);


            StoryLineFromRenLabelInitializer storyLineInitializer = new(converters);

            long ts = System.Diagnostics.Stopwatch.GetTimestamp();

            foreach (var kvp in convertedStoryLines)
            {
                storyLineInitializer.InitStoryLine(kvp.Value, kvp.Key);

                long stamp = System.Diagnostics.Stopwatch.GetTimestamp();
                if (stamp - ts > 1_000_000)
                {
                    ts = stamp;
                    await Task.Yield();
                }
            }

            UnityEngine.Debug.Log("convertion finished!");

            return new(convertedCharacters, convertedStoryLines);
        }
    }
    public class RenParser
    {
        public class Result
        {
            private readonly RenCharacter[] _renChars;
            private readonly RenLabel[] _renLabels;


            public Result(RenCharacter[] renChars, RenLabel[] renLabels)
            {
                _renChars = renChars;
                _renLabels = renLabels;
            }


            public RenCharacter[] RenChars => _renChars;
            public RenLabel[] RenLabels => _renLabels;
        }


        public async Task<Result> ParseAsync(string docPath)
        {
            var lines = await DocLines.FromFileAsync(docPath);

            var renCharsCollection = new RenCharactersCollection();
            var renLabelsCollection = new RenLabelsCollection();

            var definablesAnalyzerComposite = new DefinableEntitiesAnalyzer();

            var definableCharAnalyzer = new DefinableCharacterAnalyzer(renCharsCollection);

            definablesAnalyzerComposite.AddDefinableAnalyzer(definableCharAnalyzer);

            var fullCommentedSkipper = new FullCommentedLinesSkipper();

            var jumpToLabelAnalyzer = new RenJumpToLabelAnalyzer(renLabelsCollection);

            var dialogSlideAnalyzer = new DialogSlideAnalyzer(renLabelsCollection, renCharsCollection);

            var labelAnalyzer = new LabelAnalyzer(renLabelsCollection);

            var selectorAnalyzer = new SelectorAnalyzer(renLabelsCollection, renCharsCollection);

            var showImgAnalyzer = new RenShowImageAnalyzer(renLabelsCollection);

            var hideImgAnalyzer = new RenHideImageAnalyzer(renLabelsCollection);

            var returnAnalyzer = new RenReturnAnalyzer(renLabelsCollection);

            var setBgAnalyzer = new RenSetBackGroundAnalyzer(renLabelsCollection);

            var singleQuotesDetector = new SingleSymbolDetector('"');

            await Parser.ParseAsync(lines, fullCommentedSkipper,
                definablesAnalyzerComposite, jumpToLabelAnalyzer,
                labelAnalyzer, selectorAnalyzer, dialogSlideAnalyzer,
                showImgAnalyzer, hideImgAnalyzer, setBgAnalyzer, returnAnalyzer, singleQuotesDetector);

            return new Result(renCharsCollection.ToArray(), renLabelsCollection.ToArray());
        }

    }

    public static class ParsingHelpers
    {
        public const char Quotes = '"';
        public const char Space = ' ';
        public const char Colon = ':';
        public const string Tabulation4 = "    ";



        //public static string Between(string text, int start, char )
        public static string FromFirstToLast(string text, int start, char bounds, out int lastBoundIndex)
        {
            return FromFirstToLast(text, start, bounds, bounds, out lastBoundIndex);
        }

        public static string FromFirstToLast(string text, int start, char leftBound, char rightBound, out int rightBoundIndex)
        {
            start = text.IndexOf(leftBound, start) + 1;
            rightBoundIndex = text.LastIndexOf(rightBound);
            return text[start..rightBoundIndex];
        }

        public static string FromFirstToFirst(string text, int start, char leftBound, char rightBound, out int rightBoundIndex)
        {
            start = text.IndexOf(leftBound, start) + 1;
            rightBoundIndex = text.IndexOf(rightBound, start);
            return text[start..rightBoundIndex];
        }
        public static string FromToFirst(string text, int start, char rightBound, out int rightBoundIndex)
        {
            var span = text.AsSpan(start);
            var length = span.Length;

            for (int i = 0; i < length; i++)
            {
                var c = span[i];

                if (c == rightBound)
                {
                    rightBoundIndex = i + start;
                    return text[start..rightBoundIndex];
                }
            }

            rightBoundIndex = -1;
            return null;
        }

        public static bool StartsWithSkippingSpace(string text, string symbol, out int startIndex)
        {
            return StartsWith(text, symbol, Space, out startIndex);
        }

        public static bool StartsWith(string text, string symbol, char skippingSymbol, out int startIndex)
        {
            var span = text.AsSpan();
            var length = span.Length;

            int left = symbol.Length;

            for (int i = 0; i < length; i++)
            {
                if (span[i] == symbol[^left])
                {
                    --left;

                    if (left > 0)
                        continue;

                    startIndex = i - symbol.Length + 1;
                    return true;
                }

                if (span[i] == skippingSymbol)
                    continue;

                break;
            }

            startIndex = -1;
            return false;
        }


        public static bool EndsWithSkippingSpace(string text, string symbol, out int startIndex)
        {
            return EndsWith(text, symbol, Space, out startIndex);
        }

        public static bool EndsWith(string text, string symbol, char skippingSymbol, out int startIndex)
        {
            var span = text.AsSpan();
            var length = span.Length;

            int left = symbol.Length;

            for (int i = length - 1; i >= 0; i--)
            {
                if (span[i] == symbol[--left])
                {
                    if (left > 0)
                        continue;

                    startIndex = i;
                    return true;
                }

                if (span[i] == skippingSymbol)
                    continue;

                break;
            }

            startIndex = -1;
            return false;
        }


        public static string TextInBounds(string text, char boundingSymbol, int minLength, out int endIndex)
        {
            return TextInBounds(text, 0, boundingSymbol, boundingSymbol, minLength, out endIndex);
        }

        public static string TextInBounds(string text, int start, char leftBound, char rightBound, int minLength, out int endIndex, bool eolIsRightBound = false)
        {
            var span = text.AsSpan(start);
            var length = span.Length;

            int startIndex = -1;
            endIndex = -1;

            for (int i = 0; i < length; i++)
            {
                var c = span[i];

                if (startIndex < 0 || i - startIndex <= minLength)
                {
                    if (c == leftBound)
                    {
                        startIndex = i;
                        continue;
                    }
                }

                if (c == rightBound)
                {
                    endIndex = i + start;
                    break;
                }
            }

            if (endIndex < 0 && eolIsRightBound)
                endIndex = text.Length;

            if (startIndex < 0 || endIndex < 0)
                return null;

            return new string(span[(startIndex + 1)..(endIndex - start)]);
        }


        public static int TabulationsCount(string text, out int lastTabulationLastIndex)
        {
            var span = text.AsSpan();
            var length = span.Length;
            int i = 0;

            for (; i < length; i++)
            {
                if (span[i] != Space)
                {
                    break;
                }
            }

            lastTabulationLastIndex = i - 1;
            return i / Tabulation4.Length;
        }
    }
}
