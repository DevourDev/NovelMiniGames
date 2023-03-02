using System;
using System.Collections.Generic;
using DevourNovelEngine.Prototype.Commands;
using UnityEngine;

namespace DevourNovelEngine.Prototype.Core.Commands
{
    [CreateAssetMenu(menuName = NovelEngineConstants.MenuNameCommands + "Jump To StoryLine")]
    public class JumpToStoryLineSo : CommandSo
    {
        [SerializeField] private StoryLineSo _destination;


        public StoryLineSo Destination => _destination;



        public void Init(StoryLineSo destination)
        {
            _destination = destination;
        }


        public override bool Equals(object obj)
        {
            return obj is JumpToStoryLineSo so &&
                   base.Equals(obj) &&
                   EqualityComparer<StoryLineSo>.Default.Equals(_destination, so._destination);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_destination);
        }
    }
}
