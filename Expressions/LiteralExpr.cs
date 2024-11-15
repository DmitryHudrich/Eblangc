namespace Eblangc.Expressions;

internal class LiteralExpr(Object value) : Expr {
    public Object Value => value;
}
