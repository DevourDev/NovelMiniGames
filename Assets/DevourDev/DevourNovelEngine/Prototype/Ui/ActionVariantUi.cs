using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DevourNovelEngine.Prototype.Ui
{
    public class ActionVariantUi : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Button _button;

        private SelectorUi _parent;
        private System.Action _callback;


        private void Awake()
        {
            _button.onClick.AddListener(HandleClick);
        }

        private void HandleClick()
        {
            _parent.Hide();
            _callback();
        }


        public void Init(SelectorUi parent, ActionVariantUiSetting setting)
        {
            Init(parent, setting.Text, setting.Callback);
        }


        public void Init(SelectorUi parent, string text, System.Action callback)
        {
            _parent = parent;
            _text.text = text;
            _callback = callback;
        }
    }
}
