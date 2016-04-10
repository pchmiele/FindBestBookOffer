using System;
using System.Configuration;
using System.Linq;
using FindBestBookOffer.Amazon;
using FindBestBookOffer.Apress;
using FindBestBookOffer.Interfaces;
using FindBestBookOffer.ISBNSearch;
using FindBestBookOffer.Model;

namespace FindBestBookOffer
{
    internal class Program
    {
        private static FoundBooks FindBooksWithMatchingTitles(string bookTitle)
        {
            var isbnSearchServiceClient = new ISBNSearchServiceClient();
            var isbnSearchService = new ISBNSearchService(isbnSearchServiceClient);
            var foundBooks = isbnSearchService.FindBooksByTitle(bookTitle);

            return foundBooks;
        }

        private static ISearchResult FindApressOffer(Book book)
        {
            var appressClient = new ApressClient();
            var apressSite = new ApressSite(appressClient);

            return apressSite.FindBookByISBN(book.ISBN13);
        }

        private static ISearchResult FindAmazonOffer(Book book)
        {
            var appSettings = ValidatedAppSettings();
            var amazonClient = new AmazonClient(appSettings);
            var amazonSite = new AmazonSite(amazonClient);

            return amazonSite.FindBookByISBN(book.ISBN13);
        }

        private static AppSettings ValidatedAppSettings()
        {
            try
            {
                return new AppSettings();
            }
            catch (ConfigurationErrorsException e)
            {
                Console.WriteLine("Your app.config file is broken. Error: {0}", e.Message);
                Environment.Exit(1);
            }
            return null;
        }

        private static void FindBestOffer(Book book)
        {
            var offerFromApress = FindApressOffer(book);
            var offerFromAmazon = FindAmazonOffer(book);
            Console.WriteLine(book.ToString());
            Console.WriteLine(offerFromApress);
            Console.WriteLine(offerFromAmazon);
            ChooseBestOffer(offerFromAmazon, offerFromApress);
        }

        private static void ChooseBestOffer(ISearchResult amazonOffer, ISearchResult apressOffer)
        {
            if (amazonOffer == null && apressOffer == null)
            {
                Console.WriteLine("Sorry. Could not find best offer for you. Please try with another book title.");
                return;
            }

            Console.WriteLine("Best offer for you:");
            if (amazonOffer == null)
            {
                Console.WriteLine(apressOffer.ToString());
                return;
            }

            if (apressOffer == null)
            {
                Console.WriteLine(amazonOffer.ToString());
                return;
            }

            if (amazonOffer.BookPrice > apressOffer.BookPrice)
            {
                Console.WriteLine(apressOffer.ToString());
                return;
            }

            if (amazonOffer.BookPrice < apressOffer.BookPrice)
            {
                Console.WriteLine(amazonOffer.ToString());
                return;
            }

            Console.WriteLine("Two same price offers found:");
            Console.WriteLine(apressOffer.ToString());
            Console.WriteLine(amazonOffer.ToString());
        }

        private static void Help()
        {
            Console.WriteLine("Usage: FindBestBookOffer.exe \"Book title\" ");
        }

        private static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Help();
                return;
            }

            var bookTitle = args[0];
            var booksWithMatchingTitles = FindBooksWithMatchingTitles(bookTitle);
            if (booksWithMatchingTitles.ThereIsNoBooks())
            {
                Console.Write("Could not find book with provided title: {0}", bookTitle);
                return;
            }

            if (booksWithMatchingTitles.IsThereOnlyOneBook())
            {
                FindBestOffer(booksWithMatchingTitles.Single());
            }
            else
            {
                var book = DecideWhichBookOfferUserIsLookingFor(booksWithMatchingTitles);
                FindBestOffer(book);
            }
        }

        private static Book DecideWhichBookOfferUserIsLookingFor(FoundBooks foundBooks)
        {
            while (true)
            {
                Console.WriteLine(foundBooks.ToString());
                Console.WriteLine("There are many books with title similar to which you provided.");
                Console.WriteLine("Please choose BOOK_ID of book you are looking for from above list: ");

                var line = Console.ReadLine();
                var bookNumber = 0;
                if (int.TryParse(line, out bookNumber))
                {
                    if (bookNumber >= 1 && bookNumber <= foundBooks.Count)
                    {
                        return foundBooks[bookNumber - 1];
                    }
                    Console.WriteLine("Please provide correct BOOK_ID");
                }
                else
                {
                    Console.WriteLine("Wrong BOOK_ID format. BOOK_ID should be a number");
                }
            }
        }
    }
}