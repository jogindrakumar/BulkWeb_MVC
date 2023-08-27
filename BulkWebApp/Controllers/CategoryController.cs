
using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkWebApp.Controllers
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
                _categoryRepo.Add(obj);
                _categoryRepo.Save();
                TempData["success"] = "Category Created Successfully";
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
            Category CategoryFromDb = _categoryRepo.Get(c => c.Id == id);
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
                _categoryRepo.Update(obj);
                _categoryRepo.Save();
                TempData["success"] = "Category Edited Successfully";
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
             Category CategoryFromDb = _categoryRepo.Get(c => c.Id == id);
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
            Category? obj = _categoryRepo.Get(c => c.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _categoryRepo.Remove(obj);
            _categoryRepo.Save();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");


        }
    }
}
