﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using SmartMethodStore;

namespace SmartMethodStore
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterOpenAuth();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

            //Records all errors to database
            using (StoreDataContext data = new StoreDataContext()) 
            {
                Exception exceptionToLog = Server.GetLastError();
                Error newError = new Error();
                newError.ErrorMessage = exceptionToLog.Message;
                newError.ErrorStackTrace = exceptionToLog.StackTrace;
                newError.ErrorURL = Request.Url.ToString();
                newError.ErrorDate = DateTime.Now;
                data.Errors.InsertOnSubmit(newError);
                data.SubmitChanges();

            }
            Response.Redirect("~/error.aspx");       //Redirects to an error.aspx page if any error occurs with user's request 

        }
    }
}
