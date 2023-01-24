namespace Fwks.AspNetCore.Options;

public sealed class CorsOptions
{
    public const string DEFAULT_POLICY_NAME = "DefaultCorsPolicy";

    public string PolicyName { get; set; } = DEFAULT_POLICY_NAME;
    public string[] AllowedOrigins { get; set; } = new[] { "*" };
    public string[] AllowedHeaders { get; set; } = new[] { "*" };
    public string[] AllowedMethods { get; set; } = new[] { "GET", "POST", "PUT", "PATCH", "DELETE" };
}