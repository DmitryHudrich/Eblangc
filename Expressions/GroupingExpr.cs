namespace Eblangc.Expressions;

internal class GroupingExpr(Expr expression) : Expr {
    public Expr Expression => expression;
}
