//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DatabaseCreation
{
    using System;
    using System.Collections.Generic;
    
    public partial class CustomerAddress
    {
        public string AddressType { get; set; }
        public string ModifiedDate { get; set; }
        public int CustomerCustomerID { get; set; }
        public int AddressAddressID { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Address Address { get; set; }
    }
}
