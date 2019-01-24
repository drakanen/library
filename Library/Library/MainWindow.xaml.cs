using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Library
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Control con = new Control(Environment.CurrentDirectory + "/books.txt");
        Book b = new Book();
        public MainWindow()
        {
            InitializeComponent();
            lstBooks.ItemsSource = con.GetAllBooks();
            this.DataContext = b;
        }

        //Button to search for a book
        private void btnSearchBook(object sender, RoutedEventArgs e)
        {
            try
            {
                Book foundBook; //Find the book using the inputed ISBN
                foundBook = con.GetBook(txtSearch.Text);
                try
                {
                    lstInfo.Items.Clear(); //Clear the right info box
                    if (!foundBook.CheckedOut) //If the book is available for checkout
                        lstInfo.Items.Add(foundBook.ToStringBookAvailable());
                    else //If the book is not available for checkout
                        lstInfo.Items.Add(foundBook.ToStringBookUnavailable());
                    txtDescription.Text = foundBook.Description; //Book description
                    lstBooks.SelectedItem = foundBook; //Select the searched book in the left box
                }
                catch (NullReferenceException) { txtDescription.Text = "Book not found."; }
            }
            catch (FormatException em)
            {
                Console.WriteLine(em.Message);
            }
        }

        //Update the list of books
        public void UpdateList()
        {
            lstBooks.ItemsSource = null;
            lstBooks.ItemsSource = con.GetAllBooks();
        }

        //Button to add a book
        private void btnAddBook(object sender, RoutedEventArgs e)
        {
            AddBookWindow addBook = new AddBookWindow(this);
            addBook.ShowDialog();
        }

        //Button to remove a book
        private void btnRemoveBook(object sender, RoutedEventArgs e)
        {
            Book selectedBook = (Book)lstBooks.SelectedItem;
            con.DeleteBook(selectedBook);
            UpdateList();
        }

        //Button to check out a book
        private void btnCheckOutBook(object sender, RoutedEventArgs e)
        {
            Book selectedBook = (Book)lstBooks.SelectedItem;
            con.CheckOutBook(selectedBook);
            lstInfo.Items.Clear();
            lstInfo.Items.Add(selectedBook.ToStringBookUnavailable());
        }

        //Button to return a book
        private void btnReturnBook(object sender, RoutedEventArgs e)
        {
            Book selectedBook = (Book)lstBooks.SelectedItem;
            con.ReturnBook(selectedBook);
            lstInfo.Items.Clear();
            lstInfo.Items.Add(selectedBook.ToStringBookAvailable());
        }

        //Selecting a book in the left list box
        private void selectBook(object sender, SelectionChangedEventArgs e)
        {
            lstInfo.Items.Clear();
            Book selectedBook = (Book)lstBooks.SelectedItem;

            try
            {
                if (!selectedBook.CheckedOut)
                    lstInfo.Items.Add(selectedBook.ToStringBookAvailable());
                else
                    lstInfo.Items.Add(selectedBook.ToStringBookUnavailable());
                txtDescription.Text = selectedBook.Description;
            }
            catch (NullReferenceException){ txtDescription.Text = null; }
        }
    }
}