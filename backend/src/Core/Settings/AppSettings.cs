namespace Fwks.FwksService.Core.Settings;

public sealed class AppSettings
{
    public AuthServerSettings AuthServer { get; set; }
    public CorsSettings Cors { get; set; }
    public StorageSettings Storage { get; set; }
    public SecuritySettings Security { get; set; }
}