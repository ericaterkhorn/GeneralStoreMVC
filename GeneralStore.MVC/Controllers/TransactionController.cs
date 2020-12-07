using GeneralStore.MVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeneralStore.MVC.Controllers
{
    public class TransactionController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Transaction
        public ActionResult Index()
        {
            return View(_db.Transactions.ToArray());
        }
        //GET: Transaction/{id}
        [Route("{id}")]
        public ActionResult Details(int id)
        {
            Transaction transaction = _db.Transactions.Find(id);
            if(transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }
        //Viewdata/viewbags
        //GET: Transaction/Create
        public ActionResult Create()
        {
            var viewModel = new CreateTransactionViewModel();
            viewModel.Products = _db.Products.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.ProductID.ToString()
            });
            viewModel.Customers = _db.Customers.Select(p => new SelectListItem
            {
                Text = p.FullName,
                Value = p.CustomerID.ToString()
            });
            return View(viewModel);
        }

        //POST: Transaction/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateTransactionViewModel viewModel)
        {
            _db.Transactions.Add(new Transaction
            {
                CustomerID = viewModel.CustomerID,
                ProductID = viewModel.ProductID
            });
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET: Transaction/delete/id
        [Route("delete/{id}")]
        public ActionResult Delete([Required] int id)
        {
            Transaction transaction = _db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }

            return View(transaction);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Transaction model)
        {
            var transaction = _db.Transactions.Find(model.TransactionID);
            _db.Transactions.Remove(transaction);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Route("edit/{id}")]
        public ActionResult Edit(int id)
        {
            Transaction transaction = _db.Transactions.Find(id);
            if (transaction == null)
            {
                return View("NotFound");
            }
            ViewData["Customers"] = _db.Customers.Select(p => new SelectListItem
            {
                Text = p.FullName,
                Value = p.CustomerID.ToString()
            });
            ViewData["Products"] = _db.Products.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.ProductID.ToString()
            });

            return View(transaction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit (Transaction model)
        {
            ViewData["Customers"] = _db.Customers.Select(p => new SelectListItem
            {
                Text = p.FullName,
                Value = p.CustomerID.ToString()
            });
            ViewData["Products"] = _db.Products.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.ProductID.ToString()
            });

            Transaction transaction = _db.Transactions.Find(model.TransactionID);
            transaction.CustomerID = model.CustomerID;
            transaction.ProductID = model.ProductID;
            _db.SaveChanges();
            //whenver an error happens, you can do something like this

            if (model.CustomerID != 1)
                {
                ViewData["ErrorMessage"] = "You didn't pick me!";
                return View(model);
                }
            if (model.ProductID > 2)
            {
                ViewData["ErrorMessage"] = "Wrong number";
            }
            return View(model);
        }
    }
}