namespace SearchFile
{
    public class Display
    {
        private string showing = string.Empty;

        public void Clear() => Console.Write($"\r{new string(' ', showing.Length)}\r");

        public void Show(string line)
        {
            Clear();

            try
            {
                var width = Console.WindowWidth - 1;
                if (line.Length > width)
                {
                    var head = width / 3;
                    var tail = width - head - 3;

                    showing = line[..head] + "..." + line[^tail..];
                }
                else
                {
                    showing = line;
                }
            }
            catch
            {
                showing = line;
            }

            Console.Write(showing);
        }

        public void File(string file)
        {
            Clear();

            var color = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Yellow;

            var directory = Path.GetDirectoryName(file);
            if (!string.IsNullOrEmpty(directory))
            {
                Console.Write($"\u001B]8;;{directory}\a{directory}\u001B]8;;\a");
                Console.Write(Path.DirectorySeparatorChar);

                var name = Path.GetFileName(file);
                Console.WriteLine($"\u001B]8;;{file}\a{name}\u001B]8;;\a");
            }
            else
            {
                Console.WriteLine($"\u001B]8;;{file}\a{file}\u001B]8;;\a");
            }

            Console.ForegroundColor = color;

            Console.Write(showing);
        }

        public void Folder(string folder)
        {
            Clear();

            var color = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine($"\u001B]8;;{folder}\a{folder}\u001B]8;;\a");

            Console.ForegroundColor = color;

            Console.Write(showing);
        }
    }
}