using System.Collections.Generic;
using Antlr4.Runtime;

namespace Microsoft.Pc.TypeChecker.AST.Statements
{
    public class PrintStmt : IPStmt
    {
        public PrintStmt(ParserRuleContext sourceLocation, string message, List<IPExpr> args)
        {
            SourceLocation = sourceLocation;
            Message = message;
            Args = args;
        }

        public string Message { get; }
        public List<IPExpr> Args { get; }

        public ParserRuleContext SourceLocation { get; }
    }
}