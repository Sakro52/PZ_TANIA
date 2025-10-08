using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using System.Text.RegularExpressions;

namespace NewspaperLibUnitTests
{
    public class Prac11UnitTests
    {
        [Fact]
        public void Test_AreEqual_Vs_AreSame()
        {
            // Arrange
            string expected = "Hello";
            string actual = string.Concat("He", "llo");

            // Act / Assert
            Assert.Equal(expected, actual);
            Assert.NotSame(expected, actual);

            string interned = string.Intern(actual);
            Assert.Equal(expected, interned);
        }

        [Fact]
        public void Test_AllItemsAreUnique_And_AllItemsAreNotNull()
        {
            // Arrange
            var items = MyClass.GenerateItems();

            // Assert
            Assert.All(items, item => Assert.NotNull(item));

            var names = items.Select(i => i.Name).ToList();
            var distinctCount = names.Distinct().Count();
            Assert.Equal(names.Count, distinctCount);
        }

        [Fact]
        public void Test_StringAssert_Equivalent_Functions()
        {
            var m = new MyClass();

            string full1 = m.SayHello("Harry Potter");
            Assert.Contains("Potter", full1);

            string full2 = m.SayHello("Donald Duck");
            Assert.StartsWith("Hello Don", full2);

            string full3 = m.SayHello("Uncle Scrooge");
            Assert.EndsWith("Scrooge", full3);

            string full4 = m.SayHello("User123");
            Assert.Matches(new Regex(@"^Hello \w+\d*$"), full4);
        }

        [Fact]
        public void Test_CollectionAssert_Equivalent_Functions()
        {
            var items = MyClass.GenerateItems();

            var expectedNames = new List<string> { "Apple", "Banana", "Orange" };
            Assert.Equal(expectedNames, items.Select(i => i.Name).ToList());

            var shuffled = new List<string> { "Orange", "Apple", "Banana" };
            Assert.Equal(expectedNames.OrderBy(x => x), shuffled.OrderBy(x => x));

            Assert.Contains("Banana", items.Select(i => i.Name));

            var subset = new List<string> { "Apple", "Banana" };
            Assert.True(subset.All(s => items.Select(i => i.Name).Contains(s)));
        }
    }
}