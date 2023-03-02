using System;

namespace DevourNovelEngine.Prototype.Ui
{
    public readonly struct ActionVariantUiSetting
    {
        private readonly string _text;
        private readonly System.Action _callback;


        public ActionVariantUiSetting(string text, Action callback)
        {
            _text = text;
            _callback = callback;
        }


        internal string Text => _text;
        internal System.Action Callback => _callback;
    }
}
