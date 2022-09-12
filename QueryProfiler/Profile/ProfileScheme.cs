using System.Collections.Generic;
namespace QueryProfiler
{
    public class ProfileScheme
    {
        public int complexityLevel { get; set; }
        public int JoinCounter { get; set; }
        public int UnionCounter { get; set; }
        public int LookupCounter { get; set; }
        public int MvExpandCounter { get; set; }
        public int InCounter { get; set; }
        public List<string> Tables { get; set; } = new List<string>();
        public override bool Equals(object obj)
        {
            var ps = obj as ProfileScheme;
            if (ps == null)
                return false;
            return ps.complexityLevel== complexityLevel
                && ps.JoinCounter == JoinCounter
                && ps.UnionCounter == UnionCounter
                && ps.LookupCounter == LookupCounter
                && ps.MvExpandCounter == MvExpandCounter
                && ps.InCounter == InCounter
                && ps.Tables.ToString() == Tables.ToString();
        }
    }
}
