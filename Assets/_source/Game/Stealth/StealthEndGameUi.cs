using System;
using TMPro;
using UnityEngine;

namespace Game.Stealth
{
    public class StealthEndGameUi : InitableComponent
    {
        [System.Serializable]
        private struct ResultHandler
        {
            public StealthGameManager.MiniGameResult Result;
            public string TitleText;
            public Color TitleColor;

            public string MessageText;
        }


        [SerializeField] private ResultHandler[] _resultHandlers;

        [SerializeField] private StealthGameManager _gm;
        [Space]
        [SerializeField] TextMeshProUGUI _titleText;
        [SerializeField] TextMeshProUGUI _msgText;


        public override void Init()
        {
            _gm.OnGameEnded += HandleGameEnded;
        }


        private void HandleGameEnded(StealthGameManager gm)
        {
#if UNITY_EDITOR
            UnityEngine.Debug.Log("handle game ended. result: " + gm.Result.ToString());
#endif
            ResultHandler handler;

            {
                var result = gm.Result;
                ResultHandler? handler1 = null;

                foreach (var h in _resultHandlers)
                {
                    if (h.Result == result)
                    {
                        handler1 = h;
                        break;
                    }
                }

                if (!handler1.HasValue)
                    throw new NotImplementedException($"Unable to find handler for result: {result}");

                handler = handler1.Value;
            }

            _titleText.text = handler.TitleText;
            _titleText.color = handler.TitleColor;
            _msgText.text = handler.MessageText.Replace("<secrets_count>", gm.SpiedSecretsCount.ToString());

            gameObject.SetActive(true);
        }
    }
}
