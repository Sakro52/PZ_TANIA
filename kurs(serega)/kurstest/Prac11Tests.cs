using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Prac11;
using Xunit;

namespace TrafficAccidentLibUnitTests
{
    public class Prac11UnitTests
    {
        [Fact]
        public void Test_AreEqual_Vs_AreSame()
        {
            // Arrange
            string expected = "Hello";
            // ��������� ������ ������ � ��� �� ����������, �� ������ �������
            string actual = string.Concat("He", "llo"); // ����������� ��� ����� ������ (�� �������������� ���������������)

            // Act / Assert
            Assert.Equal(expected, actual);     // ��������������� �� ��������
            Assert.NotSame(expected, actual);   // ������ ������ => NotSame (������ AreSame/AreNotSame � MSTest)
            
            // ������� ������, ����� ������ ��������� (intern)
            string interned = string.Intern(actual);
            // interned ����� ��������� �� "Hello" � ���� ���������
            // ���� expected ��� ��������� "Hello", �� ����� �������������� ������ ����� ��������
            Assert.Equal(expected, interned);
            // ��������� ������ ����� ���� ��� �����, ��� � ��� � ����������� �� ��������������;
            // ����� ��������, ��� interned �������� ���� same ���� not, �� ������� � ������������.
        }

        [Fact]
        public void Test_AllItemsAreUnique_And_AllItemsAreNotNull()
        {
            // Arrange
            var items = MyClass.GenerateItems();

            // All items are not null
            Assert.All(items, item => Assert.NotNull(item));

            // All item names unique
            var names = items.Select(i => i.Name).ToList();
            var distinctCount = names.Distinct().Count();
            Assert.Equal(names.Count, distinctCount); // ������ CollectionAssert.AllItemsAreUnique
        }

        [Fact]
        public void Test_StringAssert_Equivalent_Functions()
        {
            var m = new MyClass();

            // 1) Contains
            string full1 = m.SayHello("Harry Potter");
            Assert.Contains("Potter", full1); // ������ StringAssert.Contains

            // 2) StartsWith (����������������)
            string full2 = m.SayHello("Donald Duck");
            Assert.StartsWith("Hello Don", full2); // ��������� ������ ������

            // 3) EndsWith
            string full3 = m.SayHello("Uncle Scrooge");
            Assert.EndsWith("Scrooge", full3); // ������ StringAssert.EndsWith

            // 4) Matches (���������� ���������)
            string full4 = m.SayHello("User123");
            Assert.Matches(new Regex(@"^Hello \w+\d*$"), full4); // ������ StringAssert.Matches
        }

        [Fact]
        public void Test_CollectionAssert_Equivalent_Functions()
        {
            var items = MyClass.GenerateItems();

            // 1) AreEqual (������������������ � ��� �� ������� � ����������)
            var expectedNames = new List<string> { "Apple", "Banana", "Orange" };
            Assert.Equal(expectedNames, items.Select(i => i.Name).ToList());

            // 2) AreEquivalent (�������� ���� � �� �� ��������, ������� �� �����)
            var shuffled = new List<string> { "Orange", "Apple", "Banana" };
            Assert.Equal(expectedNames.OrderBy(x => x), shuffled.OrderBy(x => x));

            // 3) Contains (��������� �������� �������)
            Assert.Contains("Banana", items.Select(i => i.Name));

            // 4) IsSubsetOf (��������, ��� ������������ ������������� �������� �������������)
            var subset = new List<string> { "Apple", "Banana" };
            Assert.True(subset.All(s => items.Select(i => i.Name).Contains(s)));
        }
    }
}