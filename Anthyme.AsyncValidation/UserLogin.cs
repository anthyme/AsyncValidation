using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anthyme.AsyncValidation
{
    public class UserInput : ValidatableModel
    {
        private string _userName;
        private string _email;
        private string _repeatEmail;

        [Required]
        [StringLength(20)]
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; RaisePropertyChanged("UserName"); }
        }

        [Required]
        [EmailAddress]
        [StringLength(60)]
        public string Email
        {
            get { return _email; }
            set { _email = value; RaisePropertyChanged("Email"); }
        }

        [Required]
        [EmailAddress]
        [StringLength(60)]
        [CustomValidation(typeof(UserInput), "SameEmailValidate")]
        public string RepeatEmail
        {
            get { return _repeatEmail; }
            set { _repeatEmail = value; RaisePropertyChanged("RepeatEmail"); }
        }

        public static ValidationResult SameEmailValidate(object obj, ValidationContext context)
        {
            var user = (UserInput)context.ObjectInstance;
            if (user.Email != user.RepeatEmail)
            {
                return new ValidationResult("The emails are not equal", new List<string> { "Email", "RepeatEmail" });
            }
            return ValidationResult.Success;
        }
    }
}
