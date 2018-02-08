using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoEnumerable
{
    class MyFonts : IEnumerable
    {
        string[] fonts = { "微軟正黑體", "思源黑體", "Consolas" };

        public IEnumerator GetEnumerator()
        {
            return new FontEnumerator(fonts);
        }
    }

    class FontEnumerator : IEnumerator
    {
        private string[] _fonts;
        private int _position = -1;

        public FontEnumerator(string[] fonts) // Constructor
        {
            _fonts = new string[fonts.Length];
            for (int i = 0; i < fonts.Length; i++)
                _fonts[i] = fonts[i];
        }
        public object Current // 實作 Current。
        {
            get
            {
                if (_position == -1)
                    throw new InvalidOperationException();
                if (_position >= _fonts.Length)
                    throw new InvalidOperationException();
                return _fonts[_position];
            }
        }
        public bool MoveNext() // 實作 MoveNext。
        {
            if (_position < _fonts.Length - 1)
            {
                _position++;
                return true;
            }
            else
                return false;
        }
        public void Reset() // 實作 Reset。
        {
            _position = -1;
        }
    }

}
