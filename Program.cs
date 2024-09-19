using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BOOK.classes;
using BOOKWORM.classes;

class Program
{
    static List<Bookworm> bookworm = new List<Bookworm>();
    static List<Book> books = new List<Book>
    {
        new Book("The Great Gatsby", "Classic", "F. Scott Fitzgerald", 5),
        new Book("1984", "Dystopian", "George Orwell", 10),
        new Book("To Kill a Mockingbird", "Drama", "Harper Lee", 7)
    };

    static void Main()
    {
        try
        {
            while (true)
            {
                Console.WriteLine("1. Registratsiyadan o'tish");
                Console.WriteLine("2. Kirish");
                Console.WriteLine("3. Foydalanuvchilar");
                Console.WriteLine("4. Chiqish");
                Console.Write("Raqamni tanlang: ");

                int tanlov;
                if (!int.TryParse(Console.ReadLine(), out tanlov))
                {
                    Console.WriteLine("Iltimos, raqam kiriting.");
                    continue;
                }

                switch (tanlov)
                {
                    case 1:
                        RegisterUser();
                        break;

                    case 2:
                        LoginUser();
                        break;

                    case 3:
                        ShowUsers();
                        break;

                    case 4:
                        return; 

                    default:
                        Console.WriteLine("Noto'g'ri tanlov.");
                        break;
                }

                SaveBooksToFile(); 
            }
        }
        catch (Exception error)
        {
            Console.WriteLine(error.Message); 
        }
    }

    static void RegisterUser()
    {
        Console.WriteLine("Ismingizni kiriting:");
        string enterName = Console.ReadLine();
        Console.WriteLine("Password kiriting:");
        string enterPassword = Console.ReadLine();
        Bookworm newBookworm = new Bookworm(enterName, enterPassword);
        bookworm.Add(newBookworm); 

        Console.WriteLine("Siz ro'yxatdan o'tdingiz.");
    }

    static void LoginUser()
    {
        Console.WriteLine("Ismingizni kiriting:");
        string loginName = Console.ReadLine();
        Console.WriteLine("Password kiriting:");
        string loginPassword = Console.ReadLine();

        var user = bookworm.FirstOrDefault(b => b.Name == loginName && b.Password == loginPassword);
        if (user != null)
        {
           

            while (true)
            
            {
                Console.WriteLine($"Xush kelibsiz, {user.Name}!");
                Console.WriteLine("1. Yangi kitob olish");
                Console.WriteLine("2. Kitobni qaytarish");
                Console.WriteLine("3. Chiqish");
                Console.Write("Raqamni tanlang: ");

                int checkBook = int.Parse(Console.ReadLine());
                switch (checkBook)
            {
                case 1:
                nextLevel(user);
                    break;

                case 2:
                returnBook(user);
                    break;

                case 3:
                return;
            }
            }
        }
        else
        {
            Console.WriteLine("Ism yoki parol xato.");
        }
    }

    static void ShowUsers()
    {
        Console.WriteLine("Foydalanuvchilar ro'yxati:");
        foreach (var user in bookworm)
        {
            Console.WriteLine($"Foydalanuvchi: {user.Name}");
            if (user.BorrowedBooks.Any())
            {
                Console.WriteLine("Olingan kitoblar:");
                foreach (var book in user.BorrowedBooks)
                {
                    Console.WriteLine($"- {book.Name} ({book.Total - (book.Total - 1)})");
                }
            }
            else
            {
                Console.WriteLine("Olingan kitoblar mavjud emas.");
            }
        }
    }

    static void SaveBooksToFile()
    {
        string filePath = "books_info.txt";

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var book in books)
            {
                writer.WriteLine(book.ToString());
            }
        }
    }

    static void nextLevel(Bookworm user)
    {
        Console.WriteLine("Mavjud kitoblar:");
        foreach (var item in books)
        {
            Console.WriteLine($"{item.Name} (Jami: {item.Total})"); 
        }

        Console.WriteLine("Qaysi kitobni o'qimoqchisiz?");
        string bookName = Console.ReadLine();

        var foundBook = books.FirstOrDefault(b => b.Name == bookName);

        if (foundBook != null && foundBook.Total > 0)
        {
            foundBook.Total -= 1; 
            user.BorrowedBooks.Add(foundBook); 
            Console.WriteLine($"{bookName} kitobi sizga tayyorlandi. Yangi soni: {foundBook.Total}");
            if (foundBook.Total == 0)
            {
                books.Remove(foundBook);
                Console.WriteLine($"{bookName} kitobi endi ro'yxatda mavjud emas.");
            }
        }
        else
        {
            Console.WriteLine("Bunday kitob topilmadi yoki kitobning mavjud nusxalari tugagan.");
        }
    }
    static void returnBook(Bookworm user)
    {
        Console.WriteLine("Qaysi kitobni qaytarmoqchisiz (nomi): ");
        string nameBook = Console.ReadLine();

        var bookToRemove = user.BorrowedBooks.FirstOrDefault(item => item.Name == nameBook);

        if (bookToRemove != null)
        {
            user.BorrowedBooks.Remove(bookToRemove);
            bookToRemove.Total += 1;
            Console.WriteLine($"{nameBook} kitobi muvaffaqiyatli qaytarildi.  Yangi soni: {bookToRemove.Total}");
        }
        else
        {
            Console.WriteLine($"{nameBook} kitobi topilmadi.");
        }
   }
}
