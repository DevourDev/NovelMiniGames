using System.Linq;
using System.Threading.Tasks;
using DevourNovelEngine.Prototype.Core;
using DevourNovelEngine.Prototype.Parser;
using UnityEngine;

namespace DevourNovelEngine.Prototype.Ui
{
    public class JitRenParser : MonoBehaviour
    {
        [SerializeField] private StoryLineManager _storyLineManager;
        [SerializeField] private RenParserSo _parserSo;


        private RenToUnityConverter.UnityParsedResult _data;


        private async void Awake()
        {
            var renData = await _parserSo.ParseToIntermediateAsync();
            _data = await _parserSo.ConvertToUnityAsync(renData);

            _storyLineManager.SetStoryLine(await GetFirstStoryLineAsync());
        }


        public async Task<StoryLineSo> GetFirstStoryLineAsync()
        {
            while (_data == null)
            {
                await Task.Yield();
            }

            return _data.StoryLines.First().Value;
        }
    }
}
