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

namespace CustomerGUI
{
    /// <summary>
    /// Interaction logic for ListCustomers.xaml
    /// </summary>
    public partial class ListCustomers : Window
    {
        public ListCustomers()
        {
            InitializeComponent();            
        }

        private void listAllBtn_Click(object sender, RoutedEventArgs e)
        {
            //show all customers
            using (var db = new CustomerModel())
            {                
                var list = from x in db.Customers
                           select x;

                List<Customer> c = new List<Customer>();

                foreach(var x in list)
                {
                    c.Add(x);
                }

                dataG.ItemsSource = c;
            }
        }

        private void canadianBtn_Click(object sender, RoutedEventArgs e)
        {
            //show only canadian customers
            using (var db = new CustomerModel())
            {

                var list = from x in db.Customers
                           from y in db.Addresses
                           from z in db.CustomerAddresses
                           where x.CustomerID == z.CustomerCustomerID
                           && z.AddressAddressID == y.AddressID
                           && y.CountryRegion.ToLower() == "canada"
                           select x;

                List<Customer> c = new List<Customer>();

                foreach (var x in list)
                {
                    c.Add(x);
                }

                dataG.ItemsSource = c;
            }
        }

        private void sortFNameBtn_Click(object sender, RoutedEventArgs e)
        {
            //sort by first name and display
            using (var db = new CustomerModel())
            {
                var list = from x in db.Customers
                           orderby x.FirstName
                           select x;

                List<Customer> c = new List<Customer>();

                foreach (var x in list)
                {
                    c.Add(x);
                }

                dataG.ItemsSource = c;
            }
        }

        private void sortCompanyBtn_Click(object sender, RoutedEventArgs e)
        {
            //sort by company and display
            using (var db = new CustomerModel())
            {
                var list = from x in db.Customers
                           orderby x.CompanyName
                           select x;

                List<Customer> c = new List<Customer>();

                foreach (var x in list)
                {
                    c.Add(x);
                }

                dataG.ItemsSource = c;
            }
        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {            
            //close window
            Close();
        }
    }
}
