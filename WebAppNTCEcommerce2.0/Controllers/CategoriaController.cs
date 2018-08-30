using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppNTCEcommerce2._0.Models;

namespace WebAppNTCEcommerce2._0.Controllers
{
    public class CategoriaController : Controller
    {
        private Entities1 db = new Entities1();

        // GET: Categoria
        public ActionResult Index()
        {
            return View();
        }
          public ActionResult PartialMenuCategoria()
        {
            Entities1 db = new Entities1();
            var lista = db.Categoria.OrderBy(m => m.nomeCategoria).ToList();
            db.Dispose();
            return PartialView(lista);
        }
      
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}