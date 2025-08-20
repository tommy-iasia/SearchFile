namespace SearchFile
{
    public class Logger
    {
        private const string folder = "Log";
        private readonly string name = $"{DateTime.Now:yyyyMMddHHmmssfff}.log";
        public async Task LogAsync(string line)
        {
            var folderPath = Path.Combine(Path.GetDirectoryName(Environment.ProcessPath) ?? ".", folder);
            Directory.CreateDirectory(folderPath);

            var filePath = Path.Combine(folderPath, name);
            await File.AppendAllLinesAsync(filePath, [line]);
        }
    }
}