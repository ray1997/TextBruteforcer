public class ProgramBAK
{

    public static void Main()
    {

        char[] charsets = "abcdefghijklmnopqrstuvwxyz".ToCharArray();

        int currentLength = 3;
        var maxLength = 26;
        string path = "C:\\sus\\latest.txt";
        char[] input = "```````````````````````sur".ToCharArray();

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

        while (input != "zzzzzzzzzzzzzzzzzzzz".ToCharArray())
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
                Console.Write($"Latest suspicious word: {text}");
                using StreamWriter sw = File.AppendText(@"C:\sus\sus.txt");
                sw.Write($"{text}, ");
                sw.Close();
            }
        }
    }
}