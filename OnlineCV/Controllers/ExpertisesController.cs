using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineCV;

namespace OnlineCV.Controllers
{
    public class ExpertisesController : Controller
    {
        private OnlineCVEntities db = new OnlineCVEntities();
        public int cvId;
        // GET: Expertises
        public ActionResult Index(String CVId)
        {
           //
            var expertises = db.Expertises.Include(e => e.CvMaster);
            List<Expertise> colExpertises;
            //
            if(!String.IsNullOrEmpty(CVId))
            {
                cvId = int.Parse(CVId);
                colExpertises = expertises.Where(t => t.cvId == cvId).ToList();
               
            }
            else
            {
                colExpertises = expertises.ToList();
            }
            return View(colExpertises);
           
        }

        // GET: Expertises/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expertise expertise = db.Expertises.Find(id);
            if (expertise == null)
            {
                return HttpNotFound();
            }
            return View(expertise);
        }

        // GET: Expertises/Create
        public ActionResult Create(String CVId)
        {
            ViewBag.cvId = CVId; //new SelectList(db.CvMasters, "Id", "title");
            return View();
        }

        // POST: Expertises/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,cvId,description")] Expertise expertise)
        {
            if (ModelState.IsValid)
            {
                db.Expertises.Add(expertise);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cvId = new SelectList(db.CvMasters, "Id", "title", expertise.cvId);
            return View();
        }

        // GET: Expertises/Edit/5
        [NoDirectAccess]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expertise expertise = db.Expertises.Find(id);
            if (expertise == null)
            {
                return HttpNotFound();
            }
            ViewBag.cvId = new SelectList(db.CvMasters, "Id", "title", expertise.cvId);
            return View(expertise);
        }

        // POST: Expertises/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,cvId,description")] Expertise expertise)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expertise).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cvId = new SelectList(db.CvMasters, "Id", "title", expertise.cvId);
            return View(expertise);
        }

        // GET: Expertises/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expertise expertise = db.Expertises.Find(id);
            if (expertise == null)
            {
                return HttpNotFound();
            }
            return View(expertise);
        }

        // POST: Expertises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Expertise expertise = db.Expertises.Find(id);
            db.Expertises.Remove(expertise);
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
