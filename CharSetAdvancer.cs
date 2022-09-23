public static class CharSet
{
    public static string Advance(ref char[] input, ref int currentLength)
    {
        int back = input.Length -1;
        bool lengthUpReq = true;

        input[input.Length - 1]++;
        do
        {
            if (input[back] != 'z')
            {
                if (back > input.Length - currentLength)
                    lengthUpReq = false;
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
