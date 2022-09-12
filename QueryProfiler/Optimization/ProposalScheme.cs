using System;
using System.Collections.Generic;
using System.Xml.Serialization;
namespace QueryProfiler
{
    [XmlRoot(elementName: "ProposalsOptimizations")]
    public class ProposalsOptimizations
    {
        [XmlElement(elementName: "ProposalScheme")]
        public List<ProposalScheme> ProposalsOptimization = new List<ProposalScheme>();

    }
    [Serializable]
    [XmlRoot(elementName: "ProposalScheme")]
    public class ProposalScheme
    {

        [XmlElement(elementName: "SourceOperator")]
        public string SourceOperator { get; set; }
        [XmlElement(elementName: "ProposalOptimalOperator")]
        public string ProposalOptimalOperator { get; set; }
        [XmlElement(elementName: "ProposalReason")]
        public string ProposalReason { get; set; }
        public List<int> OperatorsPosition { get; set; }

    }
    public class ProposalSchemaEqualityComparer : IEqualityComparer<ProposalScheme>
    {
        public bool Equals(ProposalScheme ps1, ProposalScheme ps2)
        {
            if (ReferenceEquals(ps1, ps2))
                return true;
            if (ReferenceEquals(ps1, null) && ReferenceEquals(ps2, null))
                return false;
            return ps1.OperatorsPosition.ToArray() == ps2.OperatorsPosition.ToArray()
                && ps1.SourceOperator == ps2.SourceOperator
                && ps1.ProposalOptimalOperator == ps2.ProposalOptimalOperator
                && ps1.ProposalReason == ps2.ProposalReason;
        }
        public int GetHashCode(ProposalScheme ps)
        {
            if (ReferenceEquals(ps, null))
                return 0;
            int hashCodeSomeProp1 = ps.SourceOperator == null ? 0 : ps.SourceOperator.GetHashCode();
            int hashCodeSomeProp2 = ps.ProposalReason.GetHashCode();
            return hashCodeSomeProp1 ^ hashCodeSomeProp2;
        }
    }
}
