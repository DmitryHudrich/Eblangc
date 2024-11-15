namespace Eblangc.Expressions;

internal class GroupingExpr(Expr expression) : Expr {
    public Expr Expression => expression;

    public override R Accept<R>(IVisitor<R> visitor) {
        return visitor.VisitGroupingExpr(this);
    }
}
