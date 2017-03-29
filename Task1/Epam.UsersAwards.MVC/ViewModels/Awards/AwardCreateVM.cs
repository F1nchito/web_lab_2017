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
        [RegularExpression(@"[A-Za-z -]+", 
            ErrorMessage = "Unacceptable title")]
        public string Title { get; set; }

        [StringLength(50)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        public HttpPostedFileBase Image { get; set; }
        //    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //    {
        //        throw new NotImplementedException();
        //    }
    }
}