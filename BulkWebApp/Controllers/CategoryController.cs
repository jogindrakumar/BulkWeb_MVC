using BulkWebApp.Data;
using BulkWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkWebApp.Controllers
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
        public IActionResult Create() {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            //if(obj.Name == obj.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("name", "The Display order Cannot exactly match the Name.");
            //}
            if(ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["Success"] = "Category Created Successfully";
                return RedirectToAction("Index");
            }
            return View();
         
            
        }


        //go to edit page with id
        public IActionResult Edit( int? id)
        {
            if(id==null || id == 0)
            {
                return NotFound();
            }
            Category CategoryFromDb = _db.Categories.FirstOrDefault(c => c.Id == id);
           // Category CategoryFromDb = _db.Categories.Find(id);
            if(CategoryFromDb == null)
            {
                return NotFound();
            }
            return View(CategoryFromDb);
        }
        // edit update method
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
         
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["Success"] = "Category Edited Successfully";
                return RedirectToAction("Index");
            }
            return View();


        }

        //go to delete page with id
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //Category CategoryFromDb = _db.Categories.FirstOrDefault(c => c.Id == id);
             Category CategoryFromDb = _db.Categories.Find(id);
            if (CategoryFromDb == null)
            {
                return NotFound();
            }
            return View(CategoryFromDb);
        }
        // Delete method
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? obj = _db.Categories.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["Danger"] = "Category Deleted Successfully";
            return RedirectToAction("Index");


        }
    }
}
