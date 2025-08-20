using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryController(ICategoryRepository db)
        {
            _categoryRepo = db;
        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = _categoryRepo.GetAll().ToList();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name.ToLower() == obj.DisplayOrder.ToString())
                ModelState.AddModelError("Name", "Tên danh mục không được trùng với thứ tự hiển thị");
            if (obj.Name.ToLower().Equals("test"))
                ModelState.AddModelError("", "giá trị test không hợp lệ");

            if (!ModelState.IsValid)
                return View(obj);

            _categoryRepo.Add(obj);
            _categoryRepo.Save();
            TempData["Success"] = "Category created successfully";
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _categoryRepo.Get(u => u.Id == id);
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);
            //Category? categoryFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name.ToLower() == obj.DisplayOrder.ToString())
                ModelState.AddModelError("Name", "Tên danh mục không được trùng với thứ tự hiển thị");
            if (obj.Name.ToLower().Equals("test"))
                ModelState.AddModelError("", "giá trị test không hợp lệ");

            if (!ModelState.IsValid)
                return View(obj);

            _categoryRepo.Update(obj);
            _categoryRepo.Save();
            TempData["Success"] = "Category update successfully";
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _categoryRepo.Get(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _categoryRepo.Get(u => u.Id == id);

            if (obj == null)
                return NotFound();

            _categoryRepo.Remove(obj);
            _categoryRepo.Save();
            TempData["Success"] = "Category delete successfully";
            return RedirectToAction("Index");
        }
    }
}
