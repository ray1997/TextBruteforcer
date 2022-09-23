using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SusAllPossibility;
/// <summary>
/// Credit to: https://gist.github.com/mchandschuh/0f9563241f935dfd5d6e6970804fb7e0
/// </summary>
public static class BaseConversion
{
    // 256 cuz it's BigInteger
    const int bufferSize = 256;
    public static string IntToStringFast(BigInteger value, char[] baseChars)
    {
        var i = bufferSize;

        var buffer = new char[i];
        var targetBase = baseChars.Length;

        do
        {
            buffer[--i] = baseChars[(int)(value % targetBase)];
            value /= targetBase;
        }
        while (value > 0);
        // avoid re-allocating another array just to trim the size
        // by using string's ReadOnlySpan<char> ctor
        var trimmed = new ReadOnlySpan<char>(buffer, i, bufferSize - i);
        return new string(trimmed);
    }
}