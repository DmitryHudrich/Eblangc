namespace Eblangc;

internal static class RawTokenExtensions {
    public static Boolean IsAlphaNumeric(this Char c) => IsAlpha(c) || IsDigit(c);

    public static Boolean IsAlpha(this Char c)
        => c is
            (>= 'a' and <= 'z') or
            (>= 'A' and <= 'Z') or '_';

    public static Boolean IsDigit(this Char c) => c is >= '0' and <= '9';
}

