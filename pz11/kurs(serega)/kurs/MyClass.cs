using System;
using System.Collections.Generic;

namespace Prac11
{
    public class Item
    {
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }

    public class MyClass
    {
        public string SayHello(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name), "Имя параметра не может быть пустым");
            }
            return "Hello " + name;
        }

        public static List<Item> GenerateItems()
        {
            return new List<Item>
            {
                new Item { Name = "Apple", Quantity = 5 },
                new Item { Name = "Banana", Quantity = 1 },
                new Item { Name = "Orange", Quantity = 3 }
            };
        }
    }
}