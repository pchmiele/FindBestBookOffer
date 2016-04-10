namespace FindBestBookOffer.Model
{
    public class Book
    {
        private string author;

        private string isbn10;

        private string isbn13;

        internal Book()
        {
        }

        public string Title { get; internal set; }

        public string Author
        {
            get { return author; }
            internal set { author = value.Replace("Author: ", string.Empty); }
        }

        public string ISBN10
        {
            get { return isbn10; }
            internal set { isbn10 = value.Replace("ISBN-10: ", string.Empty); }
        }

        public string ISBN13
        {
            get { return isbn13; }
            internal set { isbn13 = value.Replace("ISBN-13: ", string.Empty); }
        }

        public override string ToString()
        {
            return string.Format("{0} \n{1} \n{2} \n{3}", Author, Title, ISBN13, ISBN10);
        }
    }
}