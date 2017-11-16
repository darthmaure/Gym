using System;
using System.Globalization;
using Gym.Services;
using Xamarin.Forms;

namespace Gym.Converters
{
    public class CounterLimitConverter : IValueConverter
    {

        private IColorService _service = new ColorService();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                int limit =  int.Parse((parameter as Label).Text);
                int current = (int) value;
                var color = _service.Resolve(current, limit);
                return Color.FromRgb(color.Red, color.Green, color.Blue);
            }
            catch (Exception e)
            {
                return Color.Red;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
