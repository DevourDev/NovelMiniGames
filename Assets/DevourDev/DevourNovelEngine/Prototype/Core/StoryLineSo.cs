using System;
using System.Collections;
using System.Collections.Generic;
using DevourDev.Unity;
using DevourNovelEngine.Prototype.Commands;
using DevourNovelEngine.Prototype.Core.Commands;
using UnityEngine;

namespace DevourNovelEngine.Prototype.Core
{
    [CreateAssetMenu(menuName = NovelEngineConstants.MenuNameRoot + "Story Line")]
    public sealed class StoryLineSo : ScriptableObject, IStoryLine, IEnumerable<CommandSo>
    {
        [SerializeField] private MetaInfo _metaInfo;
        [SerializeField] private CommandSo[] _actions; //todo change to list


        public MetaInfo MetaInfo => _metaInfo;

        public int ActionsCount => _actions == null ? 0 : _actions.Length;

        public CommandSo this[int index] => _actions[index];


        public void Init(MetaInfo metaInfo,CommandSo[] actions)
        {
            _metaInfo = metaInfo;
            _actions = actions;
        }


        public void Init(CommandSo[] actions)
        {
            _actions = actions;
        }


        


        public CommandSo GetAction(int index)
        {
            return _actions[index];
        }

        public override bool Equals(object obj)
        {
            return obj is StoryLineSo so &&
                   base.Equals(obj) &&
                   EqualityComparer<MetaInfo>.Default.Equals(_metaInfo, so._metaInfo) &&
                   EqualityComparer<CommandSo[]>.Default.Equals(_actions, so._actions);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_metaInfo, _actions);
        }

        public IEnumerator<CommandSo> GetEnumerator()
        {
            var arr = _actions;
            var length = ActionsCount;

            for (int i = 0; i < length; i++)
            {
                yield return arr[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            var arr = _actions;
            var length = ActionsCount;

            for (int i = 0; i < length; i++)
            {
                yield return arr[i];
            }
        }
    }
}
