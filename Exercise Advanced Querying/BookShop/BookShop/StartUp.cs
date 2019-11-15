namespace BookShop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BookShop.Data.ViewModels;
    using Data;
    using Microsoft.EntityFrameworkCore;

    using Newtonsoft.Json;
    using AutoMapper;
    using BookShop.Models;

    public class StartUp
    {
        public static void Main()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Book, BookDTO>()
                    .ForMember(dest=>dest.Name, opt=>
                        opt.MapFrom(src=>src.Title));
            });

            using (var db = new BookShopContext())
            {
                var book = db
                    .Books
                    .Include(b=>b.Author)
                    .First();

                var bookDto = Mapper.Map<BookDTO>(book);

                //var bookDto = new BookDTO()
                //{ 
                //    Name = "Title",
                //    Price = 10m
                //};

                //var book = Mapper.Map<Book>(bookDto);


                string result = JsonConvert.SerializeObject(bookDto, Formatting.Indented);

                Console.WriteLine(result);

            }
        }

        //Ploblem 1
        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            StringBuilder sb = new StringBuilder();

            var result = context
                .Books
                .Where(b => b.AgeRestriction.ToString().ToLower() == command.ToLower())
                .Select(b => b.Title)
                .ToList();

            foreach (var book in result.OrderBy(b => b))
            {
                sb.AppendLine(book);
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 2
        public static string GetGoldenBooks(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var books = context
                .Books
                .Where(b => b.EditionType.ToString() == "Gold")
                .Where(b => b.Copies < 5000)
                .Select(b => new
                {
                    b.Title,
                    b.BookId
                })
                .OrderBy(b => b.BookId)
                .ToList();

            foreach (var book in books)
            {
                sb.AppendLine(book.Title);
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 3
        public static string GetBooksByPrice(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var books = context
                .Books
                .Select(b => new
                {
                    b.Title,
                    b.Price
                })
                .Where(b => b.Price > 40)
                .OrderByDescending(b => b.Price)
                .ToList();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - ${book.Price}");
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 4
        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            StringBuilder sb = new StringBuilder();

            var books = context
                .Books
                .Select(b => new
                {
                    b.Title,
                    b.ReleaseDate,
                    b.BookId
                })
                .Where(b => b.ReleaseDate.Value.Year != year)
                .OrderBy(b => b.BookId)
                .ToList();

            foreach (var book in books)
            {
                sb.AppendLine(book.Title);
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 5
        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            var categories = input
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(c => c.ToLower())
                .ToList();

            var books = context
                .Books
                .Where(b => b.BookCategories
                    .Any(bc => categories.Contains(bc.Category.Name.ToLower())))
                .Select(b => b.Title)
                .OrderBy(b => b)
                .ToList();

            return String.Join(Environment.NewLine, books);
        }

        //Problem 6
        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            StringBuilder sb = new StringBuilder();

            var books = context
                .Books
                .Select(b => new
                {
                    b.Title,
                    EditionType = b.EditionType.ToString(),
                    b.Price,
                    b.ReleaseDate
                })
                .Where(b => b.ReleaseDate < DateTime.ParseExact(date, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .OrderByDescending(b => b.ReleaseDate);

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - {book.EditionType} - ${book.Price:f2}");
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 7
        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var names = context
                .Authors
                .Where(a => a.FirstName.EndsWith(input))
                .Select(a => new
                {
                    FullName = a.FirstName + " " + a.LastName
                })
                .OrderBy(a => a.FullName)
                .ToList();

            var list = new List<string>();

            foreach (var name in names)
            {
                list.Add(name.FullName);
            }

            return string.Join(Environment.NewLine, list);

        }

        //Problem 8
        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var names = context
                .Books
                .Select(b => b.Title)
                .Where(b => b.ToLower().Contains(input.ToLower()))
                .OrderBy(b => b);

            return string.Join(Environment.NewLine, names);
        }

        //Problem 9
        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            StringBuilder sb = new StringBuilder();

            var bookAuthors = context
                .Books
                .Select(b => new
                {
                    b.Author.FirstName,
                    b.Author.LastName,
                    b.Title,
                    b.BookId
                })
                .Where(b => b.LastName.ToLower().StartsWith(input.ToLower()))
                .OrderBy(b => b.BookId)
                .ToList();

            foreach (var bookAuthor in bookAuthors)
            {
                sb.AppendLine($"{bookAuthor.Title} ({bookAuthor.FirstName + " " + bookAuthor.LastName})");
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 10
        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            var books = context
                .Books
                .Select(b => b.Title)
                .Where(b => b.Length > lengthCheck)
                .ToList();

            return books.Count;
        }

        //Problem 11
        public static string CountCopiesByAuthor(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var authorCopies = context
                .Authors
                .Select(a => new
                {
                    Copies = a.Books.Sum(b => b.Copies),
                    FullName = a.FirstName + " " + a.LastName
                })
                .OrderByDescending(ac => ac.Copies)
                .ToList();

            foreach (var a in authorCopies)
            {
                sb.AppendLine($"{a.FullName} - {a.Copies}");
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 12
        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var categoryProfit = context
                .Categories
                .Select(c => new
                {
                    Profit = c.CategoryBooks.Sum(cb => cb.Book.Copies * cb.Book.Price),
                    c.Name
                })
                .OrderByDescending(c => c.Profit)
                .ThenBy(c => c.Name)
                .ToList();

            foreach (var cp in categoryProfit)
            {
                sb.AppendLine($"{cp.Name} ${cp.Profit:f2}");
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 13
        public static string GetMostRecentBooks(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var categoriesAndBooks = context
                .Categories
                .Select(c => new
                {
                    Books = c.CategoryBooks
                    .OrderByDescending(cb => cb.Book.ReleaseDate)
                    .Take(3)
                    .Select(cb => new
                    {
                        cb.Book.ReleaseDate,
                        cb.Book.Title
                    }).ToList(),

                    c.Name
                })
                .OrderBy(c => c.Name)
                .ToList();

            foreach (var categoryAndBooks in categoriesAndBooks)
            {
                sb.AppendLine($"--{categoryAndBooks.Name}");

                foreach (var book in categoryAndBooks.Books.OrderByDescending(b => b.ReleaseDate))
                {
                    sb.AppendLine($"{book.Title} ({book.ReleaseDate.Value.Year})");
                }
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 14
        public static void IncreasePrices(BookShopContext context)
        {
            var books = context
                .Books
                .Where(b => b.ReleaseDate.Value.Year < 2010)
                .ToList();

            foreach (var book in books)
            {
                book.Price += 5;
            }

            context.SaveChanges();
        }

        //Problem 15
        public static int RemoveBooks(BookShopContext context)
        {
            var books = context
                .Books
                .Where(b => b.Copies < 4200)
                .ToList();

            var deleted = books.Count;

            context.Books.RemoveRange(books);

            context.SaveChanges();

            return deleted;
        }
    }
}
