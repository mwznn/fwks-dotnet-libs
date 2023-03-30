using System;
using Fwks.Core.Extensions;

namespace Fwks.Core.Constants;

public static class EnvironmentVariables
{
    private static string _environment;

    public static bool IsProduction()
    {
        if (_environment.IsEmpty())
            _environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        return _environment.Equals("production", StringComparison.InvariantCultureIgnoreCase);
    }
}

