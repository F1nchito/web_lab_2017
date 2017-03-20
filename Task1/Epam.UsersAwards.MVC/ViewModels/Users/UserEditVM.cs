using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Epam.UsersAwards.MVC.ViewModels.Users
{
    public class UserEditVM : IValidatableObject
    {
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        [Required]
        [StringLength(50,MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
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