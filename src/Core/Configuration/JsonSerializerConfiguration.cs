using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Fwks.Core.Configuration;

public static class JsonSerializerConfiguration
{
    private static JsonSerializerOptions _default;

    public static JsonSerializerOptions Instance
    {
        get
        {
            if (_default != default)
                return _default;

            ConfigureDefault();

            return _default;
        }
    }

    public static void ConfigureDefault()
    {
        _default = new()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        _default.Converters.Add(new JsonStringEnumConverter());
    }

    public static void Configure(Action<JsonSerializerOptions> optionsAction)
    {
        _default = new();

        optionsAction.Invoke(_default);

        return;
    }
}