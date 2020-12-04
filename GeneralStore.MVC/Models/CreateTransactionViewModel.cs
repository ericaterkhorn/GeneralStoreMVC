using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeneralStore.MVC.Models
{
    public class CreateTransactionViewModel
    {
        //Drop down products
        public IEnumerable<SelectListItem> Products { get; set; }
        //Selected product
        public int ProductID { get; set; }
        //Drop down customers
        public IEnumerable<SelectListItem> Customers { get; set; }
        //Selected customer
        public int CustomerID { get; set; }
    }
}