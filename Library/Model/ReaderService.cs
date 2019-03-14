using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace Library.Model
{
    class ReaderService
    {
        public static void CreateNewBookReader(Reader r)
        {
            BookService.CreateBook(r);
        }
        //public static void FindBook()
        //{
        //    BookService.SearchBook();
        //}
        //public void PassBook(Reader reader)
        //{
        //    using (var db = new LiteDatabase(@"Readers.db"))
        //    {
        //        var col = db.GetCollection<Reader>("Reader");

        //        var result = col.FindAll();
        //        foreach (Reader r in result)
        //        {
        //            if (reader == r)
        //            {
        //                r.tokenBooks.Remove(reader);
        //            }
        //        }
        //    }
        //}
        //public void InfoAboutReaders()
        //{
        //    Console.WriteLine("Пользователи, не сдавшие книги");
        //    using (var db = new LiteDatabase(@"Readers"))
        //    {
        //        var col = db.GetCollection<Reader>("Reader");

        //        var result = col.FindAll();
        //        foreach (Reader r in result)
        //        {
        //            if(r.tokenBooks.TryGetValue(b, false))
        //        }
        //    }
        //}
        public static Reader SearchReader()
        {
            Console.WriteLine("Введите email пользователя");
            string emailOfReader = Console.ReadLine();
            Reader readerTmp = new Reader();
            using (var db = new LiteDatabase(@"Readers.db"))
            {

                var col = db.GetCollection<Reader>("Reader");

                var result = col.FindAll();
                foreach (Reader r in result)
                {
                    if (r.email == emailOfReader)
                    {
                        readerTmp = r;
                    }
                    else
                        throw new Exception("Пользователь не найден!");
                }
            }
            return readerTmp;
        }
        //public static Reader SearchReaderByLogin(Reader r)
        //{
        //    using (var db = new LiteDatabase(@"Readers.db"))
        //    {
        //        var col = db.GetCollection<Reader>("Reader");

        //        var result = col.FindAll();
        //        foreach (Reader r in result)
        //        {
        //            if (r.login == userLogin&&r.password == userPassword)
        //               return readerTmp;                       
        //        }

        //        throw new Exception("Пользователь не найден!");
        //    }
            
        //}
    }
}
