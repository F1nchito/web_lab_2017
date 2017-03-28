using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace Epam.UsersAwards.MVCattr.ViewModels
{
    public class UserCreateVM : IValidatableObject
    {
        [Required]
        [StringLength(50, MinimumLength = 1)]
        [RegularExpression(@"[A-Za-z -]\w*",
            ErrorMessage = "Unacceptable name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        public DateTime DOB { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DOB > DateTime.Now || DOB < DateTime.Now.AddYears(-150))
            {
                yield return new ValidationResult($"Unacceptable date of birth", new[] { nameof(DOB) });
            }
        }
        public HttpPostedFileBase Photo { get; set; }
    }
}