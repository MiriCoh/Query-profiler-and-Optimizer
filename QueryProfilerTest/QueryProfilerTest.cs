using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueryProfiler;
using System.Collections.Generic;
namespace QueryProfilerTest
{
    [TestClass]
    public class QueryProfilerTest
    {
        [TestMethod]
        public void Test_QueryWithEmptyQueryt()
        {
            var query = "";
            var actual = QueryProfiler.Profile.ProfileAnalyzer.GetProfile(query);
            var expected = new ProfileScheme
            {
                complexityLevel=0,
                JoinCounter = 0,
                UnionCounter = 0,
                LookupCounter = 0,
                MvExpandCounter = 0,
                InCounter = 0,
                Tables = new List<string>
                {
                }
            };
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Test_QueryWithNoExpression()
        {
            var query = "Table1";
            var actual = QueryProfiler.Profile.ProfileAnalyzer.GetProfile(query);
            var expected = new ProfileScheme
            {
                complexityLevel = 0,
                JoinCounter = 0,
                UnionCounter = 0,
                LookupCounter = 0,
                MvExpandCounter = 0,
                InCounter = 0,
                Tables = new List<string>
                {
                    "Table1"
                }
            };
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Test_QueryWithASameOperators()
        {
            var query = "Table1 | join (Table2) on CommonColumn, $left.Col1 == $right.Col2 ";
            var actual = QueryProfiler.Profile.ProfileAnalyzer.GetProfile(query);
            var expected = new ProfileScheme
            {
                complexityLevel = 1,
                JoinCounter = 1,
                UnionCounter = 0,
                LookupCounter = 0,
                MvExpandCounter = 0,
                InCounter = 0,
                Tables = new List<string>
                {
                    "Table1",
                    "Table2"
                }
            };
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Test_QueryWithTowJoins()
        {
            var query = "Table1 | join Table2 on Region| join Table3 on Region";
            var actual = QueryProfiler.Profile.ProfileAnalyzer.GetProfile(query);
            var expected = new ProfileScheme
            {
                complexityLevel = 1,
                JoinCounter = 2,
                UnionCounter = 0,
                LookupCounter = 0,
                MvExpandCounter = 0,
                InCounter = 0,
                Tables = new List<string>
                {
                    "R1",
                    "R2",
                    "R3"
                }
            };
            Assert.AreEqual(expected, actual);
        }
    }

}
