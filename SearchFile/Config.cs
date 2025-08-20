using System.Text.Json;

namespace SearchFile
{
    public class Config
    {
        public required string[] Skip { get; set; }

        public static async Task<Config?> LoadAsync(Logger logger)
        {
            var folder = Path.GetDirectoryName(Environment.ProcessPath);
            if (folder == null)
            {
                return null;
            }

            var file = Path.Combine(folder ?? ".", $"{nameof(Config)}.json");
            if (!File.Exists(file))
            {
                return null;
            }

            await logger.LogAsync($"Load config file {file}");

            var text = await File.ReadAllTextAsync(file);
            return JsonSerializer.Deserialize<Config>(text, options: new()
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}