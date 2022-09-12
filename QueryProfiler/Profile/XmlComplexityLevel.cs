using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace QueryProfiler.Profile
{
    public class XmlComplexityLevel
    {
        public static SchenaComplexitiesLevel GetComplexityLevel()
        {
            XDocument doc = XDocument.Parse(Properties.Resources.XMLComplexityLevel);
            using (var reader = doc.CreateReader())
            {
                var xs = new XmlSerializer(typeof(SchenaComplexitiesLevel));
                var xd = xs.Deserialize(reader) as SchenaComplexitiesLevel;
                return xd != null ? xd : new SchenaComplexitiesLevel();
            }
        }
    }
    [XmlRoot(elementName: "complexitiesLevel")]
    public class SchenaComplexitiesLevel
    {
        [XmlElement(elementName: "complexityLevel")]
        public List<ComplexityLevel> complexitiesLevel { get; set; }
    
    }

    [XmlRoot(elementName: "complexityLevel")]
    public class ComplexityLevel
    {
        [XmlElement(elementName: "JoinUnionLookupCounter")]
        public JoinUnionLookupCounter JoinUnionLookupCounter { get; set; }
        [XmlElement(elementName: "Level")]
        public int Level { get; set; }
    }

    [XmlRoot(elementName: "JoinUnionLookupCounter")]
    public class JoinUnionLookupCounter
    {
        [XmlElement(elementName: "range")]
        public range range { get; set; }
    }
    [XmlRoot(elementName: "range")]
    public class range
    {
        [XmlElement(elementName: "from")]
        public int from { get; set; }
        [XmlElement(elementName: "to")]
        public int to { get; set; }
    }
}
