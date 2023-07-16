using System.Collections;
using System.ComponentModel.Design.Serialization;
using System.ComponentModel;
using System.Globalization;

namespace LabelApi;

/// <summary>
/// Класс, для конвертирования разрешения печати, обеспечивающий корректное отображение объекта в PropertyGridView
/// </summary>
internal class DpiConverter : ExpandableObjectConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        if (sourceType == typeof(string))
        {
            return true;
        }

        return base.CanConvertFrom(context, sourceType);
    }

    public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
    {
        if (destinationType == typeof(InstanceDescriptor))
        {
            return true;
        }

        return base.CanConvertTo(context, destinationType);
    }

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value is string)
        {
            try
            {
                var str = ((string)value).Trim();

                if (str.Length == 0)
                {
                    return null;
                }

                culture ??= CultureInfo.CurrentCulture;
                string[] strings = ((string)value).Split(culture.TextInfo.ListSeparator[0]);

                if (strings.Length != 2)
                {
                    throw new ArgumentException("Can not convert '" + (string)value + $"' to type {nameof(Dpi)}");
                }

                var numbers = new int[strings.Length];
                TypeConverter typeConverter = TypeDescriptor.GetConverter(typeof(int));

                for (int i = 0; i < numbers.Length; i++)
                {
                    var converted = typeConverter.ConvertFromString(context, culture, strings[i]) ?? throw new Exception($"Conversion failed on {nameof(DpiConverter)}");
                    numbers[i] = (int)converted;
                }

                return new Dpi(numbers[0], numbers[1]);
            }
            catch
            {
                throw new ArgumentException("Can not convert '" + (string)value + $"' to type {nameof(Dpi)}");
            }
        }

        return base.ConvertFrom(context, culture, value);
    }

    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
    {
        if (destinationType == typeof(string) && value is Dpi)
        {
            var dpi = (Dpi)value;
            culture ??= CultureInfo.CurrentCulture;

            string str = string.Concat(culture.TextInfo.ListSeparator, " ");
            TypeConverter typeConverter = TypeDescriptor.GetConverter(typeof(int));
            string[] strings = new string[2];
            int i = 0;
            strings[i++] = typeConverter.ConvertToString(context, culture, dpi.X) ?? throw new Exception("Can't conver X to string");
            strings[i++] = typeConverter.ConvertToString(context, culture, dpi.Y) ?? throw new Exception("Can't conver Y to string");

            return string.Join(str, strings);
        }
        if (destinationType == typeof(InstanceDescriptor) && value is Dpi)
        {
            var dpi = (Dpi)value;
            var constructorInfo = typeof(Dpi).GetConstructor(new Type[] { typeof(int), typeof(int) });

            if (constructorInfo != null)
            {
                return new InstanceDescriptor(constructorInfo, new object[] { dpi.X, dpi.Y });
            }
        }

        return base.ConvertTo(context, culture, value, destinationType);
    }

    public override object CreateInstance(ITypeDescriptorContext? context, IDictionary? propertyValues)
    {
        if (propertyValues == null)
        {
            throw new ArgumentNullException(nameof(propertyValues));
        }

        var x = propertyValues["X"] ?? throw new Exception("X not found");
        var y = propertyValues["Y"] ?? throw new Exception("Y not found");

        return new Dpi((int)x, (int)y);
    }

    public override bool GetCreateInstanceSupported(ITypeDescriptorContext? context)
    {
        return true;
    }

    public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext? context, object value, Attribute[]? attributes)
    {
        return TypeDescriptor.GetProperties(typeof(Dpi), attributes).Sort(new string[] { "X", "Y" });
    }

    public override bool GetPropertiesSupported(ITypeDescriptorContext? context)
    {
        return true;
    }
}