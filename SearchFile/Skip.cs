using System.Text.RegularExpressions;

namespace SearchFile
{
    public class Skip(Regex[] regexes)
    {
        public static async Task<Skip> LoadAsync(Logger logger)
        {
            var config = await Config.LoadAsync(logger);
            if (config == null)
            {
                return new Skip([]);
            }

            foreach (var text in config.Skip)
            {
                await logger.LogAsync($"Skip /{text}/");
            }

            var regexes = config.Skip.Select(text => new Regex(text));
            return new Skip([.. regexes]);
        }

        public bool Skipped(string path)
            => regexes.Any(regex => regex.IsMatch(path));
    }
}