using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data;
using System.Linq;
using OnlineShop.Models;
using System.Threading.Tasks;
namespace OnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SpecialTagsController : Controller
    {
            private ApplicationDbContext _db;

            public SpecialTagsController(ApplicationDbContext db)
            {
                _db = db;
            }
            public IActionResult Index()
            {
                //var data = _db.ProductTypes.ToList();
                return View(_db.SpecialTags.ToList());
            }

            //Create Get action method
            public ActionResult Create()
            {
                return View();
            }

            // Create Post Action Method
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(SpecialTags specialTags)
            {
                if (ModelState.IsValid)
                {
                    _db.SpecialTags.Add(specialTags);
                    await _db.SaveChangesAsync();
                    return RedirectToAction(actionName: nameof(Index));
                }
                return View(specialTags);
            }

            //Create Get Edit action method
            public ActionResult Edit(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }
                var specialTag = _db.SpecialTags.Find(id); // for what fing method?
                if (specialTag == null)
                {
                    return NotFound();
                }
                return View(specialTag);
            }

            // Create Post Edit Action Method
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(SpecialTags specialTags)
            {
                if (ModelState.IsValid)
                {
                    _db.Update(specialTags);
                    await _db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(specialTags);
            }


            //Create Get Details action method
            public ActionResult Details(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }
                var specialTag = _db.SpecialTags.Find(id); // for what fing method?
                if (specialTag == null)
                {
                    return NotFound();
                }
                return View(specialTag);
            }

            // Create Post Details Action Method
            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Details(SpecialTags specialTags)
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
                var specialTag = _db.SpecialTags.Find(id); // for what fing method?
                if (specialTag == null)
                {
                    return NotFound();
                }
                return View(specialTag);
            }

            // Create Post Action Method
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Delete(int? id, SpecialTags specialTags)
            {
                if (id == null)
                {
                    return NotFound();
                }
                if (id != specialTags.Id)
                {
                    return NotFound();
                }
                var specialTag = _db.SpecialTags.Find(id);

                if (specialTag == null)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    _db.SpecialTags.Remove(specialTag);
                    await _db.SaveChangesAsync();
                    return RedirectToAction(actionName: nameof(Index));
                }
                return View(specialTags);
            }
        }
    }
