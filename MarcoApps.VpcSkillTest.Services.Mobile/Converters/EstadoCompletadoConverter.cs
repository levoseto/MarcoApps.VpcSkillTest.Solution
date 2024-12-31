using System.Globalization;

namespace MarcoApps.VpcSkillTest.Services.Mobile.Converters
{
    public class EstadoCompletadoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string estado)
            {
                return estado == "Completado";
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("ConvertBack no es necesario para este convertidor.");
        }
    }
}