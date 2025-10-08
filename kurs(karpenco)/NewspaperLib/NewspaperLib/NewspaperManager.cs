using System.Collections.Generic;
using System.Linq;

namespace NewspaperLib
{
    public class NewspaperManager
    {
        private List<Newspaper> newspapers = new List<Newspaper>();
        private List<PostOffice> postOffices = new List<PostOffice>();

        public void AddNewspaper(Newspaper newspaper)
        {
            if (newspaper != null)
            {
                newspapers.Add(newspaper);
            }
        }

        public void AddPostOffice(PostOffice po)
        {
            if (po != null)
            {
                po.ID = postOffices.Count + 1;
                postOffices.Add(po);
            }
        }

        public void AddNewspaperToPostOffice(PostOffice po, Newspaper newspaper)
        {
            if (po != null && newspaper != null)
            {
                po.Newspapers.Add(newspaper);
            }
        }

        public bool RemoveNewspaper(string name)
        {
            var n = newspapers.FirstOrDefault(a => a.Name == name);
            if (n != null)
            {
                newspapers.Remove(n);
                return true;
            }
            return false;
        }

        public IEnumerable<(string Editor, int Count)> GetEditorsWithMultipleNewspapers()
        {
            return newspapers.GroupBy(a => a.Editor)
                            .Where(g => g.Key != null && g.Count() > 1)
                            .Select(g => (g.Key, g.Count()));
        }

        public IEnumerable<Newspaper> GetNewspapersByPrice(decimal price)
        {
            return newspapers.Where(a => a.Price == price)
                            .Distinct();
        }

        public IEnumerable<Newspaper> GetNewspapersByIndex(string index)
        {
            return newspapers.Where(a => a.Index == index)
                            .Distinct();
        }

        public PostOffice GetPostOfficeWithMaxNewspapers()
        {
            // ❌ Ошибка специально, чтобы тест показал проблему: возвращаем postOffice с минимальным количеством газет вместо максимального
            return postOffices.OrderBy(a => a.Newspapers.Count).FirstOrDefault();
            // Для исправления: return postOffices.OrderByDescending(a => a.Newspapers.Count).FirstOrDefault();
        }

        // Дополнительный метод для тестов
        public IEnumerable<Newspaper> GetAllNewspapers()
        {
            return newspapers;
        }
    }
}