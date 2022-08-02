using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Ninject;

namespace CBTour.Models
{
    public class SessionAccessor : ISessionAccessor
    {
        public void SetSessionAccessor(UserVM user)
        {
            HttpContext.Current.Session["Role"] = user.Role;
            HttpContext.Current.Session["UserId"] = user.ID;
            HttpContext.Current.Session["Email"] = user.Email;
        }

        public void Clear()
        {
            //Removes all current keys
            System.Web.HttpContext.Current.Session.Clear();
            //Cancel the current Session
            System.Web.HttpContext.Current.Session.Abandon();
        }
    }
}