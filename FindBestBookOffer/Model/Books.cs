using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindBestBookOffer.Model
{
    public class FoundBooks : List<Book>
    {
        public bool IsThereOnlyOneBook()
        {
            return Count == 1;
        }

        public bool ThereIsNoBooks()
        {
            return !this.Any();
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            var bookId = 1;
            foreach (var book in this.ToList())
            {
                stringBuilder.AppendLine(string.Format("{0} - BOOK_ID", bookId));
                stringBuilder.AppendLine(book.ToString());
                stringBuilder.AppendLine();
                bookId++;
            }

            return stringBuilder.ToString();
        }
    }
}