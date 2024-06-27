using System.Globalization;

namespace AppExt;

public class HSLColorExtension : IMarkupExtension<Color>
{
    private double[] _hue;
    public string H
    {
        set => _hue = ParseHslValues(value, 360);
    }


    private double[] _saturation;
    public string S
    {
        set => _saturation = ParseHslValues(value, 100);
    }


    private double[] _lightness;
    public string L
    {
        set => _lightness = ParseHslValues(value, 100);
    }

    public HSLColorExtension()
    {
        _hue = [0, 0];
        _saturation = [1, 1];
        _lightness = [0.5, 0.5];
    }

    public Color ProvideValue(IServiceProvider serviceProvider)
    {
        int themeIndex = Application.Current.RequestedTheme == AppTheme.Dark ? DarkIndex : LightIndex;
        return Color.FromHsla(_hue[themeIndex], _saturation[themeIndex], _lightness[themeIndex]);
    }

    object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
    {
        return ProvideValue(serviceProvider);
    }


    private double Normalize(int value, int maxValue)
    {
        if(value < 0) value = 0;
        if(value > maxValue) value = maxValue;

        return (double)value / (double)maxValue;
    }

    const int DarkIndex = 0;
    const int LightIndex = 1;


    private double[] ParseHslValues(string hslString, int maxValue)
    {
        var values = hslString.Split(',');
        if(values.Length < 1 || values.Length > 2)
            throw new ArgumentException("HSL values must be provided in 'lightValue,darkValue' or 'sameValue' format");

        double darkValue = Normalize(int.Parse(values[DarkIndex].Trim(), CultureInfo.InvariantCulture), maxValue);
        double lightValue = values.Length == 1 ? darkValue : Normalize(int.Parse(values[LightIndex].Trim(), CultureInfo.InvariantCulture), maxValue);

        return [darkValue, lightValue];
    }
}