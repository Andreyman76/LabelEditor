using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;

namespace LabelApi;

/// <summary>
/// Класс, для конвертирования позиции элемента на печати, обеспечивающий корректное отображение объекта в PropertyGridView
/// </summary>
internal class PrintingPositionConverter : ExpandableObjectConverter
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
        if (value is string valueAsString)
        {
            try
            {
                var str = valueAsString.Trim();

                if (str.Length == 0)
                {
                    return null;
                }

                culture ??= CultureInfo.CurrentCulture;
                string[] strings = valueAsString.Split(culture.TextInfo.ListSeparator[0]);

                if (strings.Length != 2)
                {
                    throw new ArgumentException("Can not convert '" + valueAsString + $"' to type {nameof(PrintingPosition)}");
                }

                var numbers = new float[strings.Length];
                TypeConverter typeConverter = TypeDescriptor.GetConverter(typeof(float));

                for (int i = 0; i < numbers.Length; i++)
                {
                    var converted = typeConverter.ConvertFromString(context, culture, strings[i]) ?? throw new Exception($"Conversion failed on {nameof(PrintingPositionConverter)}");
                    numbers[i] = (float)converted;
                }

                return new PrintingPosition(numbers[0], numbers[1]);
            }
            catch
            {
                throw new ArgumentException("Can not convert '" + valueAsString + $"' to type {nameof(PrintingPosition)}");
            }
        }

        return base.ConvertFrom(context, culture, value);
    }

    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
    {
        if (destinationType == typeof(string) && value is PrintingPosition valueAsPrintingPosition1)
        {
            var position = valueAsPrintingPosition1;
            culture ??= CultureInfo.CurrentCulture;

            string str = string.Concat(culture.TextInfo.ListSeparator, " ");
            TypeConverter typeConverter = TypeDescriptor.GetConverter(typeof(float));
            string[] strings = new string[2];
            int i = 0;
            strings[i++] = typeConverter.ConvertToString(context, culture, position.X) ?? throw new Exception("Can't conver X to string");
            strings[i++] = typeConverter.ConvertToString(context, culture, position.Y) ?? throw new Exception("Can't conver Y to string");

            return string.Join(str, strings);
        }
        if (destinationType == typeof(InstanceDescriptor) && value is PrintingPosition valueAsPrintingPosition2)
        {
            var position = valueAsPrintingPosition2;
            var constructorInfo = typeof(PrintingPosition).GetConstructor(new Type[] { typeof(float), typeof(float) });

            if (constructorInfo != null)
            {
                return new InstanceDescriptor(constructorInfo, new object[] { position.X, position.Y });
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

        return new PrintingPosition((float)x, (float)y);
    }

    public override bool GetCreateInstanceSupported(ITypeDescriptorContext? context)
    {
        return true;
    }

    public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext? context, object value, Attribute[]? attributes)
    {
        return TypeDescriptor.GetProperties(typeof(PrintingPosition), attributes).Sort(new string[] { "X", "Y" });
    }

    public override bool GetPropertiesSupported(ITypeDescriptorContext? context)
    {
        return true;
    }
}