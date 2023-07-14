﻿using System.ComponentModel;
using System.Globalization;
using System.Net;

namespace PrintingApi;

internal class IpAddressConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        if (sourceType == typeof(string)) return true;
        return base.CanConvertFrom(context, sourceType);
    }
    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value is string)
            return IPAddress.Parse((string)value);
        return base.ConvertFrom(context, culture, value);
    }
}