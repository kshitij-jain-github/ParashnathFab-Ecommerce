using Ecommerce.Data;
using Ecommerce.DataAccess.Repository.IRepository;
using Ecommerce.Model;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork db)
        {
            _unitOfWork = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
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
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
				TempData["success"] = "Category created successfully";

				return RedirectToAction("index", "category");

			}
			return View();
		}

		public IActionResult Edit(int? id)
		{
			if(id == null||id==0) {
				return NotFound();

			}
			Category? categoryFromdb = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (categoryFromdb == null) {
				return NotFound();
			}

			return View(categoryFromdb);
		}
		[HttpPost]
		public IActionResult Edit(Category obj)
		{

			if (ModelState.IsValid)
			{
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
				TempData["success"] = "Category updates successfully";

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
			var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);

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
			var obj = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
			{
				return NotFound();
			}

            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
			TempData["success"] = "Category deleted successfully";
			return RedirectToAction("Index");

		}
	}
}
