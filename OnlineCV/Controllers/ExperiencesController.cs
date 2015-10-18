/*
 * Controller class for Create/Edit/Delete experiences associate with CV
 */
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
    public class ExperiencesController : Controller
    {
        private OnlineCVEntities db = new OnlineCVEntities();
        public int cvId;
        // GET: Experiences
        public ActionResult Index(String CVId)
        {
            var experiences = db.Experiences.Include(e => e.CvMaster);
            List<Experience> colExperiences;
            //
            if (!String.IsNullOrEmpty(CVId))
            {
                cvId = int.Parse(CVId);
                colExperiences = experiences.Where(t => t.cvId == cvId).ToList();
 
            }
            else
            {
                colExperiences = experiences.ToList();
            }
            
            return View(colExperiences);
        }

        // GET: Experiences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Experience experience = db.Experiences.Find(id);
            if (experience == null)
            {
                return HttpNotFound();
            }
            return View(experience);
        }

        // GET: Experiences/Create
        public ActionResult Create(String CVId)
        {
            ViewBag.cvId = CVId;
            //ViewBag.cvId =  new SelectList(db.CvMasters, "Id", "title");
            return View();
        }

        // POST: Experiences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cvId,company_name,title,period,description")] Experience experience)
        {
            if (ModelState.IsValid)
            {
                
                db.Experiences.Add(experience);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cvId = new SelectList(db.CvMasters, "Id", "title", experience.cvId);
            return View(experience);
        }

        // GET: Experiences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Experience experience = db.Experiences.Find(id);
            if (experience == null)
            {
                return HttpNotFound();
            }
            ViewBag.cvId = new SelectList(db.CvMasters, "Id", "title", experience.cvId);
            return View(experience);
        }

        // POST: Experiences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,cvId,company_name,title,period,description")] Experience experience)
        {
            if (ModelState.IsValid)
            {
                db.Entry(experience).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cvId = new SelectList(db.CvMasters, "Id", "title", experience.cvId);
            return View(experience);
        }

        // GET: Experiences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Experience experience = db.Experiences.Find(id);
            if (experience == null)
            {
                return HttpNotFound();
            }
            return View(experience);
        }

        // POST: Experiences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {


            Experience experience = db.Experiences.Find(id);
            db.Experiences.Remove(experience);
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
