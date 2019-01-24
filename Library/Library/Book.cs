using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class Book
    {
        //Auto-implemented variables to hold book information
        public string Name { get; set; }
        public string Description { get; set; }
        public string Isbn { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
        public bool CheckedOut { get; set; }
        public string FileReaderForCheckedOut { get; set; }
        public string checkedOutBy; //Currently unused

        //Constructor
        public Book(string name, string author, string isbn, string genre, string desc)
        {
            Name = name;
            Description = desc;
            Isbn = isbn;
            Genre = genre;
            Author = author;
        }

        //No-arg constructor that sets everything to nothing
        public Book()
        {
            Name = "";
            Description = "";
            Isbn = "";
            Genre = "";
            Author = "";
            FileReaderForCheckedOut = "";
        }

        //Unused property
        public string CheckedOutBy
        {
            get
            {
                return checkedOutBy;
            }
            set
            {
                if (CheckedOut == true)
                    checkedOutBy = value;
            }
        }

        //ToString function if the book is not available
        public string ToStringBookUnavailable()
        {
            return $"Book is not available.\nISBN: {Isbn} \nName: {Name}\nAuthor: {Author}\n" +
                $"Genre: {Genre}\nDescription:";
        }

        //ToString function if the book is available
        public  string ToStringBookAvailable()
        {
            return $"Book is available.\nISBN: {Isbn} \nName: {Name}\nAuthor: {Author}\n" +
                $"Genre: {Genre}\nDescription:";
        }

        //ToString function that displays some info about the book
        public override string ToString()
        {
            return $"ISBN: {Isbn}\nName: {Name}\nGenre: {Genre}";
        }
    }
}
