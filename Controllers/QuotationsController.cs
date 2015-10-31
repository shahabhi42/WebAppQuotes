using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuotationsApp.Models;

namespace QuotationsApp.Controllers
{
    public class QuotationsController : Controller
    {
        private QuotationsAppCSISDBContext db = new QuotationsAppCSISDBContext();

       
        public ActionResult Index(string id)
        {
            List<string> listOfAuthors = new List<string>();
            listOfAuthors = db.Quotations.Select(x => x.Author).ToList();
            listOfAuthors = listOfAuthors.Distinct().ToList();
            ViewBag.Authors = new SelectList(listOfAuthors);

            List<string> listOfCategoryNames = new List<string>();
            listOfCategoryNames = db.Quotations.Select(x => x.Category.Name).ToList();
            listOfCategoryNames = listOfCategoryNames.Distinct().ToList();
            ViewBag.Categories = new SelectList(listOfCategoryNames);

            List<string> listOfQuotes = new List<string>();
            listOfQuotes = db.Quotations.Select(x => x.Quote).ToList();
            listOfQuotes = listOfQuotes.Distinct().ToList();
            ViewBag.Quotes = new SelectList(listOfQuotes);

            IQueryable<Quotation> Quotations = db.Quotations.AsQueryable();
            IQueryable<Quotation> filteredQuotations = Quotations.Where(x => x.Quote.Contains(id));

            IQueryable<Quotation> Authors = db.Quotations.AsQueryable();
            IQueryable<Quotation> filteredAuthors = Authors.Where(x => x.Author.Contains(id));

            IQueryable<Quotation> Categories = db.Quotations.AsQueryable();
            IQueryable<Quotation> filteredCategories = Categories.Where(x => x.Category.Name.Contains(id));

            IQueryable<Quotation> final;

            if (id != null && id.Length != 0)
            {
                
                final = filteredQuotations.Concat(filteredAuthors);
                final = final.Concat(filteredCategories);
                final = final.Distinct();


                return View(final);
            }

            var quotations = db.Quotations.Include(q => q.Category);
            return View(quotations.ToList());
        }
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quotation quotation = db.Quotations.Find(id);
            if (quotation == null)
            {
                return HttpNotFound();
            }
            return View(quotation);
        }

   
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QuotationID,Quote,Author,Date,CategoryID")] Quotation quotation, string NewCategory)
        {

            bool flag = false;
            foreach (var item in db.Categories)
            {

                if (item.Name.ToString() != NewCategory && flag == false && NewCategory != "")
                {
                    db.Categories.Add(new Category { Name = NewCategory });
                    flag = true;
                }
            }
            if (ModelState.IsValid)
            {
                if (quotation.Date == default(DateTime))
                {
                    quotation.Date = DateTime.Now;
                }

                db.Quotations.Add(quotation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", quotation.CategoryID);
            return View(quotation);
        }

   
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quotation quotation = db.Quotations.Find(id);
            if (quotation == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", quotation.CategoryID);
            return View(quotation);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QuotationID,Quote,Author,Date,CategoryID")] Quotation quotation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quotation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", quotation.CategoryID);
            return View(quotation);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quotation quotation = db.Quotations.Find(id);
            if (quotation == null)
            {
                return HttpNotFound();
            }
            return View(quotation);
        }

  
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Quotation quotation = db.Quotations.Find(id);
            db.Quotations.Remove(quotation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
