using System.Collections;
using System.Collections.Generic;
using DevourNovelEngine.Prototype.MiniGamesSystem;
using UnityEngine;

namespace DevourNovelEngine.Prototype
{
    [CreateAssetMenu(menuName = "DevourDev/Novel Engine/Parsing/Mini-Games Names List")]
    public class MiniGamesParsingManager : ScriptableObject
    {
        [System.Serializable]
        public struct Entry
        {
            [SerializeField] private string _name;
            //[SerializeField] private MiniGameKeyObject
        }
    }
}
