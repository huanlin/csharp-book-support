using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsDemo
{
    public class MyCodeTableCache
    {
        private Dictionary<int, string> _eyeColors;
        private Dictionary<string, CountryInfo> _countries;

        public MyCodeTableCache()
        {
            // 從資料庫載入幾個常用的代碼表，並存入此物件的私有欄位中。
        }

        public string GetEyeColorName(int eyeColorId)
        {
            string eyeColorName = null;
            if (_eyeColors.TryGetValue(eyeColorId, out eyeColorName))
            {
                return eyeColorName;
            }
            return String.Empty;
        }

        public CountryInfo GetCountry(string countryCode)
        {
            return _countries[countryCode]; // 如果指定的 key 不存在，會拋異常!
        }
    }

    public class CountryInfo
    {
        public string Code;
        public string Name;
        public string Currency;
    }
}
