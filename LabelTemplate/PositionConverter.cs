using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace LabelTemplate;

internal class PositionConverter : ExpandableObjectConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
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
    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
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
    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
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
                    throw new ArgumentException("Can not convert '" + (string)value + "' to type Position");

                var nums = new float[strs.Length];
                TypeConverter typeConverter = TypeDescriptor.GetConverter(typeof(float));
                for (int i = 0; i < nums.Length; i++)
                    nums[i] = (float)typeConverter.ConvertFromString(context, culture, strs[i]);

                return new Position(nums[0], nums[1]);
            }
            catch
            {
                throw new ArgumentException("Can not convert '" + (string)value + "' to type SpaceSize");
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
    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, System.Type destinationType)
    {
        if (destinationType == typeof(string) && value is Position)
        {
            var position = (Position)value;
            if (culture == null)
                culture = CultureInfo.CurrentCulture;

            string str = String.Concat(culture.TextInfo.ListSeparator, " ");
            TypeConverter typeConverter = TypeDescriptor.GetConverter(typeof(int));
            string[] strs = new string[2];
            int i = 0;
            strs[i++] = typeConverter.ConvertToString(context, culture, position.X);
            strs[i++] = typeConverter.ConvertToString(context, culture, position.Y);
            
            return String.Join(str, strs);
        }
        if (destinationType == typeof(InstanceDescriptor) && value is Position)
        {
            var position = (Position)value;
            ConstructorInfo constructorInfo = typeof(Position).GetConstructor(new Type[] { typeof(int), typeof(int), typeof(int), typeof(int) });
            if (constructorInfo != null)
                return new InstanceDescriptor(constructorInfo, new object[] { position.X, position.Y });
        }
        return base.ConvertTo(context, culture, value, destinationType);
    }

    public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
    {
        return new Position((float)propertyValues["X"], (float)propertyValues["Y"]);
    }

    public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
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
    public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
    {
        return TypeDescriptor.GetProperties(typeof(Position), attributes).Sort(new string[] { "X", "Y"});
    }

    public override bool GetPropertiesSupported(ITypeDescriptorContext context)
    {
        return true;
    }
}