using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Task2.Models;

namespace Task2.Controllers
{
    public class ImageController : Controller
    {
        private readonly ImageDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ImageController(ImageDbContext context,IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: ImageModels
        public List<ImageModel>/*async Task<IActionResult>*/ Index()
        {
            List<ImageModel> imageList = _context.Images.FromSqlRaw("Select * from Images").ToList();
            return imageList/*View(await _context.Images.ToListAsync())*/;
            //System.Diagnostics.Debug.WriteLine(msg);
        }



        // GET: ImageModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imageModel = await _context.Images
                .FirstOrDefaultAsync(m => m.ImageId == id);
            if (imageModel == null)
            {
                return NotFound();
            }

            return View(imageModel);
        }

        // GET: ImageModels/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public void/*async void*/ SubmitImage(UserModel user )
        {
            System.Diagnostics.Debug.WriteLine(user.Choice);
            UserModel admin = _context.Users.SingleOrDefault(x => x.UserId==1);
            admin.Choice = user.Choice;
            _context.Update(admin);
            _context.SaveChanges();
            /*return RedirectToAction("Index", "Image");*/
        }

        //[Bind("ImageId,Title,ImageFile")]
        // POST: ImageModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public /*async Task<IActionResult>*/ void Create( ImageModel imageModel)
        {
            System.Diagnostics.Debug.WriteLine(imageModel.Title, imageModel.ImageFile);

            if (ModelState.IsValid)
            {
                //save image to wwwroot/image
                //System.Diagnostics.Debug.WriteLine("in");
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = /*imageModel.Title*/ Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
                string extension = Path.GetExtension(imageModel.ImageFile.FileName);
                imageModel.ImageName = fileName = imageModel.Title /*+ DateTime.Now.ToString("yymmssfff")*/ + extension;
                String path = Path.Combine(wwwRootPath + "/Image/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                     imageModel.ImageFile.CopyTo(fileStream);
                }
                _context.Add(imageModel);
                 _context.SaveChanges();
                //return RedirectToAction(nameof(Index));
            }
            //return View(imageModel);
        }

        // GET: ImageModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imageModel = await _context.Images.FindAsync(id);
            if (imageModel == null)
            {
                return NotFound();
            }
            return View(imageModel);
        }

        // POST: ImageModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ImageId,Title,ImageName")] ImageModel imageModel)
        {
            if (id != imageModel.ImageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(imageModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImageModelExists(imageModel.ImageId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(imageModel);
        }

        // GET: ImageModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imageModel = await _context.Images
                .FirstOrDefaultAsync(m => m.ImageId == id);
            if (imageModel == null)
            {
                return NotFound();
            }

            return View(imageModel);
        }

        // POST: ImageModels/Delete/5
        [HttpPost, ActionName("Delete")]
        public /*async Task<IActionResult>*/void DeleteConfirmed(int id)
        {
            System.Diagnostics.Debug.WriteLine(id);
            var imageModel =  _context.Images.Find(id);
            //delete image from wwwroot/image
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "image", imageModel.ImageName);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            //delete the record
            _context.Images.Remove(imageModel);
             _context.SaveChanges();
            //return RedirectToAction(nameof(Index));
        }

        private bool ImageModelExists(int id)
        {
            return _context.Images.Any(e => e.ImageId == id);
        }
    }
}
