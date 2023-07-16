using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;

namespace LabelApi;

/// <summary>
/// Класс, для конвертирования размера элемента на печати, обеспечивающий корректное отображение объекта в PropertyGridView
/// </summary>
internal class PrintingSizeConverter : ExpandableObjectConverter
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
                    throw new ArgumentException("Can not convert '" + (string)value + $"' to type {nameof(PrintingSize)}");
                }
                    
                var numbers = new float[strings.Length];
                TypeConverter typeConverter = TypeDescriptor.GetConverter(typeof(float));

                for (int i = 0; i < numbers.Length; i++)
                {
                    var converted = typeConverter.ConvertFromString(context, culture, strings[i]) ?? throw new Exception($"Conversion failed on {nameof(PrintingSizeConverter)}");
                    numbers[i] = (float)converted;
                }

                return new PrintingSize(numbers[0], numbers[1]);
            }
            catch
            {
                throw new ArgumentException("Can not convert '" + (string)value + $"' to type {nameof(PrintingSize)}");
            }
        }

        return base.ConvertFrom(context, culture, value);
    }

    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
    {
        if (destinationType == typeof(string) && value is PrintingSize)
        {
            var size = (PrintingSize)value;
            culture ??= CultureInfo.CurrentCulture;

            string str = string.Concat(culture.TextInfo.ListSeparator, " ");
            TypeConverter typeConverter = TypeDescriptor.GetConverter(typeof(float));
            string[] strings = new string[2];
            int i = 0;
            strings[i++] = typeConverter.ConvertToString(context, culture, size.Width) ?? throw new Exception("Can't conver Width to string");
            strings[i++] = typeConverter.ConvertToString(context, culture, size.Height) ?? throw new Exception("Can't conver Height to string");

            return string.Join(str, strings);
        }
        if (destinationType == typeof(InstanceDescriptor) && value is PrintingSize)
        {
            var size = (PrintingSize)value;
            var constructorInfo = typeof(PrintingSize).GetConstructor(new Type[] { typeof(float), typeof(float) });
            
            if (constructorInfo != null)
            {
                return new InstanceDescriptor(constructorInfo, new object[] { size.Width, size.Height });
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

        var width = propertyValues["Width"] ?? throw new Exception("Width not found");
        var height = propertyValues["Height"] ?? throw new Exception("Height not found");

        return new PrintingSize((float)width, (float)height);
    }

    public override bool GetCreateInstanceSupported(ITypeDescriptorContext? context)
    {
        return true;
    }

    public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext? context, object value, Attribute[]? attributes)
    {
        return TypeDescriptor.GetProperties(typeof(PrintingSize), attributes).Sort(new string[] { "Width", "Height" });
    }

    public override bool GetPropertiesSupported(ITypeDescriptorContext? context)
    {
        return true;
    }
}