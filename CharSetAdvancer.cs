public static class CharSet
{
    public static string Advance(ref char[] input, ref int currentLength)
    {
        var back = input.Length -1;

        input[input.Length - 1]++; //Increase last character by one
        do
        {
            if (input[back] != 'z')
            {
                if (back <= input.Length - currentLength)
                    currentLength++;
            }

            if (input[back] > 'z')
            {
                input[back] = 'a';
                if (back > 0)
                    input[back - 1]++;
            }

            back--;
        }        
        while (back > 0);
        var trim = new ReadOnlySpan<char>(input, input.Length - currentLength, currentLength);
        return new string(trim);
    }
}
