//App target directory should now be where it ran, instead of C:\
var now = Environment.CurrentDirectory;
var charsets = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
var currentLength = 1;

//Default lookup word "sus"
var wanted = "sus";
//Looking for "wanted.txt" in the running directory, if there's a wanted.txt will change from "sus" to that wanted word
var wantedPath = Path.Join(now, "wanted.txt");
if (File.Exists(wantedPath))
    wanted = File.ReadAllText(wantedPath);

//Length of text to bruteforce
//By default, max at 26
var maxLength = 26;
//If the file "length.txt" exist on the running directory, will try to read from that folder
var lengthPath = Path.Join(now, "length.txt");
if (File.Exists(lengthPath))
{
    //Try parsing text from "length.txt"
    var length = -1;
    if (int.TryParse(File.ReadAllText(lengthPath), out length))
    {
        maxLength = length;
    } //If it's failed to parse, the target length will stuck at 26
}

//String builder, as I'm too lazy to write for() to generate text consist of same character
StringBuilder bd = new();
//Input or CurrentPosition text
var input = bd
    .Append('`', maxLength) //Write ` for 26 times as a base text
    .ToString()
    .ToArray();

//Use as a specify on where "input" (currentPosition) should start at instead of ````````~
var progressPath = Path.Join(now, "latest.txt");
//Read a progres file if its exist
if (File.Exists(progressPath))
{
    input = File.ReadAllText(progressPath).ToArray();
}

bd.Clear();
//Target text on where the input (currentProgress) will stop at
//By default it's zzzz~ (26 characters long)
var target = bd
    .Append('z', maxLength)
    .ToString()
    .ToArray();
//Chech "target.txt" on running directory
var targetPath = Path.Join(now, "target.txt");
if (File.Exists(targetPath))
{
    target = File.ReadAllText(targetPath).ToArray();
}

//Use on checking when user force terminate console via CTRL+C
Console.CancelKeyPress += TrySave;
void TrySave(object? sender, ConsoleCancelEventArgs e)
{
    Console.CursorLeft = 0;
    Console.CursorTop = 4;
    //Tell console, we're not trying to cancel the CTRL+C
    e.Cancel = true;
    //We're just need to do something before quitting
    //Delete existing progress for upcoming new progress
    if (File.Exists(progressPath))
        File.Delete(progressPath);
    File.WriteAllText(progressPath, new string(input));
    //Actually exist the console, return the funny number
    Environment.Exit(69);
}

//Heart of operation
//Repeat this until input is same as target
while (!input.SequenceEqual(target))
{
    //Set cursor to top-left
    Console.CursorLeft = 0;
    Console.CursorTop = 0;

    var text = CharSet.Advance(ref input, ref currentLength);
    text = text.Replace("`", string.Empty);
    Console.Write(text);

    if (text.Contains(wanted))
    {
        Console.CursorLeft = 0;
        Console.CursorTop = 1;
        Console.Write($"Latest {wanted} word: {text}");
        using var sw = File.AppendText(Path.Join(now, $"{wanted}.txt"));
        sw.Write($"{text}, ");
        sw.Close();
    }
}

if (input.SequenceEqual(target))
{
    Console.CursorLeft = 0;
    Console.CursorTop = 2;
    Console.Write("Bruteforcer has reach its target");
    //Delete progress if exist
    if (File.Exists(progressPath))
        File.Delete(progressPath);
    Environment.Exit(-69);
}