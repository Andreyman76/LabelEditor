﻿using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace LabelTemplate;

internal class PrintingSizeConverter : ExpandableObjectConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        if (sourceType == typeof(String))
            return true;
        return base.CanConvertFrom(context, sourceType);
    }

    /// <summary>
    /// Returns whether this converter can convert the object to the specified type.
    /// </summary>
    /// <param name="context">context</param>
    /// <param name="destinationType">destinationType</param>
    /// <returns>value</returns>
    public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
    {
        if (destinationType == typeof(InstanceDescriptor))
            return true;
        return base.CanConvertTo(context, destinationType);
    }

    /// <summary>
    /// Converts the given value to the type of this converter.
    /// </summary>
    /// <param name="context">context</param>
    /// <param name="culture">culture</param>
    /// <param name="value">value</param>
    /// <returns>result</returns>
    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value is string)
        {
            try
            {
                var str = ((string)value).Trim();
                if (str.Length == 0)
                    return null;
                if (culture == null)
                    culture = CultureInfo.CurrentCulture;
                string[] strs = ((string)value).Split(culture.TextInfo.ListSeparator[0]);
                if (strs.Length != 2)
                    throw new ArgumentException("Can not convert '" + (string)value + $"' to type {nameof(PrintingSize)}");

                var nums = new float[strs.Length];
                TypeConverter typeConverter = TypeDescriptor.GetConverter(typeof(float));
                for (int i = 0; i < nums.Length; i++)
                    nums[i] = (float)typeConverter.ConvertFromString(context, culture, strs[i]);

                return new PrintingSize(nums[0], nums[1]);
            }
            catch
            {
                throw new ArgumentException("Can not convert '" + (string)value + $"' to type {nameof(PrintingSize)}");
            }
        }
        return base.ConvertFrom(context, culture, value);
    }

    /// <summary>
    /// Converts the given value object to the specified type.
    /// </summary>
    /// <param name="context">context</param>
    /// <param name="culture">culture</param>
    /// <param name="value">value</param>
    /// <param name="destinationType">destinationType</param>
    /// <returns>result</returns>
    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
    {
        if (destinationType == typeof(string) && value is PrintingSize)
        {
            var size = (PrintingSize)value;
            if (culture == null)
                culture = CultureInfo.CurrentCulture;

            string str = String.Concat(culture.TextInfo.ListSeparator, " ");
            TypeConverter typeConverter = TypeDescriptor.GetConverter(typeof(int));
            string[] strs = new string[2];
            int i = 0;
            strs[i++] = typeConverter.ConvertToString(context, culture, size.Width);
            strs[i++] = typeConverter.ConvertToString(context, culture, size.Height);

            return String.Join(str, strs);
        }
        if (destinationType == typeof(InstanceDescriptor) && value is PrintingSize)
        {
            var size = (PrintingSize)value;
            ConstructorInfo constructorInfo = typeof(PrintingSize).GetConstructor(new Type[] { typeof(float), typeof(float) });
            if (constructorInfo != null)
                return new InstanceDescriptor(constructorInfo, new object[] { size.Width, size.Height });
        }
        return base.ConvertTo(context, culture, value, destinationType);
    }

    public override object CreateInstance(ITypeDescriptorContext? context, IDictionary? propertyValues)
    {
        return new PrintingSize((float)propertyValues["Width"], (float)propertyValues["Height"]);
    }

    public override bool GetCreateInstanceSupported(ITypeDescriptorContext? context)
    {
        return true;
    }

    /// <summary>
    /// This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.
    /// </summary>
    /// <param name="context">context</param>
    /// <param name="value">value</param>
    /// <param name="attributes">attributes</param>
    /// <returns>result</returns>
    public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext? context, object value, Attribute[]? attributes)
    {
        return TypeDescriptor.GetProperties(typeof(PrintingSize), attributes).Sort(new string[] { "Width", "Height" });
    }

    public override bool GetPropertiesSupported(ITypeDescriptorContext? context)
    {
        return true;
    }
}