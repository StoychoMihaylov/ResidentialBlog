using ResidentialBlog.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ResidentialBlog.Controllers
{
    public class PublicationController : Controller
    {
        // GET: Publication
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        // GET Publication/List
        public ActionResult List()
        {
            using (var database = new BlogDbContext())
            {
                var publications = database
                    .Publications
                    .Include(p => p.Author)
                    .ToList();

                return View(publications);
            }
        }

        //
        //GET Publication/ReadPost
        public ActionResult ReadPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var database = new BlogDbContext())
            {
                var publication = database
                    .Publications
                    .Where(p => p.Id == id)
                    .Include(p => p.Author)
                    .Include(p => p.Comments)
                    .First();

                if (publication == null)
                {
                    return HttpNotFound();
                }

                return View(publication);
            }
        }

        //GET Publication/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        //POST Publication/Create      
        [HttpPost]
        [Authorize]
        public ActionResult Create(Publication publication)
        {
            if (ModelState.IsValid)
            {
                using (var database = new BlogDbContext())
                {
                    var authorId = database
                        .Users
                        .Where(u => u.UserName == this.User.Identity.Name)
                        .First()
                        .Id;
                    publication.AuthorId = authorId;
                    publication.DateTime = DateTime.Now;

                    database.Publications.Add(publication);
                    database.SaveChanges();
                    return RedirectToAction("List");
                }
            }
            return View(publication);
        }

        //GET: Publication/Delete
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var database = new BlogDbContext())
            {
                var publication = database.Publications
                    .Where(p => p.Id == id)
                    .Include(a => a.Author)
                    .Include(p => p.Comments)
                    .First();

                if (publication == null)
                {
                    return HttpNotFound();
                }
                return View(publication);
            }
        }


        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var database = new BlogDbContext())
            {
                var publication = database.Publications
                    .Where(p => p.Id == id)
                    .Include(p => p.Author)
                    .Include(p => p.Comments)
                    .First();

                if (publication == null)
                {
                    return HttpNotFound();
                }

                database.Publications.Remove(publication);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Comment ([Bind(Include ="postID, name, message")] int postID, string name, string message)
        {
            var database = new BlogDbContext();
            Publication publication = database.Publications.Find(postID);
            Comment comment = new Comment();
            comment.postID = postID;
            comment.Email = User.Identity.Name;
            comment.CreatedDate = DateTime.Now;
            comment.Name = User.Identity.Name;
            comment.Body = message;
            comment.Publication = publication;

            database.Comments.Add(comment);
            database.SaveChanges();
            return RedirectToAction("");
        }

        [Authorize]
        public ActionResult DeleteComment (int id)
        {
            using (var database = new BlogDbContext())
            {
                Comment comment = database.Comments.Find(id);
                if(comment ==null)
                {
                    return HttpNotFound();
                }
                database.Comments.Remove(comment);
                database.SaveChanges();

                return RedirectToAction("");
            }
        }
    }
}