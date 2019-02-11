using Onboarding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Onboarding.Controllers
{
    public class StoreController : Controller
    {
        private TalnetEntities1 _context = new TalnetEntities1();

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            return View();
        }

        // GET Stores
        public JsonResult GetStore()
        {
            try
            {
                var storeList = _context.Stores.ToList();
                return new JsonResult { Data = storeList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception e)
            {
                Console.Write(e.Data + "Exception Occured");
                return new JsonResult { Data = "Data Not Found", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        // DELETE Store
        public JsonResult DeleteStore(int id)
        {
            try
            {
                var store = _context.Stores.Where(s => s.Id == id).SingleOrDefault();
                if (store != null)
                {
                    _context.Stores.Remove(store);
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

        // CREATE Store
        public JsonResult CreateStore(Store store)
        {
            try
            {
                _context.Stores.Add(store);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.Write(e.Data + "Exception Occured");
                return new JsonResult { Data = "Store Create Failed", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return new JsonResult { Data = "Success", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // UPDATE Store
        public JsonResult GetUpdateStore(int id)
        {
            try
            {
                Store store = _context.Stores.Where(s => s.Id == id).SingleOrDefault();
                return new JsonResult { Data = store, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception e)
            {
                Console.Write(e.Data + "Exception Occured");
                return new JsonResult { Data = "Store Not Found", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public JsonResult UpdateStore(Store store)
        {
            try
            {
                Store st = _context.Stores.Where(s => s.Id == store.Id).SingleOrDefault();
                st.Name = store.Name;
                st.Address = store.Address;
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.Write(e.Data + "Exception Occured");
                return new JsonResult { Data = "Store Update Failed", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return new JsonResult { Data = "Success", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}