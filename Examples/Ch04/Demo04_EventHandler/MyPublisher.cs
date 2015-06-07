using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo04_EventHandler
{
    public class MyPublisher
    {
        private string _text;

        public event EventHandler TextChanged;

        protected void OnTextChanged()
        {
            if (this.TextChanged != null)
            {
                TextChanged(this, EventArgs.Empty);
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
                    OnTextChanged();     // 觸發事件!
                }
            }
        }
    }
}
