using System.Collections.Generic;
using System.Linq;
using DataServiceLib.DBObjects;
using DataServiceLib.IDataService;

namespace DataServiceLib.DataService
{
    public class BookmarkingDataService : IBookmarkingDataService
    {
        private readonly Raw11Context _db;

        public BookmarkingDataService(string connStr)
        {
            _db = new Raw11Context(connStr);
        }

        public IList<BookmarkPerson> GetBookmarks(int userId)  // gets all bookmark from 1 user
        {
            return _db.BookmarkPerson.Where(x => x.UserId == userId).ToList();
        }

        public BookmarkPerson GetBookMark(int userId, string nConst) // gets  1  unique bookmark
        {
            return _db.BookmarkPerson.FirstOrDefault(x => x.UserId == userId
                                                          && x.NConst == nConst);
        }

        public bool CreateBookmark(BookmarkPerson bookmarkPerson)
        {
            var dbBook = GetBookMark(bookmarkPerson.UserId, bookmarkPerson.NConst);
            if (dbBook == null) // if bookmark is empty, create bookmark return true
            {
                _db.Add(bookmarkPerson);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteBookmark(int userId, string nConst)
        {
            var dbBook = GetBookMark(userId, nConst);
            if (dbBook == null) // if bookmark is empty, return false
            {
                return false;
            }
            _db.BookmarkPerson.Remove(dbBook);
            _db.SaveChanges();
            return true;
        }
    }
}