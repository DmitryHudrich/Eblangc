namespace Eblangc.Expressions;

internal interface IVisitor<T> {
    T VisitBinaryExpr(BinaryExpr expr);
    T VisitGroupingExpr(GroupingExpr expr);
    T VisitLiteralExpr(LiteralExpr expr);
    T VisitUnaryExpr(UnaryExpr expr);

}

internal abstract class Expr {
    public abstract T Accept<T>(IVisitor<T> visitor);
}
