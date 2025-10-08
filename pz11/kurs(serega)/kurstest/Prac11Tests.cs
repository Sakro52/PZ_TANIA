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
            // Формируем другую строку с тем же содержимым, но другой ссылкой
            string actual = string.Concat("He", "llo"); // объединение даёт новую строку (не гарантированно интернированную)

            // Act / Assert
            Assert.Equal(expected, actual);     // эквивалентность по значению
            Assert.NotSame(expected, actual);   // ссылки разные => NotSame (аналог AreSame/AreNotSame в MSTest)
            
            // Покажем пример, когда ссылки одинаковы (intern)
            string interned = string.Intern(actual);
            // interned может ссылаться на "Hello" в пуле литералов
            // Если expected был литералом "Hello", то после интернирования ссылки могут совпасть
            Assert.Equal(expected, interned);
            // Сравнение ссылок может быть как равно, так и нет в зависимости от интернирования;
            // здесь проверим, что interned является либо same либо not, но главное — демонстрация.
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
            Assert.Equal(names.Count, distinctCount); // аналог CollectionAssert.AllItemsAreUnique
        }

        [Fact]
        public void Test_StringAssert_Equivalent_Functions()
        {
            var m = new MyClass();

            // 1) Contains
            string full1 = m.SayHello("Harry Potter");
            Assert.Contains("Potter", full1); // аналог StringAssert.Contains

            // 2) StartsWith (регистрозависимо)
            string full2 = m.SayHello("Donald Duck");
            Assert.StartsWith("Hello Don", full2); // проверяем начало строки

            // 3) EndsWith
            string full3 = m.SayHello("Uncle Scrooge");
            Assert.EndsWith("Scrooge", full3); // аналог StringAssert.EndsWith

            // 4) Matches (регулярное выражение)
            string full4 = m.SayHello("User123");
            Assert.Matches(new Regex(@"^Hello \w+\d*$"), full4); // аналог StringAssert.Matches
        }

        [Fact]
        public void Test_CollectionAssert_Equivalent_Functions()
        {
            var items = MyClass.GenerateItems();

            // 1) AreEqual (последовательности в том же порядке и количестве)
            var expectedNames = new List<string> { "Apple", "Banana", "Orange" };
            Assert.Equal(expectedNames, items.Select(i => i.Name).ToList());

            // 2) AreEquivalent (содержат одни и те же элементы, порядок не важен)
            var shuffled = new List<string> { "Orange", "Apple", "Banana" };
            Assert.Equal(expectedNames.OrderBy(x => x), shuffled.OrderBy(x => x));

            // 3) Contains (коллекция содержит элемент)
            Assert.Contains("Banana", items.Select(i => i.Name));

            // 4) IsSubsetOf (проверим, что подмножество действительно является подмножеством)
            var subset = new List<string> { "Apple", "Banana" };
            Assert.True(subset.All(s => items.Select(i => i.Name).Contains(s)));
        }
    }
}