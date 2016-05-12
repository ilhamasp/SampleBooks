using ContosoBooks.Models;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoBooks.Controllers
{
    public class AuthorsController : Controller
    {
        [FromServices]
        public AuthorContext AuthorContext { get; set; }

        [FromServices]
        public ILogger<BooksController> Logger { get; set; }

        // GET: Authors
        public IActionResult Index()
        {
            var authors = AuthorContext.Authors;
            return View(authors);
        }

        // GET: Authors Details
        public async Task<ActionResult> Details(int id)
        {
            Author author = await AuthorContext.Authors
                .SingleOrDefaultAsync(b => b.AuthorID == id);
            if (author == null)
            {
                Logger.LogInformation("Details: Item not found {0}", id);
                return HttpNotFound();
            }
            return View(author);
        }

        // GET: Authors Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("FirstMidName", "LastName", "Books")] Author author)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AuthorContext.Authors.Add(author);
                    await AuthorContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DataStoreException)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes.");
            }
            return View(author);
        }

        // GET: Authors Edit
        public async Task<ActionResult> Edit(int id)
        {
            Author author = await FindAuthorAsync(id);
            if (author == null)
            {
                Logger.LogInformation("Edit: Item not found {0}", id);
                return HttpNotFound();
            }

            return View(author);
        }

        // POST: Authors Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("FirstMidName", "LastName", "Books")] Author author)
        {
            try
            {
                author.AuthorID = id;
                AuthorContext.Authors.Attach(author);
                AuthorContext.Entry(author).State = EntityState.Modified;
                await AuthorContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (DataStoreException)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes.");
            }
            return View(author);
        }

        private Task<Author> FindAuthorAsync(int id)
        {
            return AuthorContext.Authors.SingleOrDefaultAsync(author => author.AuthorID == id);
        }

        // GET: Authors Delete
        [HttpGet]
        [ActionName("Delete")]
        public async Task<ActionResult> ConfirmDelete(int id, bool? retry)
        {
            Author author = await FindAuthorAsync(id);
            if (author == null)
            {
                Logger.LogInformation("Delete: Item not found {0}", id);
                return HttpNotFound();
            }
            ViewBag.Retry = retry ?? false;
            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Author author = await FindAuthorAsync(id);
                AuthorContext.Authors.Remove(author);
                await AuthorContext.SaveChangesAsync();
            }
            catch (DataStoreException)
            {
                return RedirectToAction("Delete", new { id = id, retry = true });
            }
            return RedirectToAction("Index");
        }
    }
}
