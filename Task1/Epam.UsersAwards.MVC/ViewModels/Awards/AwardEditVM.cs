using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Epam.UsersAwards.MVC.ViewModels.Awards
{
    public class AwardEditVM
    {
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        [Required, StringLength(50)]
        [RegularExpression(@"[A-Za-z\d -]+",
            ErrorMessage = "Unacceptable title")]
        public string Title { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        [Required]
        public HttpPostedFileBase Image { get; set; }
    }
}