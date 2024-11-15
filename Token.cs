namespace Eblangc;

internal record struct Token(
    TokenType Type,
    String Lexeme,
    Object? Literal,
    Int32 Line);

