using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Library
{
    class Control
    {
        List<Book> books;
        string pathName;

        //Constructor
        public Control(string path)
        {
            pathName = path;
            try
            {
                books = LoadFile(); //Get the list of books
            }
            catch (FileNotFoundException)
            {
                File.WriteAllText(pathName, ""); //Create the file to hold the list of books
                books = LoadFile(); //Load the empty file
            }
        }

        //Loads and returns the list of books from the file
        public List<Book> GetAllBooks()
        {
            books = LoadFile();
            return books;
        }

        //Opens a predetermined file and reads the book information from it
        //Creates book objects and adds them to a list before returning the list
        public List<Book> LoadFile()
        {
            List<Book> books = new List<Book>();
            if (File.Exists(pathName))
            {
                StreamReader sr = File.OpenText(pathName); //Streamreader to read the file
                string line; //Holds a line in the file
                string value = ""; //The current value to be looked at
                int i = 0; //Which element of the line string to look at

                //While the file is not empty,
                //parse through it and get all of the information to fill in a student object
                //then add it to a list and return the list when completed
                while (!sr.EndOfStream)
                {
                    Book b = new Book();
                    line = sr.ReadLine();
                    while (!line[i].Equals(";") && i < line.Length - 1)
                    {
                        if (line[i].Equals('%') && b.FileReaderForCheckedOut == "")
                        {
                            if (value == "True")
                                b.CheckedOut = true;
                            else
                                b.CheckedOut = false;
                            b.FileReaderForCheckedOut = "Checked";
                            value = "";
                        }

                        else if (line[i].Equals('%') && b.Isbn == "")
                        {
                            b.Isbn = value;
                            value = "";
                        }
                        else if (line[i].Equals('%') && b.Name == "")
                        {
                            b.Name = value;
                            value = "";
                        }
                        else if (line[i].Equals('%') && b.Author == "")
                        {
                            b.Author = value;
                            value = "";
                        }
                        else if (line[i].Equals('%') && b.Genre == "")
                        {
                            b.Genre = value;
                            value = "";
                        }
                        else if (line[i].Equals('%') && b.Description == "")
                        {
                            b.Description = value;
                            value = "";
                        }
                        else
                            value += line[i];
                        ++i;
                    }
                    books.Add(b);
                    //b = null;
                    i = 0;
                }
                sr.Close(); //Close the file
                sr.Dispose(); //Dispose of the streamreader
                return books;
            }
            else
                throw new FileNotFoundException();
        }

        //Save the book list to the file
        public void SaveToFile()
        {
            string toWrite = "";
            if (books != null)
            {
                foreach (Book b in books)
                {
                    toWrite += b.CheckedOut + "%";
                    toWrite += b.Isbn + "%";
                    toWrite += b.Name + "%";
                    toWrite += b.Author + "%";
                    toWrite += b.Genre + "%";
                    toWrite += b.Description + "%";
                    toWrite += ";";
                    toWrite += Environment.NewLine;
                }
            }
            File.WriteAllText(pathName, toWrite);
        }

        //Add the given book to the list
        public void AddBook(Book bk)
        {
            books.Add(bk);
        }

        //Find a book based on its ISBN
        public Book GetBook(string isbn)
        {
            foreach (var b in books)
            {
                if (b.Isbn == isbn)
                    return b;
            }
            return null;
        }

        //Delete the book matching the ISBN
        public void DeleteBook(string isbn)
        {
            Book b = null;
            foreach (var bo in books)
            {
                if (bo.Isbn == isbn)
                {
                    b = bo;
                    break;
                }
            }
            books.Remove(b);
            SaveToFile();
        }

        //Delete the given book
        public void DeleteBook(Book b)
        {
            books.Remove(b);
            SaveToFile();
        }

        //Check out the given book
        public void CheckOutBook(Book b, string name = "")
        {
            Book bo = b;
            bo.CheckedOut = true;
            bo.checkedOutBy = name;
            SaveToFile();
        }

        //Return the given book
        public void ReturnBook(Book b, string name = "")
        {
            Book bo = b;
            if (bo.CheckedOut == true)
            { 
                bo.CheckedOut = false;
                bo.checkedOutBy = "";
                SaveToFile();
            }
        }

        //Returns if the currently selected book is available
        public bool IsBookAvailable(string isbn)
        {
            Book b = GetBook(isbn);
            return b.CheckedOut;
        }
    }
}
