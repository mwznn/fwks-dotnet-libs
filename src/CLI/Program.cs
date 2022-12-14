using System;
using System.IO;
using System.Net.Http;

try
{
    using var httpClient = new HttpClient();

    using var stream = await httpClient.GetStreamAsync(AppSettings.TemplateZipUri);

    Directory.CreateDirectory(AppSettings.AppFolder);

    using var fileStream = new FileStream(AppSettings.LatestTemplateFile, FileMode.OpenOrCreate);

    await stream.CopyToAsync(fileStream);

    var tempPath = AppSettings.AppFolder;
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

public static class AppSettings
{
    public static string AppFolder => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Fwks CLI");
    public static string TemplateZipUri => "https://github.com/mwznn/fwks/archive/refs/heads/main.zip";
    public static string LatestTemplateFile => Path.Combine(AppFolder, "latest-template.zip");
}