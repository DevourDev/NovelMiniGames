using System;
using System.Collections;
using DevourNovelEngine.Prototype.Characters;
using DevourNovelEngine.Prototype.Core.Commands;
using TMPro;
using UnityEngine;

namespace DevourNovelEngine.Prototype.Ui
{
    public class NovelDialogPanelUi : MonoBehaviour, IDialogPanel<ShowDialogSlideSo, CharacterReferenceSo>
    {
        [SerializeField] private TextMeshProUGUI _authorText;
        [SerializeField] private TextMeshProUGUI _messageText;
        [SerializeField] private float _symbolsPerSecond = 35f;

        private Coroutine _textRevealing;


        public void Say(ShowDialogSlideSo slideData)
        {
            if (slideData.Author == null)
            {
                _authorText.text = string.Empty;
            }
            else
            {
                _authorText.text = slideData.Author.MetaInfo.Name.Get();
            }

            if (_textRevealing != null)
            {
                StopCoroutine(_textRevealing);
            }

            _textRevealing = StartCoroutine(RevealCharacters(_messageText, slideData.Text.Get()));
        }

        private IEnumerator RevealCharacters(TMP_Text textComponent, string text)
        {
            textComponent.text = text;
            textComponent.ForceMeshUpdate();

            int totalVisibleCharacters = text.Length;

            float visibleFloat = 0;
            textComponent.maxVisibleCharacters = 0;

            for (int visibleCount = 0; visibleCount < totalVisibleCharacters;)
            {
                visibleFloat += _symbolsPerSecond * Time.deltaTime;
                int tmp = visibleCount;
                visibleCount = (int)visibleFloat;

                if (tmp != visibleCount)
                    textComponent.maxVisibleCharacters = visibleCount;

                yield return null;
            }

            _textRevealing = null;
        }
    }
}
