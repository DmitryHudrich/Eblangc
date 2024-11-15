namespace Eblangc.Expressions;

internal class LiteralExpr(Object value) : Expr {
    public Object Value => value;

    public override R Accept<R>(IVisitor<R> visitor) {
        return visitor.VisitLiteralExpr(this);
    }
}
