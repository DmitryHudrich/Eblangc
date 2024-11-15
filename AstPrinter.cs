using System.Text;
using Eblangc.Expressions;

namespace Eblangc;

internal class AstPrinter : IVisitor<String?> {
    public String VisitBinaryExpr(BinaryExpr expr) {
        return Parenthesize(expr.Op.Lexeme, expr.Left, expr.Right);
    }

    public String VisitGroupingExpr(GroupingExpr expr) {
        return Parenthesize("group", expr.Expression);
    }

    public String? VisitLiteralExpr(LiteralExpr expr) {
        return expr.Value == null ? "nil" : expr.Value.ToString();
    }

    public String VisitUnaryExpr(UnaryExpr expr) {
        return Parenthesize(expr.Op.Lexeme, expr.Right);
    }

    private String Parenthesize(String name, params Expr[] expressions) {
        var builder = new StringBuilder();
        builder.Append('(').Append(name);

        foreach (var expr in expressions) {
            builder.Append(' ');
            builder.Append(expr.Accept(this));
        }

        builder.Append(')');
        return builder.ToString();
    }
}
