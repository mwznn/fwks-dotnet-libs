using System;
using System.Text;
using System.Text.Json;
using Fwks.Core.Configuration;

namespace Fwks.Core.Extensions;

public static class SerializationExtensions
{
    public static string SerializeObject(this object target, Action<JsonSerializerOptions> optionsAction = default)
    {
        return JsonSerializer.Serialize(target, BuildOptions(optionsAction));
    }

    public static string SerializeObject(this object target, JsonSerializerOptions options)
    {
        return JsonSerializer.Serialize(target, options);
    }

    public static byte[] SerializeToBytes(this object target, Encoding encoding = default, Action<JsonSerializerOptions> optionsAction = default)
    {
        return (encoding ??= Encoding.UTF8).GetBytes(target.SerializeObject(optionsAction));
    }

    public static byte[] SerializeToBytes(this object target, JsonSerializerOptions options = default)
    {
        return target.SerializeToBytes(Encoding.UTF8, x => x = options);
    }

    public static byte[] SerializeToBytes(this object target, Encoding encoding, JsonSerializerOptions options)
    {
        return target.SerializeToBytes(encoding, x => x = options);
    }

    public static T DeserializeObject<T>(this string source, Action<JsonSerializerOptions> optionsAction = default)
    {
        return source.DeserializeObject<T>(BuildOptions(optionsAction));
    }

    public static T DeserializeObject<T>(this string source, JsonSerializerOptions options)
    {
        return JsonSerializer.Deserialize<T>(source, options);
    }

    public static T DeserializeObject<T>(this ReadOnlyMemory<byte> source, Encoding encoding = default, Action<JsonSerializerOptions> optionsAction = default)
    {
        return source.ToArray().DeserializeObject<T>(encoding, BuildOptions(optionsAction));
    }

    public static T DeserializeObject<T>(this byte[] source, Encoding encoding = default, JsonSerializerOptions options = default)
    {
        return JsonSerializer.Deserialize<T>((encoding ?? Encoding.UTF8).GetString(source), options);
    }

    private static JsonSerializerOptions BuildOptions(Action<JsonSerializerOptions> optionsAction = default)
    {
        if (optionsAction == default)
            return JsonSerializerConfiguration.Instance;

        JsonSerializerOptions options = new();

        optionsAction.Invoke(options);

        return options;
    }
}