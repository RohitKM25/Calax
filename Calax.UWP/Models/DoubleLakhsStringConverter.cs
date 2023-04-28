using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Calax.UWP.Models
{
    public class DoubleLakhsStringConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value==null) return null;
            return $"{(double)value / 100000}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            string v = value as string;
            if (v == null) return null;
            double cn = double.Parse(v);
            return cn * 100000;
        }
    }
}
