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

namespace CustomerGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();            
        }

        private void createBtn_Click(object sender, RoutedEventArgs e)
        {
            //create new xaml window to create a customer and submit there to db
            CreateCustomer c = new CreateCustomer();
            c.Show();
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            //maybe could use the same xaml as create button?
            UpdateCustomer u = new UpdateCustomer();
            u.Show();
        }

        private void listBtn_Click(object sender, RoutedEventArgs e)
        {
            //definitely another xaml for listing, needs to have buttons to perform functions 
            ListCustomers l = new ListCustomers();
            l.Show();
        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
