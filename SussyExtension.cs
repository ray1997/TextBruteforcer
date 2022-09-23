public static class SussyExtension
{
    public static bool IsSus(this StringBuilder word)
    {
        return word.ToString().Contains("sus");
    }
}