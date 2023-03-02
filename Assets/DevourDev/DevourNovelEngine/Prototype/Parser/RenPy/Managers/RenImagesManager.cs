using System;
using System.Collections.Generic;
using DevourDev.Unity.Utils;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace DevourNovelEngine.Prototype.Parser.Converters
{
    [CreateAssetMenu(menuName = "DevourDev/Novel Engine/Parsing/Ren/Images Manager")]
    public class RenImagesManager : ScriptableObject
    {
        [SerializeField] private Sprite _defaultSprite;
        [SerializeField] private Sprite[] _sprites;
#if UNITY_EDITOR
        [SerializeField] private bool _findInThisDirectory;
        [SerializeField] private bool _testNamesUniqueness;
#endif
        private Dictionary<string, Sprite> _dict;


        private Dictionary<string, Sprite> Dict
        {
            get
            {
                if (_dict == null)
                    InitDictionary();

                return _dict;
            }
        }

        private void InitDictionary()
        {
            var arr = _sprites;
            var len = arr.Length;
            _dict = new Dictionary<string, Sprite>(len);

            for (int i = 0; i < len; i++)
            {
                var sprite = arr[i];

                if (!_dict.TryAdd(sprite.name, sprite))
                {
#if UNITY_EDITOR
                    Debug.Log($"Image {sprite.name} ({UnityEditor.AssetDatabase.GetAssetPath(sprite)}) has duplicating name");
#endif
                }
            }
        }


#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_findInThisDirectory)
            {
                _findInThisDirectory = false;

                var path = AssetDatabase.GetAssetPath(this);
                path = EditorHelpers.DirectoryUp(path);
                var buffer = new List<Sprite>();
                //EditorHelpers.FindAssetsOfType<Sprite>(buffer);
                EditorHelpers.FindAssetsOfTypeAtPath<Sprite>(buffer, path);
                _sprites = buffer.ToArray();
                EditorUtility.SetDirty(this);
                AssetDatabase.SaveAssetIfDirty(this);
            }

            if (_testNamesUniqueness)
            {
                _testNamesUniqueness = false;
                InitDictionary();
            }
        }
#endif

        public Sprite FindByName(string name)
        {
            if (!Dict.TryGetValue(name, out var value))
                value = _defaultSprite;

            return value;
        }
    }
}
