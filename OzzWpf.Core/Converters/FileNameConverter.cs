using System.Globalization;
using System.IO;
using System.Windows.Data;

namespace OzzWpf.Core.Converters;

[ValueConversion(typeof(string), typeof(string))]
public class FileNameConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null)
            return FallbackValue;

        if (value is string path && !string.IsNullOrEmpty(path))
        {
            return Path.GetFileName(path);
        }
        return FallbackValue;
    }

    public string FallbackValue { get; set; } = string.Empty;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotSupportedException();
}
