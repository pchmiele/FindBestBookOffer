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

            var app = new FindBestBookOffer();
            app.Run(bookTitle);
        }
    }
}