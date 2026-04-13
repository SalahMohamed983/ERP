using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DominLayer.Commons
{
    public class LocalizableEntity
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }

        public string GetLocalized()
        {
            CultureInfo culture = Thread.CurrentThread.CurrentCulture;
            if (culture.TwoLetterISOLanguageName.ToLower().Equals("ar"))
                return NameAr;
            return NameEn;
        }
    }
}
