/*
 * WEB API Controller written by Arun Kumar Narayanan for purpose of OnlineCV project.
 * This api functions are called from jquery ajax funtions of this project.
 * It can extend cross domain if required later based on the project requirment. 
 * Make sure App_start/WebApiConfg.cs is properly configured based on your routing requirments.
 */
 
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using OnlineCV;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace OnlineCV.Controllers
{
    public class CvMastersController : ApiController
    {
        private OnlineCVEntities db = new OnlineCVEntities();

        /*
        * Api function to fetch all CV's , currently no where it is using. 
        * 
        * -------------------------------------------------------------------------------
        * Description       Author      Date           Comments
         * -------------------------------------------------------------------------------
        * Created           Arun        17-Oct-2015    
        * -------------------------------------------------------------------------------
        */
        // GET: api/CvMasters
        public String GetCvMasters()
        {
            var cv = db.CvMasters.ToList();
            String retString;
            try
            {
                retString = JsonConvert.SerializeObject(cv, Formatting.Indented,
                new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    }
                );
                
            }
            catch(Exception ex)
            {
                retString = ex.Message.ToString();
            }
            return retString;
        }
        //

        /*
        * Api function to fetch details of specified CV. 
        * Currently it is called form angularjs as a ajax function to populate the scope. 
        * It is using Newtonsoft.Json for seriazing the model to json.
        * 
        * -------------------------------------------------------------------------------
        * Description       Author      Date           Comments
         * -------------------------------------------------------------------------------
        * Created           Arun        17-Oct-2015    
        * -------------------------------------------------------------------------------
        */
        // GET: api/CvMasters/5
        public String GetCvMasters(String cvName)
        {
            var cv = db.CvMasters.FirstOrDefault(e => e.cv_name == cvName);
            //
            String retString;
            try
            {
                retString = JsonConvert.SerializeObject(cv, Formatting.Indented,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    }
                );
            }
            catch (Exception ex)
            {
                retString = ex.Message.ToString();
            }
            return retString;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /*
        * Api function to check a specified CV exists. 
        * Currently it is not called from anywhere, but i should be called from 
        * onChange of CV Name texbox from Create new CV page. This avoids duplicate cvname.
        * 
        * -------------------------------------------------------------------------------
        * Description       Author      Date           Comments
         * -------------------------------------------------------------------------------
        * Created           Arun        17-Oct-2015    
        * -------------------------------------------------------------------------------
        */
        private bool CvMasterExists(int id)
        {
            return db.CvMasters.Count(e => e.Id == id) > 0;
        }
    }
}