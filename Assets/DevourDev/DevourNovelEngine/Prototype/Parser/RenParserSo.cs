using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DevourDev.Unity.MultiCulture;
using DevourDev.Unity.Utils;
using DevourNovelEngine.Prototype.Characters;
using DevourNovelEngine.Prototype.Core;
using DevourNovelEngine.Prototype.Core.Commands;
using DevourNovelEngine.Prototype.Parser.Converters;
using DevourNovelEngine.Prototype.Parser.RenPy.Converters;
using DevourNovelEngine.Prototype.Parser.RenPy.Entities;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace DevourNovelEngine.Prototype.Parser
{


    [CreateAssetMenu(menuName = "DevourDev/Novel Engine/Parsers/RenParser")]
    public sealed class RenParserSo : ScriptableObject
    {
        [System.Serializable]
        private enum ParseMode
        {
            RenSyntaxCheck,
            ParseToUnity,
            ParseToFile
        }


        [SerializeField] private CultureObject _culture;
        [SerializeField] private RenImagesManager _renImagesManager;

        [SerializeField] private string _renScriptFullPath;
        [SerializeField] private bool _selectRenScript;

        [SerializeField] private string _outputFullPath;
        [SerializeField] private bool _selectOutputFolder;

        [SerializeField] private ParseMode _parseMode;
        [SerializeField] private bool _startParsing;
        [SerializeField] private string _parserMessage;

        private RenParser _renParser;
        private bool _parsingInProgress;


        [ContextMenu("Reset Parsing In Progress State")]
        private void ResetParsingInProgress()
        {
            _parsingInProgress = false;
        }


#if UNITY_EDITOR
        private async void OnValidate()
        {
            if (_selectRenScript)
            {
                _selectRenScript = false;
                _renScriptFullPath = EditorUtility.OpenFilePanel("Select RenPy Script", Environment.CurrentDirectory, "rpy");
                EditorUtility.SetDirty(this);
                AssetDatabase.SaveAssetIfDirty(this);
            }

            if (_selectOutputFolder)
            {
                _selectOutputFolder = false;
                _outputFullPath = EditorUtility.OpenFolderPanel("Select Output Folder", Environment.CurrentDirectory, "");
                EditorUtility.SetDirty(this);
                AssetDatabase.SaveAssetIfDirty(this);
            }

            if (_startParsing)
            {
                _startParsing = false;

                if (_parsingInProgress)
                {
                    _parserMessage = "parsing in progress!";
                }
                else
                {
                    await StartParsingAsync();
                }
            }
        }
#endif

#if UNITY_EDITOR
        private CultureObject GetRussianCulture()
        {
            List<CultureObject> cultureObjects = new();
            EditorHelpers.FindAssetsOfType<CultureObject>(cultureObjects);
            return cultureObjects.First((x) => x.MetaInfo.Name.Get() == "Русский Россия");
        }
#endif

        public async Task<RenParser.Result> ParseToIntermediateAsync()
        {
            _renParser = new();
            RenParser.Result results = await _renParser.ParseAsync(_renScriptFullPath);
            return results;
        }
#if UNITY_EDITOR
        private async Task StartParsingAsync()
        {
            _parsingInProgress = true;

            try
            {

                if (_culture == null)
                    _culture = GetRussianCulture();

                Parser.OnLog = (msg) =>
                {
                    _parserMessage = msg;
                    Debug.Log($"PARSER:: {msg}");
                };

                RenParser.Result results = await ParseToIntermediateAsync();
                _parserMessage = "converting results...";

                switch (_parseMode)
                {
                    case ParseMode.ParseToUnity:
                        var convertedData = await ConvertToUnityAsync(results);
                        var savedAssets = await SaveConvertedAssets(convertedData.Characters, convertedData.StoryLines);
                        SetAllDirty(savedAssets, true, true);
                        Debug.Log("All saved!");
                        break;
                    case ParseMode.ParseToFile:
                        var devNovScriptWriter = new DevourNovelScriptWriter();
                        await devNovScriptWriter.WriteToFileAsync(_outputFullPath, "WhoAmI", results);
                        Debug.Log("Writing file finished!");
                        return;
                    case ParseMode.RenSyntaxCheck:
                        break;
                    default:
                        break;
                }

            }
            finally
            {
                _parsingInProgress = false;
            }
        }

        public async Task<RenToUnityConverter.UnityParsedResult> ConvertToUnityAsync(RenParser.Result renData)
        {
            RenToUnityConverter renToUnityConverter = new();
            International.SetCurrentCulture(_culture);
            var convertedResults = await renToUnityConverter.ConvertResultsAsync(renData, _renImagesManager);
            return convertedResults;
        }
#endif


#if UNITY_EDITOR
        private void PseduoSaveAssets(Dictionary<RenCharacter, CharacterReferenceSo> convertedCharacters,
                                               Dictionary<RenLabel, StoryLineSo> convertedStoryLines)
        {
            string path = _outputFullPath + '/' + "pseudo output";
            path = EnsurePathIsAssetsRelative(path);
            EnsureUnityFolder(path);

            var sl = convertedStoryLines.First().Value;
            AssetDatabase.CreateAsset(sl, path + '/' + "storyLine.asset");
        }
#endif

#if UNITY_EDITOR
        private async Task<IEnumerable<UnityEngine.Object>> SaveConvertedAssets(Dictionary<RenCharacter, CharacterReferenceSo> convertedCharacters,
                                               Dictionary<RenLabel, StoryLineSo> convertedStoryLines)
        {
            //PseduoSaveAssets(convertedCharacters, convertedStoryLines);
            //return;

            List<UnityEngine.Object> savedAssets = new();

            string outputFolder = EnsurePathIsAssetsRelative(_outputFullPath);

            if (outputFolder[^1] == '/')
                outputFolder = outputFolder[..^1];

            long ts = System.Diagnostics.Stopwatch.GetTimestamp();
            string relPath = outputFolder + '/' + "Characters";
            EnsureUnityFolder(relPath);
            int i = 0;

            foreach (var character in convertedCharacters)
            {
                SaveAsset(relPath, $"{i++}.{character.Key}.asset", character.Value);
                savedAssets.Add(character.Value);

                long stamp = System.Diagnostics.Stopwatch.GetTimestamp();
                if (stamp - ts > 1_000_000)
                {
                    ts = stamp;
                    _parserMessage = $"{i + 1} characters saved";
                    await Task.Yield();
                }
            }

            Debug.Log("Characters saved!");

            ts = System.Diagnostics.Stopwatch.GetTimestamp();
            relPath = outputFolder + '/' + "StoryLines";
            EnsureUnityFolder(relPath);
            i = 0;

            foreach (var sl in convertedStoryLines)
            {
                string slFolder = relPath + '/' + FixName($"{i}.{sl.Key}");
                EnsureUnityFolder(slFolder);
                SaveAsset(slFolder, $"{i++}.{FixName(sl.Key.ToString())}.asset", sl.Value);
                savedAssets.Add(sl.Value);

                long stamp = System.Diagnostics.Stopwatch.GetTimestamp();
                if (stamp - ts > 10_000_000)
                {
                    ts = stamp;
                    _parserMessage = $"{i + 1} story lines saved";
                    await Task.Yield();
                }
            }

            ts = System.Diagnostics.Stopwatch.GetTimestamp();
            i = 0;
            foreach (var sl in convertedStoryLines)
            {
                string slFolder = relPath + '/' + FixName($"{i}.{sl.Key}");
                string commandsPath = slFolder + '/' + "Commands";
                EnsureUnityFolder(commandsPath);

                int comId = 0;

                foreach (var command in sl.Value)
                {
                    if (command is ShowSelectorSo showSelector)
                    {
                        int varActionId = 0;
                        foreach (var variant in showSelector.Variants)
                        {
                            SaveAsset(commandsPath, $"{comId}.{varActionId++}.{variant.Action.GetType().Name}.asset", variant.Action);
                            savedAssets.Add(variant.Action);
                        }
                    }

                    SaveAsset(commandsPath, $"{comId++}.{command.GetType().Name}.asset", command);
                    savedAssets.Add(command);
                }

                ++i;
                long stamp = System.Diagnostics.Stopwatch.GetTimestamp();
                if (stamp - ts > 10_000_000)
                {
                    ts = stamp;
                    _parserMessage = $"{i + 1} story lines saved";
                    await Task.Yield();
                }
            }

            return savedAssets;
        }
#endif

#if UNITY_EDITOR
        public static void SetAllDirty(IEnumerable<UnityEngine.Object> assets, bool isAssetCheck, bool saveAfter)
        {
            foreach (var ass in assets)
            {
                if (isAssetCheck)
                {
                    if (!AssetDatabase.Contains(ass))
                    {
                        throw new Exception($"asset + {ass} is not an asset!");
                    }
                }

                EditorUtility.SetDirty(ass);
            }

            if (saveAfter)
            {
                foreach (var ass in assets)
                {
                    AssetDatabase.SaveAssetIfDirty(ass);
                }
            }
        }
#endif

        public string FixName(string rawName)
        {
            return rawName.Replace('/', '_')
                .Replace('\\', '_')
                .Replace(':', '_')
                .Replace('?', '_')
                .Replace('|', '_')
                .Replace('<', '_')
                .Replace('>', '_')
                .Replace('*', '_')
                .Replace('"', '_');
        }



#if UNITY_EDITOR
        private async Task SaveAssetsAsync(string savePathAbsolute, IEnumerable<UnityEngine.Object> assets)
        {
            EnsureFolder(savePathAbsolute);
            var relPath = EnsurePathIsAssetsRelative(savePathAbsolute);
            int i = 0;

            long ts = System.Diagnostics.Stopwatch.GetTimestamp();
            foreach (var asset in assets)
            {
                string assetName = $"{i}.asset";
                SaveAsset(relPath, assetName, asset);

                long stamp = System.Diagnostics.Stopwatch.GetTimestamp();
                if (stamp - ts > 1_000_000)
                {
                    ts = stamp;
                    await Task.Yield();
                }
            }
        }
#endif

#if UNITY_EDITOR
        public static void SaveAsset(string parentFolder, string assetName, UnityEngine.Object asset)
        {
            AssetDatabase.CreateAsset(asset, parentFolder + '/' + assetName);
        }
#endif
#if UNITY_EDITOR
        public static void EnsureUnityFolder(string path)
        {
            path = EnsurePathIsAssetsRelative(path);

            if (AssetDatabase.IsValidFolder(path))
                return;

            BuildUnityDirectoryRecursive(path);
        }
#endif
#if UNITY_EDITOR
        private static void BuildUnityDirectoryRecursive(string relativePath)
        {
            Stack<string> stack = new();
            string path = relativePath;

            while (!AssetDatabase.IsValidFolder(path))
            {
                stack.Push(path);
                path = DirectoryUp(path, true);
            }

            while (stack.TryPop(out path))
            {
                string guid = CreateUnityFolder(path);
            }
        }
#endif
#if UNITY_EDITOR
        public static string CreateUnityFolder(string path)
        {
            TrySplitDirectoryToParentAndChild(path, out var parent, out var child);
            return AssetDatabase.CreateFolder(parent, child);
        }
#endif
        public static string DirectoryUp(string path, bool trimTrailingSeparator)
        {
            string p;
            if (path[^1] == '/')
                p = path[..(path.LastIndexOf('/', path.Length - 2) + 1)];
            else
                p = path[..(path.LastIndexOf('/') + 1)];

            if (trimTrailingSeparator)
                p = p.TrimEnd('/');

            return p;
        }

        public static bool TrySplitDirectoryToParentAndChild(string path, out string parentPath, out string childName)
        {
            int startIndex = path[^1] == '/' ? path.Length - 2 : path.Length - 1;
            int separation = path.LastIndexOf('/', startIndex);

            if (separation <= 0)
            {
                parentPath = null;
                childName = null;
                return false;
            }

            parentPath = path[..separation];
            int childStart = separation + 1;

            if (path[^1] == '/')
                childName = path[childStart..(^1)];
            else
                childName = path[childStart..];

            return true;
        }

#if UNITY_EDITOR
        public static void EnsureFolder(string absPath)
        {
            if (Directory.Exists(absPath))
                return;

            Directory.CreateDirectory(absPath);
            AssetDatabase.Refresh();
        }
#endif
#if UNITY_EDITOR
        public static string EnsurePathIsAssetsRelative(string path)
        {
            if (path.StartsWith("Assets"))
                return path;

            return path[path.LastIndexOf("Assets")..].Replace('\\', '/');
        }
#endif
    }


}
