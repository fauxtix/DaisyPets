using System;
using System.Globalization;
using Microsoft.Maui.Controls;

public class TipoToBooleanConverter : IValueConverter
{
    // Convert method converts Tipo string to boolean for IsChecked property
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string tipo && parameter is string tipoParameter)
        {
            return tipo == tipoParameter;
        }
        return false;
    }

    // ConvertBack method converts boolean IsChecked to Tipo string
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool isChecked && isChecked && parameter is string tipoParameter)
        {
            return tipoParameter;
        }
        return null; // Ensure you handle the null case properly in your ViewModel or elsewhere
    }
}
