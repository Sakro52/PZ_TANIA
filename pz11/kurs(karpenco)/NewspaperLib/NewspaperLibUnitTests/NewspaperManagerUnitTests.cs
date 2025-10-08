using System;
using System.Collections.Generic;
using System.Linq;
using NewspaperLibUnitTests;
using Xunit;
using System.Text.RegularExpressions;
using NewspaperLib;

namespace NewspaperLibUnitTests
{
    public class NewspaperManagerUnitTests
    {
        [Fact]
        public void TestAddNewspaper()
        {
            // Arrange
            var manager = new NewspaperManager();
            var newspaper = new Newspaper { Name = "Daily News", Index = "DN001", Editor = "John Doe", Price = 50 };

            // Act
            manager.AddNewspaper(newspaper);

            // Assert
            var newspapersList = manager.GetType().GetField("newspapers", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(manager) as List<Newspaper>;
            Assert.Single(newspapersList);
            Assert.Equal("Daily News", newspaper.Name);
        }

        [Fact]
        public void TestAddPostOffice()
        {
            // Arrange
            var manager = new NewspaperManager();
            var po = new PostOffice { Number = 1, Address = "Main St" };

            // Act
            manager.AddPostOffice(po);

            // Assert
            var postOfficesList = manager.GetType().GetField("postOffices", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(manager) as List<PostOffice>;
            Assert.Single(postOfficesList);
            Assert.Equal(1, po.ID);
            Assert.Equal(1, po.Number);
        }

        [Fact]
        public void TestAddNewspaperToPostOffice()
        {
            // Arrange
            var manager = new NewspaperManager();
            var po = new PostOffice { Number = 1, Address = "Main St" };
            manager.AddPostOffice(po);
            var newspaper = new Newspaper { Name = "Daily News", Index = "DN001", Editor = "John Doe", Price = 50 };

            // Act
            manager.AddNewspaperToPostOffice(po, newspaper);

            // Assert
            Assert.Single(po.Newspapers);
            Assert.Equal("Daily News", po.Newspapers[0].Name);
        }

        [Fact]
        public void TestRemoveNewspaper_Success()
        {
            // Arrange
            var manager = new NewspaperManager();
            var newspaper = new Newspaper { Name = "ToRemove" };
            manager.AddNewspaper(newspaper);

            // Act
            bool removed = manager.RemoveNewspaper("ToRemove");

            // Assert
            Assert.True(removed);
            var newspapersList = manager.GetType().GetField("newspapers", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(manager) as List<Newspaper>;
            Assert.Empty(newspapersList);
        }

        [Fact]
        public void TestGetEditorsWithMultipleNewspapers()
        {
            // Arrange
            var manager = new NewspaperManager();
            var newspaper1 = new Newspaper { Editor = "Editor1" };
            var newspaper2 = new Newspaper { Editor = "Editor1" };
            var newspaper3 = new Newspaper { Editor = "Editor2" };
            manager.AddNewspaper(newspaper1);
            manager.AddNewspaper(newspaper2);
            manager.AddNewspaper(newspaper3);

            // Act
            var result = manager.GetEditorsWithMultipleNewspapers().ToList();

            // Assert
            Assert.Single(result);
            Assert.Equal("Editor1", result[0].Editor);
            Assert.Equal(2, result[0].Count);
        }

        [Fact]
        public void TestGetNewspapersByPrice()
        {
            // Arrange
            var manager = new NewspaperManager();
            var newspaper1 = new Newspaper { Name = "Newspaper1", Price = 50 };
            var newspaper2 = new Newspaper { Name = "Newspaper2", Price = 60 };
            manager.AddNewspaper(newspaper1);
            manager.AddNewspaper(newspaper2);

            // Act
            var result = manager.GetNewspapersByPrice(50).ToList();

            // Assert
            Assert.Single(result);
            Assert.Equal("Newspaper1", result[0].Name);
        }

        [Fact]
        public void TestGetNewspapersByIndex()
        {
            // Arrange
            var manager = new NewspaperManager();
            var newspaper1 = new Newspaper { Name = "Newspaper1", Index = "IDX1" };
            var newspaper2 = new Newspaper { Name = "Newspaper2", Index = "IDX2" };
            manager.AddNewspaper(newspaper1);
            manager.AddNewspaper(newspaper2);

            // Act
            var result = manager.GetNewspapersByIndex("IDX1").ToList();

            // Assert
            Assert.Single(result);
            Assert.Equal("Newspaper1", result[0].Name);
        }

        [Fact]
        public void TestGetPostOfficeWithMaxNewspapers()
        {
            // Arrange
            var manager = new NewspaperManager();
            var po1 = new PostOffice { Newspapers = new List<Newspaper> { new Newspaper() } }; // 1
            var po2 = new PostOffice { Newspapers = new List<Newspaper> { new Newspaper(), new Newspaper(), new Newspaper() } }; // 3
            var po3 = new PostOffice { Newspapers = new List<Newspaper> { new Newspaper(), new Newspaper() } }; // 2
            manager.AddPostOffice(po1);
            manager.AddPostOffice(po2);
            manager.AddPostOffice(po3);

            // Act
            var result = manager.GetPostOfficeWithMaxNewspapers();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Newspapers.Count);  // Этот тест провалится из-за преднамеренной ошибки в методе; после исправления пройдет
        }
    }
}