using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineShop.Data;
using OnlineShop.Models;
using OnlineShop.Models.ViewModels;
using OnlineShop.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace OnlineShop.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private ApplicationDbContext _db;
        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(int? page)
        {
            return View(_db.Products.Include(c=>c.ProductTypes).Include(c=>c.SpecialTag).ToList().ToPagedList(pageNumber:page??1, pageSize:9));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //GET product detail acation method
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productInBase = _db.Products.Include(c => c.ProductTypes).FirstOrDefault(c => c.Id == id);
            if (productInBase == null)
            {
                return NotFound();
            }
            ProductsCount productsCount = new ProductsCount() {Product=productInBase, Count=0};
            return View(productsCount);
        }

        //Post product details method
        [HttpPost]
        [ActionName("Details")]
        public ActionResult ProductDetails(ProductsCount productsCount)
        {
            List<Products> products = new List<Products>();
            var product = _db.Products.Include(c => c.ProductTypes).FirstOrDefault(c => c.Id == productsCount.Product.Id);
            if (product == null)
            {
                return NotFound();
            }
            if (!product.IsAvailable)
            {
                ViewBag.message = "This product is not available!";
                return View(new ProductsCount() { Product = product, Count = productsCount.Count});
            }
            products = HttpContext.Session.Get<List<Products>>("products");
            if (products==null)
            {
                products = new List<Products>();
            }
            for(int i=0; i< productsCount.Count; i++)
            {
                products.Add(product);
            }
            HttpContext.Session.Set("products", products);
            return RedirectToAction(nameof(Index));
        }

        // Get Remove method
        [ActionName("Remove")]
        public IActionResult RemoveToCart(int? id)
        {
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.Id == id);
                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);

                }
            }
            return RedirectToAction(nameof(Cart));
        }


        [HttpPost]
        public IActionResult Remove(int? id)
        {
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.Id == id);
                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);

                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult RemoveAll()
        {
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if (products != null)
            {
                //products.Remove(product);
                products = new List<Products>();
                HttpContext.Session.Set("products", products);
            }
            return RedirectToAction(nameof(Cart));
        }

        //GET product Cart Action method

        public IActionResult Cart()
        {
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if (products == null)
            {
                products = new List<Products>();
            }
            return View(products);
        }
    }
}
