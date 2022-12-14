using System;

namespace Fwks.Core.Extensions;

public static class EnumExtensions
{
    public static TEnum ToEnum<TEnum>(this string value) where TEnum : struct
    {
        if (!Enum.TryParse(value, out TEnum enumValue))
            throw new InvalidCastException($"{value} is not a valid value for this enum.");

        return enumValue;
    }
}
