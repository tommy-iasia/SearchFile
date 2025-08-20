using System.Text.RegularExpressions;

namespace SearchFile
{
    public interface ICapture
    {
        bool Capture(string path);

        public static async Task<ICapture> LoadAsync(Logger logger)
        {
            var args = Environment.GetCommandLineArgs();
            var text = args.Length > 1 ? args[1] : "abc";

            if (text.StartsWith('/') && text.Length > 2 && text.EndsWith('/'))
            {
                var message = $"Search for file {text}...";
                Console.WriteLine();
                await logger.LogAsync(message);

                var regex = new Regex(text[1..^1]);
                return new RegexCapture(regex);
            }
            else
            {
                var message = $"Search for file \"{text}\"...";
                Console.WriteLine(message);
                await logger.LogAsync(message);

                return new TextCapture(text);
            }
        }
    }

    public class TextCapture(string text) : ICapture
    {
        public bool Capture(string path) => path.Contains(text, StringComparison.OrdinalIgnoreCase);
    }

    public class RegexCapture(Regex regex) : ICapture
    {
        public bool Capture(string path) => regex.IsMatch(path);
    }
}