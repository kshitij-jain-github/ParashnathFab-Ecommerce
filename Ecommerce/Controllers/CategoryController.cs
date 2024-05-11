using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList =_db.Categories.ToList();
            return View(objCategoryList);
        }

		public IActionResult Create()
		{
			return View();
		}
        [HttpPost]
        public IActionResult Create(Category obj)
		{
			if(ModelState.IsValid)
			{
				_db.Categories.Add(obj);
				_db.SaveChanges();
				return RedirectToAction("index", "category");

			}
			return View();
		}

		public IActionResult Edit(int? id)
		{
			if(id == null||id==0) {
				return NotFound();

			}
			Category? categoryFromdb = _db.Categories.Find(id);
			if(categoryFromdb == null) {
				return NotFound();
			}

			return View(categoryFromdb);
		}
		[HttpPost]
		public IActionResult Edit(Category obj)
		{

			if (ModelState.IsValid)
			{
				_db.Categories.Update(obj);
				_db.SaveChanges();
				return RedirectToAction("index", "category");

			}
			return View();
		}


		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			var categoryFromDb = _db.Categories.Find(id);

			if (categoryFromDb == null)
			{
				return NotFound();
			}

			return View(categoryFromDb);
		}

		//POST
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeletePOST(int? id)
		{
			var obj = _db.Categories.Find(id);
			if (obj == null)
			{
				return NotFound();
			}

			_db.Categories.Remove(obj);
			_db.SaveChanges();
 			return RedirectToAction("Index");

		}
	}
}
