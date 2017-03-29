using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Epam.UsersAwards.MVCattr.ViewModels.Users
{
    public class UserEditVM : IValidatableObject
    {
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        [Required]
        [StringLength(50,MinimumLength = 1)]
        [RegularExpression(@"[A-Za-z -]\w*",
            ErrorMessage = "Unacceptable name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString ="{0:d}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }

        public HttpPostedFileBase Photo { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DOB > DateTime.Now || DOB < DateTime.Now.AddYears(-150))
            {
                yield return new ValidationResult($"Unacceptable date of birth", new[] { nameof(DOB) });
            }
        }
    }
}