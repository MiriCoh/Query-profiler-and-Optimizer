using Kusto.Cloud.Platform.Utils;
using Kusto.Language;
using Kusto.Language.Symbols;
using Kusto.Language.Syntax;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

namespace QueryProfiler.Optimization
{
    public class OptimalProposalForQuery
    {
        public static List<ProposalScheme> GetListOfPropsalToQuery(string query)
        {
            if (!query.IsNotNullOrEmpty()) return null;
            var code = KustoCode.Parse(query);
            var diagnostics = IsValidQuery(code);
            if (diagnostics.Count > 0)
                return new List<ProposalScheme>();
            var currentProposalsOptimization = new List<ProposalScheme>();
            var currentKeywords = new Dictionary<SyntaxNode, int>();
            SyntaxElement.WalkNodes(code.Syntax,
           Operator =>
           {
               switch (Operator.Kind.ToString())
               {
                   case "ContainsExpression":
                   case "ContainsCsExpression":
                   case "HasExpression":
                       currentKeywords.Add(Operator, Operator.TextStart);
                       break;
                   default:
                       break;
               }
               switch (Operator)
               {
                   case HasAnyExpression operator1:
                   case InExpression operator2:
                   case LookupOperator operator3:
                   case JoinOperator operator4:
                   case SearchOperator operator5:
                       currentKeywords.Add(Operator, Operator.TextStart);
                       break;
                   default:
                       break;
               }
           });
            currentProposalsOptimization = AddProposalsAndUpdatePosition(currentKeywords);
   
            return currentProposalsOptimization;
        }
        public static List<string> IsValidQuery(KustoCode query)
        {
            var lst = new List<string>();
            query.GetDiagnostics().ForEach(item => lst.Add(item.Message));
            return lst;
        }
        private static List<ProposalScheme> AddProposalsAndUpdatePosition(Dictionary<SyntaxNode, int> operatorsList)
        {
            var proposals = XmlOptimalProposals.GetProposalsOptimization();
            var currentProposals = new List<ProposalScheme>();
            var allMatchProposals = new List<ProposalScheme>();
            foreach (var keyword in operatorsList)
            {
                var operatorKind = GetOperatorKind(keyword.Key.Kind.ToString(), keyword.Key.NameInParent);
                var isFind= allMatchProposals.Find(op => op.SourceOperator == operatorKind);
                if (isFind != null)
                    allMatchProposals.ForEach(op => op.OperatorsPosition.Add(keyword.Value));
                else
                {
                    currentProposals = proposals.ProposalsOptimization.FindAll(op => op.SourceOperator == operatorKind);
                    currentProposals.ForEach(op => op.OperatorsPosition.Add(keyword.Value));
                    allMatchProposals.AddRange(currentProposals);
                }

            }
            return allMatchProposals;
        }

        private static List<ProposalScheme> DeepCopyProposals(List<ProposalScheme> proposals)
        {
            var deepCopyProposals = new List<ProposalScheme>();
            foreach (var prop in proposals)
            {
                using (var ms = new MemoryStream())
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(ms, prop);
                    ms.Position = 0;

                    deepCopyProposals.Add((ProposalScheme)formatter.Deserialize(ms));
                }
            }
            return deepCopyProposals;
        }
        private static string GetOperatorKind(string strToSub, string kind)
        {
            return strToSub.Substring(0, strToSub.Length - kind.Length);
        }
    }
}