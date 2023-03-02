using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace DevourDev.Unity.Helpers
{
    public static class AnimationHelpers
    {
        public static AnimationCurve CurveStartEnd(float startV, float endV)
        {
            return new AnimationCurve(new Keyframe(0, startV), new Keyframe(1f, endV));
        }
    }
    public static class ExternalConfigHelpers
    {
        private const string _logsFileName = "configLogs.txt";

        public static void Log(string msg)
        {
            using var writer = new StreamWriter(_logsFileName, true);
            writer.WriteLine($"{System.DateTime.Now}: {msg}{Environment.NewLine}");
        }

        public static bool TryOpenConfig<T>(string fileName, out T deserialized)
        {
            FileStream fs = File.Open(fileName, FileMode.OpenOrCreate, FileAccess.Read);
            var reader = new StreamReader(fs);
            try
            {
                XmlSerializer serializer = new(typeof(T));
                var x = serializer.Deserialize(fs);
                deserialized = (T)x;
                return true;
            }
            catch (Exception ex)
            {
                Log($"unable to open config {fileName}: {ex}");
                deserialized = default;
                return false;
            }
            finally
            {
                reader.Close();
            }
        }

        public static bool TryWriteConfig<T>(string path, T config)
        {
            FileStream fs = File.Create(path);
            try
            {
                XmlSerializer serializer = new(typeof(T));
                serializer.Serialize(fs, config);
                return true;
            }
            catch (Exception ex)
            {
                Log($"unable to write config {path}: {ex.Message}");
                return false;
            }
            finally
            {
                fs.Close();
            }
        }
    }
    public static class CachingAccessors
    {
        private static readonly Dictionary<Type, Component> _dict = new();


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Clear()
        {
            _dict.Clear();
        }


        public static T Get<T>() where T : UnityEngine.Component
        {
            //return (T)GameObject.FindObjectOfType(typeof(T));

            if (!_dict.TryGetValue(typeof(T), out var x))
            {
                x = (Component)GameObject.FindObjectOfType(typeof(T));

                if (x != null)
                {
                    _dict.Add(typeof(T), x);
                }
            }

            if (x == null)
                x = (Component)GameObject.FindObjectOfType(typeof(T));

            if (x == null)
                _dict.Remove(typeof(T));
            else
                _dict[typeof(T)] = x;

            return (T)x;
        }
    }
}
