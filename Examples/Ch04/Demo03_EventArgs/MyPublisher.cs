using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo03_EventArgs
{
    /// <summary>
    /// 供 TextChanged 事件使用的事件參數。
    /// </summary>
    public class TextChangedEventArgs : EventArgs
    {
        public TextChangedEventArgs(string oldValue, string newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }

        public string OldValue { get; set; }   
        public string NewValue { get; set; }   
    }

    // 宣告委派型別
    public delegate void TextChangedEventHandler(object sender, TextChangedEventArgs args);

    public class MyPublisher
    {
        private string _text;

        public event TextChangedEventHandler TextChanged;

        protected void OnTextChanged(string oldValue, string newValue)
        {
            if (this.TextChanged != null)
            {
                var args = new TextChangedEventArgs(oldValue, newValue);
                TextChanged(this, args);
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
