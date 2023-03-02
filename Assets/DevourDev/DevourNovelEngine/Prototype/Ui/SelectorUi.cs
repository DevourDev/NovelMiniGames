using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DevourNovelEngine.Prototype.Ui
{
    public class SelectorUi : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private ActionVariantUi _actionVariantPrefab;
        [SerializeField] private Transform _actionVariantsParent;


        private readonly List<GameObject> _variantsOnScreen = new();


        public void BuildSelector(string title, params ActionVariantUiSetting[] variants)
        {
            Clear();
            _title.text = title;

            foreach (var setting in variants)
            {
                var variant = Instantiate(_actionVariantPrefab, _actionVariantsParent);
                variant.Init(this, setting);
                _variantsOnScreen.Add(variant.gameObject);
            }
            gameObject.SetActive(true);
        }


        internal void Hide()
        {
            gameObject.SetActive(false);
            Clear();
        }

        private void Clear()
        {
            if (_variantsOnScreen.Count > 0)
            {
                foreach (var item in _variantsOnScreen)
                {
                    Destroy(item);
                }

                _variantsOnScreen.Clear();
            }
        }
    }
}
