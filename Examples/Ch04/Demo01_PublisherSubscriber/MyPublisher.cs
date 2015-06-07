using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo01_PublisherSubscriber
{
    // 宣告委派型別
    public delegate void TextChangedEventHandler(string oldValue, string newValue);

    public class MyPublisher
    {
        private string _text;

        public TextChangedEventHandler TextChanged;

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
                    TextChanged(old, _text); // 觸發事件!
                }
            }
        }
    }
}
