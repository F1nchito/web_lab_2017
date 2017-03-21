using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace Epam.UsersAwards.MVC.ViewModels.Awards
{
    public class AwardCreateVM /*: IValidatableObject*/
    {
        [Required, StringLength(50)]
        [RegularExpression(@"[\w\d -]+", 
            ErrorMessage = "Unacceptable title")]
        public string Title { get; set; }

        [StringLength(50)]
        public string Description { get; set; }
        public HttpPostedFileBase Image { get; set; }
        //    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //    {
        //        throw new NotImplementedException();
        //    }
    }
}