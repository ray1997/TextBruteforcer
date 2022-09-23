string wanted = "sus";

char[] charsets = "abcdefghijklmnopqrstuvwxyz".ToCharArray();

int currentLength = 3;
var maxLength = 26;
string path = $"C:\\{wanted}\\latest.txt";
char[] input = "``````````````````````````".ToCharArray();

if (!Directory.Exists(path))
    Directory.CreateDirectory(path);

if (File.Exists(path))
{
    using var reader = File.OpenText(path);
    input = reader.ReadToEnd().ToCharArray();
    reader.Close();
    File.Delete(path);
}

Console.CancelKeyPress += TrySave;

void TrySave(object? sender, ConsoleCancelEventArgs e)
{
    if (File.Exists(path))
        File.Delete(path);
    using StreamWriter writer = File.CreateText(path);
    writer.Write(input);
    writer.Close();
    e.Cancel = false;
    Environment.Exit(69);
}

do
{
    Console.CursorLeft = 0;
    Console.CursorTop = 0;
    string text = CharSet.Advance(ref input, ref currentLength);
    text = text.Replace("`", string.Empty);
    Console.Write(text);

    if (text.Contains("sus"))
    {
        Console.CursorLeft = 0;
        Console.CursorTop = 1;
        Console.Write($"Latest {wanted} word: {text}");
        using StreamWriter sw = File.AppendText(@$"C:\{wanted}\{wanted}.txt");
        sw.Write($"{text}, ");
        sw.Close();
    }
}
while (currentLength < maxLength);