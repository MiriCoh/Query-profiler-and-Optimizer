using Kusto.Language.Syntax;
namespace QueryProfiler.Profile
{
   public class OperatorSchema
    {
        public SyntaxKind Operator { get; set; }
        public string Kind { get; set; }
    }
}
