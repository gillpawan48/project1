using Onboarding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Onboarding.Controllers
{
    public class CustomerController : Controller
    {
        private TalnetEntities1 _context;

        public CustomerController()
        {
            _context = new TalnetEntities1();
        }

        //protected override void.Dispose(bool disposing)
        //{
        //    _context.Dispose();
        //}

        public ActionResult Index()
        {
            return View();
        }

        // GET Customers
        public JsonResult GetCustomer()
        {
            try
            {
                var customerList = _context.Customers.ToList();
                return new JsonResult { Data = customerList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception e)
            {
                Console.Write(e.Data + "Exception Occured");
                return new JsonResult { Data = "Data Not Found", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        // DELETE Customer
        public JsonResult DeleteCustomer(int id)
        {
            try
            {
                var customer = _context.Customers.Where(c => c.Id == id).SingleOrDefault();
                if (customer != null)
                {
                    _context.Customers.Remove(customer);
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Data + "Exception Occured");
                return new JsonResult { Data = "Deletion Falied", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return new JsonResult { Data = "Success", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // CREATE Customer
        [HttpPost]
        public JsonResult CreateCustomer(Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Customer newCus = new Customer
                    //{
                    //    Name = customer.Name,
                    //    Address = customer.Address,
                    //};
                    _context.Customers.Add(customer);
                    _context.SaveChanges();
                    
                }
                
            }
            catch (Exception e)
            {
                Console.Write(e.Data + "Exception Occured");
                return Json ( new { Data = "Customer Create Failed", JsonRequestBehavior = JsonRequestBehavior.AllowGet });
            }
            return Json(new { Data = "Success", JsonRequestBehavior = JsonRequestBehavior.AllowGet });
        }

        // UPDATE Customer
        public JsonResult GetUpdateCustomer(int id)
        {
            try
            {
                Customer customer = _context.Customers.Where(c => c.Id == id).SingleOrDefault();
                return new JsonResult { Data = customer, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception e)
            {
                Console.Write(e.Data + "Exception Occured");
                return new JsonResult { Data = "Customer Not Found", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public JsonResult UpdateCustomer(Customer customer)
        {
            try
            {
                Customer cust = _context.Customers.Where(c => c.Id == customer.Id).SingleOrDefault();
                cust.Name = customer.Name;
                cust.Address = customer.Address;
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.Write(e.Data + "Exception Occured");
                return new JsonResult { Data = "Customer Update Failed", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return new JsonResult { Data = "Success", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}

