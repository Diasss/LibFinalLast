using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LiteDB;

namespace Library.Model
{
    public class Libraryy
    {
        public Libraryy()
        {
            while (true)
            {
                Console.WriteLine("Добро пожаловать!\n1)Зарегестрироваться\n2)Войти\n3)Выйти");
                string chose = Console.ReadLine();
                if (chose == "1")
                {
                    AdminService.CreateReader();
                }
                else if (chose == "2")
                {
                    Console.WriteLine("Авторизоваться под админом или читателем?\n1)админ\n2)читатель");
                    string adminOrReader = Console.ReadLine();
                    if (adminOrReader == "2")
                    {
                        Console.WriteLine("Введите логин: ");
                        string login = Console.ReadLine();
                        Console.WriteLine("Введите пароль: ");
                        string password = Console.ReadLine();
                        using (var db = new LiteDatabase(@"Readers.db"))
                        {
                            var col = db.GetCollection<Reader>("Reader");

                            var result = col.FindAll();
                            foreach (Reader r in result)
                            {
                                if (r.login == login && r.password == password)
                                {
                                    Console.WriteLine("Вы успешно авторизовались!");
                                    while (true)
                                    {
                                        Console.Clear();

                                        Console.WriteLine("1)Взять книгу\n2)Поменять пароль\n3)Издать книгу\n4)Выйти");
                                        string chose2 = Console.ReadLine();
                                        if (chose2 == "1")
                                        {
                                            BookService.SearchBook(r);
                                        }
                                        else if (chose2 == "2")
                                        {
                                            AdminService.ChangePasswordToReader();
                                        }
                                        else if (chose2 == "3")
                                        {
                                            ReaderService.CreateNewBookReader(r);
                                        }
                                        else if (chose2 == "4")
                                        {
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Повторите пароль заново");
                                }
                            }
                        }
                    }
                    else if (adminOrReader == "1")
                    {
                        Console.WriteLine("Введите логин: ");
                        string login = Console.ReadLine();
                        Console.WriteLine("Введите пароль: ");
                        string password = Console.ReadLine();
                        using (var db = new LiteDatabase(@"Readers.db"))
                        {
                            var col = db.GetCollection<Admin>("Admin");

                            var result = col.FindAll();
                            foreach (Admin a in result)
                            {
                                if (a.login == login && a.password == password)
                                {
                                    Console.WriteLine("Вы успешно авторизовались!");
                                    Thread.Sleep(2000);
                                    Console.Clear();

                                    Console.WriteLine("Что сделать?\n1)Добавить пользователя\n2)Поменять пароль пользователю\n3)Забанить пользоветаля\n4)Поменять свой пароль");
                                    string chose1 = Console.ReadLine();
                                    if (chose1 == "3")
                                    {
                                        AdminService.BanReader(ReaderService.SearchReader());
                                    }
                                    else if (chose1 == "1")
                                    {
                                        AdminService.CreateReader();
                                    }
                                    else if (chose1 == "2")
                                    {
                                        AdminService.ChangePasswordToReader();
                                    }
                                    else if (chose1 == "4")
                                    {
                                        AdminService.ChangeAdminPassword();
                                    }

                                }
                                else
                                {
                                    Console.WriteLine("Повторите пароль заново");
                                }
                            }
                        }
                    }
                    else if (adminOrReader == "3")
                    {
                        break;
                    }
                }
            }

        }
    }
}