using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data;
using System.Linq;
using OnlineShop.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProductTypesController : Controller
    {
        private ApplicationDbContext _db;

        public ProductTypesController(ApplicationDbContext db)
        {
            _db = db;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            //var data = _db.ProductTypes.ToList();
            return View(_db.ProductTypes.ToList());
        }

        //Create Get action method
        public ActionResult Create()
        {
            return View();
        }

        // Create Post Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Create(ProductTypes productTypes)
        {
            if (ModelState.IsValid)
            {
                _db.ProductTypes.Add(productTypes);
                await _db.SaveChangesAsync();
                TempData["save"]="Product type has been saved";
                return RedirectToAction(actionName: nameof(Index));
            }
            return View (productTypes);
        }

        //Create Get Edit action method
        public ActionResult Edit(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            var productType = _db.ProductTypes.Find(id); // for what fing method?
            if(productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        // Create Post Edit Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductTypes productTypes)
        {
            if (ModelState.IsValid)
            {
                _db.Update(productTypes);
                await _db.SaveChangesAsync();
                TempData["save"] = "Product type has been Updated";
                return RedirectToAction(nameof(Index));
            }
            return View(productTypes);
        }


        //Create Get Details action method
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productType = _db.ProductTypes.Find(id); // for what fing method?
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        // Create Post Details Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(ProductTypes productTypes)
        {
                return RedirectToAction(nameof(Index));
        }



        // Create GET Delete Method
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productType = _db.ProductTypes.Find(id); // for what fing method?
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        // Create Post Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, ProductTypes productTypes)
        {
            if(id==null)
            {
                return NotFound();
            }
            if(id!=productTypes.Id)
            {
                return NotFound();
            }
            var productType = _db.ProductTypes.Find(id);

            if(productType == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.ProductTypes.Remove(productType);
                await _db.SaveChangesAsync();
                TempData["delete"] = "Product type has been Deleted";
                return RedirectToAction(actionName: nameof(Index));
            }
            return View(productTypes);
        }
    }
}
