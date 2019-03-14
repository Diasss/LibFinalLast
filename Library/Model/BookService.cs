﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace Library.Model
{
    public class BookService
    {
        public static void CreateBook(Reader reader)
        {
           
            using (var db = new LiteDatabase(@"Readers.db"))
            {
                var col = db.GetCollection<Book>("Book");

                Book book = new Book();
                book.author = reader.name;
                book.publishDate = DateTime.Now.Date;
                Console.WriteLine("Введите название книги: ");
                book.name = Console.ReadLine();
                book.code = book.name.Substring(0, (book.name.Length / 2)) + reader.name.Substring(0, (reader.name.Length) / 2);

                col.Insert(book);
            }
        }
        public static void PrintInfoAboutBook(Book book)
        {
            Console.WriteLine("Название книги: {0}", book.name);
            Console.WriteLine("Код книги: {0}", book.code);
            Console.WriteLine("Автор книги: {0}", book.author);
            Console.WriteLine("Дата публикации книги: {0}", book.publishDate);
        }
        public static void SearchBook(Reader r)
        {
            Console.WriteLine("Введите код книги для поиска: ");
            string bookCode = Console.ReadLine();
            string question;
            using (var db = new LiteDatabase(@"Readers.db"))
            {
                var col = db.GetCollection<Book>("Book");

                var result = col.FindAll();
                foreach (Book b in result)
                {
                    if (b.code == bookCode)
                    {
                        PrintInfoAboutBook(b);
                        Console.WriteLine("Хотите ли вы взять книгу?(y-да, взять; n-нет, не брать): ");
                        question = Console.ReadLine();
                        if (question == "y")
                        {
                            using (var dbr = new LiteDatabase(@"Readers.db"))
                            {
                                var colr = db.GetCollection<Reader>("Reader");

                                var resultr = colr.FindAll();

                                r.tokenBooks = new SortedList<Book, bool>();
                                r.tokenBooks.Add(b, true);

                                colr.Update(r);
                            }
                        }
                        else if (question == "n")
                        {
                            Console.WriteLine("Вы не взяли книгу!");
                        }
                    }
                }
            }
        }

    }
}
