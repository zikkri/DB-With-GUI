using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Interaction logic for UpdateCustomer.xaml
    /// </summary>
    ///     
    public partial class UpdateCustomer : Window
    {
        
        int custID = 0;
        int addID = 0;       

        //maybe do this for the delete from table 
        string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\CustomerModel.mdf;MultipleActiveResultSets=true;Integrated Security=True";

        public UpdateCustomer()
        {
            InitializeComponent();
        }

        private void findBtn_Click(object sender, RoutedEventArgs e)
        {
            //search for customer
            using (var db = new CustomerModel())
            {                
                int num = 0;
                bool found = false;

                if(findCust.Text == "" || findCust.Text == null)
                {
                    MessageBox.Show("Please enter a customer ID.");
                    return;
                }

                else
                {
                    num = Convert.ToInt32(findCust.Text);
                }
                
                foreach(Customer x in db.Customers)
                {
                    if(x.CustomerID == num)
                    {                        
                        found = true;
                        custID = x.CustomerID;


                        nameStyle.Text = x.NameStyle;
                        title.Text = x.Title;
                        fName.Text = x.FirstName;
                        mName.Text = x.MiddleName;
                        lName.Text = x.LastName;    
                        compName.Text = x.CompanyName;  
                        salesPerson.Text = x.SalesPerson;
                        email.Text = x.EmailAddress;
                        phone.Text = x.Phone;
                        password.Text = x.Password;                        

                        foreach(CustomerAddress y in db.CustomerAddresses)
                        {
                            if(y.CustomerCustomerID == num)
                            {
                                foreach(Address z in db.Addresses)
                                {
                                    if(y.AddressAddressID == z.AddressID)
                                    {
                                        addID = z.AddressID;
                                        
                                        addLine1.Text = z.AddressLine1;
                                        addLine2.Text = z.AddressLine2;
                                        city.Text = z.City;
                                        stateProv.Text = z.StateProvince;
                                        countryRegion.Text = z.CountryRegion;
                                        postalCode.Text = z.PostalCode;
                                        addType.Text = y.AddressType;
                                        modDate.Text = y.ModifiedDate;
                                    }
                                }
                            }
                        }
                    }
                }
                
                if(!found)
                {
                    MessageBox.Show("Sorry that is an invalid entry.");
                }
            }
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

                
                //establish connection
                SqlConnection connection = new SqlConnection(conn);                

                //customer stuff
                SqlCommand customerCommand = new SqlCommand("update Customers " +
                    "set Namestyle = @namestyle, Title = @title, " +
                    "FirstName = @fname, MiddleName = @mname, LastName = @lname, " +
                    "CompanyName = @compname, SalesPerson = @salesperson, EmailAddress = @email, " +
                    "Phone = @phone, Password = @password " +
                    "where CustomerID = @custID", connection);

                customerCommand.Parameters.AddWithValue("@namestyle", newCustomer.NameStyle);
                customerCommand.Parameters.AddWithValue("@title", newCustomer.Title);
                customerCommand.Parameters.AddWithValue("@fname", newCustomer.FirstName);
                customerCommand.Parameters.AddWithValue("@mname", newCustomer.MiddleName);
                customerCommand.Parameters.AddWithValue("@lname", newCustomer.LastName);
                customerCommand.Parameters.AddWithValue("@compname", newCustomer.CompanyName);
                customerCommand.Parameters.AddWithValue("@salesperson", newCustomer.SalesPerson);
                customerCommand.Parameters.AddWithValue("@email", newCustomer.EmailAddress);
                customerCommand.Parameters.AddWithValue("@phone", newCustomer.Phone);
                customerCommand.Parameters.AddWithValue("@password", newCustomer.Password);
                customerCommand.Parameters.AddWithValue("@custID", custID);

                //addresss stuff
                SqlCommand addressCommand = new SqlCommand("update Addresses " +
                    "set AddressLine1 = @add1, AddressLine2 = @add2, " +
                    "City = @city, StateProvince = @stateprov, CountryRegion = @countreg, PostalCode = @postal " +
                    "where AddressID = @addID", connection);

                addressCommand.Parameters.AddWithValue("@add1", customerAddress.AddressLine1);
                addressCommand.Parameters.AddWithValue("@add2", customerAddress.AddressLine2);
                addressCommand.Parameters.AddWithValue("@city", customerAddress.City);
                addressCommand.Parameters.AddWithValue("@stateprov", customerAddress.StateProvince);
                addressCommand.Parameters.AddWithValue("@countreg", customerAddress.CountryRegion);
                addressCommand.Parameters.AddWithValue("@postal", customerAddress.PostalCode);
                addressCommand.Parameters.AddWithValue("@addID", addID);
                
                //bridge table stuff
                SqlCommand comboCommand = new SqlCommand("update CustomerAddresses " +
                    "set AddressType = @addtype, ModifiedDate = @moddate " +
                    "where CustomerCustomerID = @custID and AddressAddressID = @addID", connection);

                comboCommand.Parameters.AddWithValue("@addtype", custAddItem.AddressType);
                comboCommand.Parameters.AddWithValue("@moddate", custAddItem.ModifiedDate);
                comboCommand.Parameters.AddWithValue("@custID", custID);
                comboCommand.Parameters.AddWithValue("@addID", addID);

                //open connection to DB
                connection.Open();
                
                //execute all updates
                customerCommand.ExecuteNonQuery();
                addressCommand.ExecuteNonQuery();
                comboCommand.ExecuteNonQuery();

                //save before exiting, save again after?? not sure if necassary but Im paranoid
                db.SaveChanges();
                connection.Close();
                db.SaveChanges();

                //display success and close window
                MessageBox.Show("Customer updated successfully!");
                Close();
            }
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            //delete customer from database            
            using (var db = new CustomerModel())
            {
                //establish connection
                SqlConnection connection = new SqlConnection(conn);

                //customer stuff                
                SqlCommand customerDeleteCommand = new SqlCommand("delete from Customers where CustomerID = @custID", connection);
                customerDeleteCommand.Parameters.AddWithValue("@custID", custID);

                //address stuff                
                SqlCommand addressDeleteCommand = new SqlCommand("delete from Addresses where AddressID = @addID", connection);
                addressDeleteCommand.Parameters.AddWithValue("@addID", addID);

                //bridge table stuff                
                SqlCommand comboDeleteCommand = new SqlCommand("delete from CustomerAddresses where CustomerCustomerID = @custID and AddressAddressID = @addID", connection);
                comboDeleteCommand.Parameters.AddWithValue("@custID", custID);
                comboDeleteCommand.Parameters.AddWithValue("@addID", addID);

                //open connection to DB
                connection.Open();
                
                //execute all updates
                comboDeleteCommand.ExecuteNonQuery();
                customerDeleteCommand.ExecuteNonQuery();
                addressDeleteCommand.ExecuteNonQuery();
                

                //save before exiting, save again after?? not sure if necassary but Im paranoid
                db.SaveChanges();
                connection.Close();
                db.SaveChanges();

                //display success and close window
                MessageBox.Show("Customer deleted successfully!");
                Close();
            }
        }
    }
}