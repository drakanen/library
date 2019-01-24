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
using System.Windows.Shapes;

namespace Library
{
    /// <summary>
    /// Interaction logic for AddBookWindow.xaml
    /// </summary>
    public partial class AddBookWindow : Window
    {
        MainWindow parentForm;
        Control con = new Control(Environment.CurrentDirectory + "/books.txt");
        Book b = new Book();

        //Constructor
        public AddBookWindow(MainWindow window)
        {
            InitializeComponent();
            parentForm = window; //Make a parent window
            DataContext = b;
        }

        //Button to add the book
        private void btnAddBook(object sender, RoutedEventArgs e)
        {
            try
            { //Create a new book object with the filled in text boxes and add it
                con.AddBook(new Book(txtName.Text, txtAuthor.Text, txtIsbn.Text, txtGenre.Text, txtDescrption.Text));
                con.SaveToFile(); //Save to the file
                parentForm.UpdateList(); //Update the parent box list
                Close();
            }
            catch (FormatException fm)
            {
                Console.WriteLine(fm.Message);
            }
        }

        //Cancel the window
        private void btnCancelBook(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
