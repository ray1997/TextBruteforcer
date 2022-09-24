
var now = Environment.CurrentDirectory;

string wanted = "sus";
string wantedPath = Path.Join(now, "wanted.txt");
if (File.Exists(wantedPath))
    wanted = File.ReadAllText(wantedPath);
else
    File.WriteAllText(wantedPath, wanted);

char[] charsets = "abcdefghijklmnopqrstuvwxyz".ToCharArray();

int currentLength = 1;


var maxLength = 26;

StringBuilder bd = new();
bd.Append('`', maxLength);
var input = bd.ToString().ToArray();

string progress = $"{now}\\latest.txt";
if (File.Exists(progress))
{
    using var reader = File.OpenText(progress);
    input = reader.ReadToEnd().ToCharArray();
    reader.Close();
}

Console.CancelKeyPress += TrySave;

void TrySave(object? sender, ConsoleCancelEventArgs e)
{
    if (File.Exists(progress))
        File.Delete(progress);
    using StreamWriter writer = File.CreateText(progress);
    writer.Write(input);
    writer.Close();
    e.Cancel = false;
    Environment.Exit(69);
}

while (true)
{
    Console.CursorLeft = 0;
    Console.CursorTop = 0;
    string text = CharSet.Advance(ref input, ref currentLength);
    text = text.Replace("`", string.Empty);
    Console.Write(text);

    if (text.Contains(wanted))
    {
        Console.CursorLeft = 0;
        Console.CursorTop = 1;
        Console.Write($"Latest {wanted} word: {text}");
        using StreamWriter sw = File.AppendText(@$"{now}\{wanted}.txt");
        sw.Write($"{text}, ");
        sw.Close();
    }
}