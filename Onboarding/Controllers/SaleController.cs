using Onboarding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Onboarding.Controllers
{
    public class SaleController : Controller
    {
        private TalnetEntities1 _context;

        public SaleController()
        {
            _context = new TalnetEntities1();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            return View();
        }

        // GET Sales
        public JsonResult GetSale()
        {
            try
            {
                var saleList = _context.Sales.Select(s => new
                {
                    Id = s.Id,
                    DateSold = s.DateSold,
                    CustomerName = s.Customer.Name,
                    ProductName = s.Product.Name,
                    StoreName = s.Store.Name

                }).ToList();
                return new JsonResult { Data = saleList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception e)
            {
                Console.Write(e.Data + "Exception Occured");
                return new JsonResult { Data = "Data Not Found", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public JsonResult GetCustomer()
        {
            try
            {
                var Customerdata = _context.Customers.Select(p => new { Id = p.Id, CustomerName = p.Name }).ToList();

                return new JsonResult { Data = Customerdata, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception e)
            {
                Console.Write(e.Data + "Exception Occured");
                return new JsonResult { Data = "Data Not Found", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public JsonResult GetProduct()
        {
            try
            {
                var ProductsData = _context.Products.Select(p => new { Id = p.Id, ProductName = p.Name }).ToList();

                return new JsonResult { Data = ProductsData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception e)
            {
                Console.Write(e.Data + "Exception Occured");
                return new JsonResult { Data = "Data Not Found", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public JsonResult GetStore()
        {
            try
            {
                var StoresData = _context.Stores.Select(p => new { Id = p.Id, StoreName = p.Name }).ToList();

                return new JsonResult { Data = StoresData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception e)
            {
                Console.Write(e.Data + "Exception Occured");
                return new JsonResult { Data = "Data Not Found", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        // DELETE Sale
        public JsonResult DeleteSale(int id)
        {
            try
            {
                var sale = _context.Sales.Where(s => s.Id == id).SingleOrDefault();
                if (sale != null)
                {
                    _context.Sales.Remove(sale);
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

        // CREATE Sale
        public JsonResult CreateSale(Sale sale)
        {
            try
            {
                _context.Sales.Add(sale);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.Write(e.Data + "Exception Occured");
                return new JsonResult { Data = "Sale Create Failed", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return new JsonResult { Data = "Success", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // UPDATE Sale
        public JsonResult GetUpdateSale(int id)
        {
            try
            {
                Sale sale = _context.Sales.Where(s => s.Id == id).SingleOrDefault();
                return new JsonResult { Data = sale, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception e)
            {
                Console.Write(e.Data + "Exception Occured");
                return new JsonResult { Data = "Sale Not Found", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public JsonResult UpdateSale(Sale sale)
        {
            try
            {
                Sale sa = _context.Sales.Where(s => s.Id == sale.Id).SingleOrDefault();
                sa.CustomerID = sale.CustomerID;
                sa.ProductId = sale.ProductId;
                sa.StoreId = sale.StoreId;
                sa.DateSold = sale.DateSold;

                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.Write(e.Data + "Exception Occured");
                return new JsonResult { Data = "Sale Update Failed", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return new JsonResult { Data = "Success", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}