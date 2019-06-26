using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class DependencyTests
    {

        [Test]
        public void Should_Return_Empty_Job_Sequence_When_Empty_String_Is_Passed()
        {
            var result = DependencyChallenge.Program.GetDependencyList(new HashSet<string>(new[] { "" }),
                 new HashSet<Tuple<string, string>>());

            Assert.AreEqual(result, new List<string> { "" });
            Assert.AreEqual(result.Count, 1);
        }

        [Test]
        public void Should_Return_Single_Job_Sequence_When_Single_Job_Is_Passed()
        {
            var result = DependencyChallenge.Program.GetDependencyList(new HashSet<string>(new[] { "A" }),
                new HashSet<Tuple<string, string>>());

            Assert.AreEqual(result, new List<string> { "A" });
            Assert.AreEqual(result.Count, 1);
        }

        [Test]
        public void Should_Return_Job_Sequence_When_Jobs_With_No_Dependency_Are_Passed()
        {
            var result = DependencyChallenge.Program.GetDependencyList(new HashSet<string>(new[] { "A" , "B" , "C" }),
                new HashSet<Tuple<string, string>>());

            Assert.AreEqual(result, new List<string> { "A" , "B", "C" });
            Assert.AreEqual(result.Count, 3);
        }

        [Test]
        public void Should_Return_Job_Sequence_When_Jobs_With_Dependency_Are_Passed()
        {
            var result = DependencyChallenge.Program.GetDependencyList(new HashSet<string>(new[] { "A", "B", "C", "D", "E", "F" }),
             new HashSet<Tuple<string, string>>(
                 new[] {
                        Tuple.Create("B" , "C"),
                        Tuple.Create("C", "F"),
                        Tuple.Create("D", "A"),
                        Tuple.Create("E" , "B"),

                 }));

            Assert.AreEqual(result, new List<string> { "A", "D", "F", "C", "B", "E" });
            Assert.AreEqual(result.Count, 6);
        }

        [Test]
        public void Should_Return_Null_When_Jobs_With_Circular_Dependency_Are_Passe()
        {
            var result = DependencyChallenge.Program.GetDependencyList(new HashSet<string>(new[] { "A" }),
               new HashSet<Tuple<string, string>>(new[] {
                        Tuple.Create("A" , "A") }));

            Assert.AreEqual(result, null);
        }        
    }
}