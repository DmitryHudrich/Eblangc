namespace Eblangc.Expressions;

internal class UnaryExpr(Token op, Expr right) : Expr {
    public Token Op => op;
    public Expr Right => right;
}
