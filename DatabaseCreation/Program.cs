using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCreation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var db = new CustomerContext())
            {
                Console.WriteLine("Enter first name of customer");
                var name = Console.ReadLine();

                Console.WriteLine("Enter last name of customer");
                var last = Console.ReadLine();
                
                var customer = new Customer { FirstName = name, LastName = last };
                db.Customers.Add(customer);
                
                //this line doesnt work -- other fields need to be filled out, maybe make some nullable in script and execute again??
                try 
                {                     
                    db.SaveChanges();                 
                }
                
                catch(Exception e)
                {
                    
                }



                var cust = from x in db.Customers
                           select x;

                Console.WriteLine("All customers:");

                foreach(var x in cust)
                {
                    Console.WriteLine(x);
                }
            }
        }
    }
}
