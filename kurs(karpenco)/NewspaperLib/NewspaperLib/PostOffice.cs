using System.Collections.Generic;

namespace NewspaperLib
{
    public class PostOffice
    {
        public int ID { get; set; }
        public int Number { get; set; }
        public string Address { get; set; }
        public List<Newspaper> Newspapers { get; set; } = new List<Newspaper>();
    }
}