namespace Eblangc;

internal class Scanner(String source) {
    private readonly List<Token> tokens = [];

    private Int32 start = 0;
    private Int32 current = 0;
    private Int32 line = 1;

    private static IReadOnlyDictionary<String, TokenType> keywords => KeywordsContainer.Keywords;

    public List<Token> ScanTokens() {
        while (!IsAtEnd()) {
            start = current;
            ScanToken();
        }

        tokens.Add(new Token(TokenType.EOF, "", null, line));
        return tokens;
    }

    private void ScanToken() {
        var c = Advance();
        switch (c) {
            // simple single chars
            case '(': AddToken(TokenType.LEFT_PAREN); break;
            case ')': AddToken(TokenType.RIGHT_PAREN); break;
            case '{': AddToken(TokenType.LEFT_BRACE); break;
            case '}': AddToken(TokenType.RIGHT_BRACE); break;
            case ',': AddToken(TokenType.COMMA); break;
            case '.': AddToken(TokenType.DOT); break;
            case '-': AddToken(TokenType.MINUS); break;
            case '+': AddToken(TokenType.PLUS); break;
            case ';': AddToken(TokenType.SEMICOLON); break;
            case '*': AddToken(TokenType.STAR); break;

            // symbols that need to be disambiguated
            case '!':
                AddToken(MatchSingleChar('=') ? TokenType.BANG_EQUAL : TokenType.BANG);
                break;
            case '=':
                AddToken(MatchSingleChar('=') ? TokenType.EQUAL_EQUAL : TokenType.EQUAL);
                break;
            case '<':
                AddToken(MatchSingleChar('=') ? TokenType.LESS_EQUAL : TokenType.LESS);
                break;
            case '>':
                AddToken(MatchSingleChar('=') ? TokenType.GREATER_EQUAL : TokenType.GREATER);
                break;
            case '/':
                if (MatchSingleChar('/')) {
                    while (Peek() != '\n' && !IsAtEnd()) {
                        Advance();
                    }
                }
                else {
                    AddToken(TokenType.SLASH);
                }
                break;

            // string literal
            case '"':
                MatchString();
                break;

            // musor
            case ' ':
            case '\r':
            case '\t':
                // Ignore other trash.
                break;

            case '\n':
                line++;
                break;

            default:
                if (c.IsDigit()) {
                    MatchNumber();
                }
                else if (c.IsAlpha()) {
                    MatchIdentifier();
                }
                else {
                    ErrorHandler.Error(line, "Unexpected character.");
                }
                break;
        }
    }

    private void MatchIdentifier() {
        while (Peek().IsAlphaNumeric()) { Advance(); }
        var text = source[start..current];
        if (keywords.TryGetValue(text, out var type)) {
            AddToken(type);
        }
        else {
            AddToken(TokenType.IDENTIFIER);
        }
    }

    private void MatchNumber() {
        while (Peek().IsDigit()) { Advance(); }

        if (Peek() == '.' && PeekNext().IsDigit()) {
            Advance();
            while (Peek().IsDigit()) { Advance(); }
        }

        AddToken(TokenType.NUMBER, Double.Parse(source[start..current]));
    }

    private Char PeekNext() => current + 1 >= source.Length ? '\0' : source.ElementAt(current + 1);

    private void MatchString() {
        while (Peek() != '"' && !IsAtEnd()) {
            // Multirow strings. Ya so lazy.
            if (Peek() == '\n') {
                line++;
            }
            Advance();
        }

        if (IsAtEnd()) {
            ErrorHandler.Error(line, "Unterminated string literal.");
            return;
        }
        Advance();
        // Remove '"'.
        // In future, there will be escape sequences. Maybe.
        var value = source[(start + 1)..(current - 1)];
        AddToken(TokenType.STRING, value);
    }

    private Boolean MatchSingleChar(Char expected) {
        if (IsAtEnd() || source.ElementAt(current) != expected) {
            return false;
        }

        current++;
        return true;
    }

    private Char Peek() => IsAtEnd() ? '\0' : source.ElementAt(current);

    private Char Advance() => source.ElementAt(current++);

    private void AddToken(TokenType type) => AddToken(type, null);

    private void AddToken(TokenType type, Object? literal) => tokens.Add(new Token(type, source[start..current], literal, line));

    private Boolean IsAtEnd() => current >= source.Length;
}
