using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
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
            List<Category> objCategoryList = _db.Categories.ToList();
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
           
            _db.Categories.Add(obj);
            _db.SaveChanges();
            TempData["Success"] = "Category created successfully";
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id);
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
           
            _db.Categories.Update(obj);
            _db.SaveChanges();
            TempData["Success"] = "Category update successfully";
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _db.Categories.Find(id);

            if (obj == null)
                return NotFound();
           
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["Success"] = "Category delete successfully";
            return RedirectToAction("Index");
        }
    }
}
