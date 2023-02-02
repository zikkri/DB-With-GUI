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
    /// Interaction logic for CreateCustomer.xaml
    /// </summary>
    public partial class CreateCustomer : Window
    {
        public CreateCustomer()
        {
            InitializeComponent();
        }

        private void submitBtn_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new CustomerModel())
            {
                var newCustomer = new Customer
                {
                    NameStyle = nameStyle.Text,
                    Title = title.Text,
                    FirstName = fName.Text,
                    MiddleName = mName.Text,
                    LastName = lName.Text,
                    CompanyName = compName.Text,
                    SalesPerson = salesPerson.Text,
                    EmailAddress = email.Text,
                    Phone = phone.Text,
                    Password = password.Text
                };

                var customerAddress = new Address
                {
                    AddressLine1 = addLine1.Text,
                    AddressLine2 = addLine2.Text,
                    City = city.Text,
                    StateProvince = stateProv.Text,
                    CountryRegion = countryRegion.Text, 
                    PostalCode = postalCode.Text
                };


                var custAddItem = new CustomerAddress
                {
                    AddressType = addType.Text,
                    ModifiedDate = modDate.Text,
                    CustomerCustomerID = newCustomer.CustomerID,
                    AddressAddressID = customerAddress.AddressID,
                    Address = customerAddress,
                    Customer = newCustomer
                };

                db.Customers.Add(newCustomer);                
                db.Addresses.Add(customerAddress);
                db.CustomerAddresses.Add(custAddItem);
                db.SaveChanges();

                MessageBox.Show("Customer added successfully!");
                Close();
            }
        }
    }
}
