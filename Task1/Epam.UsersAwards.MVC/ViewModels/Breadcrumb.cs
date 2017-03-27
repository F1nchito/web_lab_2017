using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epam.UsersAwards.MVC.ViewModels
{
    public class Breadcrumb
    {
        public string Controller { get; set; }

        public string Action { get; set; }

        public string Title { get; set; }

        public Breadcrumb(string controller, string action, string title)
        {
            Controller = controller;
            Action = action;
            Title = title;
        }
    }
}