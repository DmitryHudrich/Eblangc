namespace Eblangc.Expressions;

internal class BinaryExpr(Expr left, Token op, Expr right) : Expr {
    public Expr Left => left;
    public Token Op => op;
    public Expr Right = right;

    public override R Accept<R>(IVisitor<R> visitor) {
        return visitor.VisitBinaryExpr(this);
    }
}
