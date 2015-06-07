using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo02_Event
{
    // 宣告委派型別
    public delegate void TextChangedEventHandler(string oldValue, string newValue);

    public class MyPublisher
    {
        private string _text;

        public event TextChangedEventHandler TextChanged;

        protected void OnTextChanged(string oldValue, string newValue)
        {
            if (this.TextChanged != null)
            {
                TextChanged(oldValue, newValue);
            }
        }


        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                if (_text != value)
                {
                    string old = _text;
                    _text = value;
                    OnTextChanged(old, _text); // 觸發事件!
                }
            }
        }
    }
}
