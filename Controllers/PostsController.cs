using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using alkemy_blog_challenge.Database;
using alkemy_blog_challenge.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace alkemy_blog_challenge.Controllers
{
    public class PostsController : Controller
    {
        private readonly BlogDbContext _context;

        public PostsController(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var posts = _context.Post.Where(p => p.SoftDeleted == false).OrderByDescending(p => p.FechaCreacion).ToListAsync();
            return View(await posts);
        }

        public ActionResult RetrieveImage(Guid id)
        {
            byte[] cover = GetImageFromDataBase(id);
            if (cover != null)
            {
                return File(cover, "image/jpg");
            }
            else
            {
                return null;
            }
        }
        public byte[] GetImageFromDataBase(Guid Id)
        {
            var q = from temp in _context.Post where temp.Id == Id select temp.Imagen;
            byte[] cover = q.First();
            return cover;
        }


        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Post post)
        {

            IFormFile file = Request.Form.Files["ImageData"];
                if (ModelState.IsValid && file != null)
                {
                    post.Id = Guid.NewGuid();
                    post.FechaCreacion = DateTime.Now;
                    post.Imagen = ConvertToBytes(file);
                    _context.Add(post);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

            ModelState.AddModelError("Imagen", "Tiene que elegir una image.");
            return View(post);
        }


        public byte[] ConvertToBytes(IFormFile image)
        {
            byte[] CoverImageBytes = null;
            BinaryReader reader = new BinaryReader(image.OpenReadStream());
            CoverImageBytes = reader.ReadBytes((int)image.Length);
            return CoverImageBytes;
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }


            IFormFile file = Request.Form.Files["ImageData"];
            
            if (ModelState.IsValid && file!=null)
            {
                try
                {

                    post.Imagen = ConvertToBytes(file);
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
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
            ModelState.AddModelError("Imagen", "Tiene que elegir una imagen.");
            return View(post);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var post = _context.Post.Find(id);
            _context.Post.Remove(post);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(Guid id)
        {
            return _context.Post.Any(e => e.Id == id);
        }
    }
}
