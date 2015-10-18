/*
 * The controller for administration of CV.
 * It holds the model CvMasters
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
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;

namespace OnlineCV.Controllers
{
    public class AdminController : Controller
    {
        private OnlineCVEntities db = new OnlineCVEntities();
         //
         /*
         * Upload profile photo 
         * Called from edit CV page. 
         * -------------------------------------------------------------------------------
         * Description       Author      Date           Comments
          * -------------------------------------------------------------------------------
         * Created           Arun        17-Oct-2015    
         * -------------------------------------------------------------------------------
         */
        public ActionResult UploadPhoto(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // If id is given as parameter continue with searching with Id
            CvMaster cvMaster = db.CvMasters.Find(id);
            if (cvMaster == null)
            {
                // if no CV available with the Id provided throw not found error
                return HttpNotFound();
            }
            //
            HttpPostedFileBase photo = Request.Files["filePhoto"];
            if (photo != null && photo.ContentLength > 0)
            {
                // Image can be save directly to db, but its not a good idea for now.
                //

                //cvMaster.image_type = photo.ContentType;
                //cvMaster.image = new byte[photo.ContentLength];
                //photo.InputStream.Read(cvMaster.image, 0, photo.ContentLength);
                //
                // Save the photos uploaded by user by its id name in the directory mentioned.
                string directory = Server.MapPath("/Uploads/Photos");
                var ext = Path.GetExtension(photo.FileName);
                // Photo is saving with CV Id
                var fileName = cvMaster.Id + ext;
                cvMaster.photo_path = "/Uploads/Photos/" + fileName;
                photo.SaveAs(Path.Combine(directory, fileName));
                //
                if (ModelState.IsValid)
                {
                    db.Entry(cvMaster).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.Information = "Photo uploaded successfully.";
                }
                else
                {
                    ViewBag.Information = "Photo not uploaded successfully. Please try again.";
                }
            }
            return View("Edit",cvMaster);

        }
        /*
        * Default page for Admin
        * Called when /Admin url, which lists all the CV's regardless of any criteria
        * -------------------------------------------------------------------------------
        * Description       Author      Date           Comments
         * -------------------------------------------------------------------------------
        * Created           Arun        17-Oct-2015    
        * -------------------------------------------------------------------------------
        */
        // GET: Admin
        public ActionResult Index()
        {
            // Returns list of all the CV's regardless of any criteria
            return View(db.CvMasters.ToList());
        }
        /*
        * Details page for a specified CV.
        * Called when user clicks details menu of particular CV.
        * -------------------------------------------------------------------------------
        * Description       Author      Date           Comments
         * -------------------------------------------------------------------------------
        * Created           Arun        17-Oct-2015    
        * -------------------------------------------------------------------------------
        */
        // GET: Admin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                // If Id is not supplied throw Bad request error
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CvMaster cvMaster = db.CvMasters.Find(id);
            if (cvMaster == null)
            {
                // If record not found, return not found result.
                return HttpNotFound();
            }
            return View(cvMaster);
        }
        /*
        * CV Creation page
        * 
        * -------------------------------------------------------------------------------
        * Description       Author      Date           Comments
         * -------------------------------------------------------------------------------
        * Created           Arun        17-Oct-2015    
        * -------------------------------------------------------------------------------
        */
        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }
        /*
        * The function which save the CV on submit. 
        * Called when user clicks submit button from CV create new page.
        * -------------------------------------------------------------------------------
        * Description       Author      Date           Comments
         * -------------------------------------------------------------------------------
        * Created           Arun        17-Oct-2015    
        * -------------------------------------------------------------------------------
        */
        // POST: Admin/Create
        [HttpPost] //It will call on HTTP Post request
        [ValidateAntiForgeryToken] // To block CSRF attacks
        public ActionResult Create([Bind(Include = "title,mobile,home_phone,email,image,summary,cv_name,fullname")] CvMaster cvMaster)
        {
            // Checking the Model is valid before saving to DB.
            if (ModelState.IsValid)
            {
                db.CvMasters.Add(cvMaster);
                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    // If any error catches up while saving int DB, trace it for debugging purpose.
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation(
                                  "Class: {0}, Property: {1}, Error: {2}",
                                  validationErrors.Entry.Entity.GetType().FullName,
                                  validationError.PropertyName,
                                  validationError.ErrorMessage);
                        }
                    }
                }
                
                return RedirectToAction("Index");
            }

            return View(cvMaster);
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CvMaster cvMaster = db.CvMasters.Find(id);
            if (cvMaster == null)
            {
                return HttpNotFound();
            }
            return View(cvMaster);
        }
        /*
        * The function which save the CV on submit from Edit page. 
        * Called when user clicks submit button from CV Edit page.
        * -------------------------------------------------------------------------------
        * Description       Author      Date           Comments
         * -------------------------------------------------------------------------------
        * Created           Arun        17-Oct-2015    
        * -------------------------------------------------------------------------------
        */
        // POST: Admin/Edit/5
         [HttpPost]  // It will call on HTTP Post request
        [ValidateAntiForgeryToken] // To protect from CSRF attack
        public ActionResult Edit([Bind(Include = "Id,title,mobile,home_phone,email,image,summary,cv_name,fullname")] CvMaster cvMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cvMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cvMaster);
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CvMaster cvMaster = db.CvMasters.Find(id);
            if (cvMaster == null)
            {
                return HttpNotFound();
            }
            return View(cvMaster);
        }
        /*
        * The function delete the CV master table record. Before deleting make sure that all its
        * child records are deleted.
        * 
        * Called when user clicks delete button from CV delete confirmation page.
        * -------------------------------------------------------------------------------
        * Description       Author      Date           Comments
         * -------------------------------------------------------------------------------
        * Created           Arun        17-Oct-2015    
        * -------------------------------------------------------------------------------
        */
        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CvMaster cvMaster = db.CvMasters.Find(id);
            // Delete the child records before deleting master
            // Please note, this can also done by enabling cascade deletion on entity association.
            foreach(var expertise in cvMaster.Expertises)
            {
                db.Expertises.Remove(expertise);
            }
            foreach (var skill in cvMaster.Skills)
            {
                db.Skills.Remove(skill);
            }
            foreach (var experience in cvMaster.Experiences)
            {
                db.Experiences.Remove(experience);
            }
            db.CvMasters.Remove(cvMaster);
            db.SaveChanges();
            //After deleting return back to home page
            return RedirectToAction("Index");
        }
        //
        //public String GetPhoto(int cvId)
        //{
        //    CvMaster cvMaster = db.CvMasters.FirstOrDefault(p => p.Id == cvId);
        //    //
        //    if (cvMaster != null)
        //    {
        //         //String imgType = (cvMaster.image_type == null ? imgType = "image/jpeg" : cvMaster.image_type.ToString());
        //        return cvMaster.photo_path;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
        //
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
