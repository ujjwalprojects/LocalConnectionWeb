using System.ComponentModel.DataAnnotations;

namespace LocalConnWeb.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
    public class LoginModel
    {
        [Required(ErrorMessage = "Enter Registered Email.")]
        [Display(Name = "Email/Mobile No.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Enter Password")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
    public class LoginErrorModel
    {
        public string error { get; set; }
        public string error_description { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
    public class RegisterModel
    {
        [Required(ErrorMessage = "Enter User Email ID")]
        [EmailAddress(ErrorMessage = "Enter Valid Email ID")]
        public string Email { get; set; }
        public string MobileNo { get; set; }
        [Required(ErrorMessage = "Enter User Profile Name")]
        public string ProfileName { get; set; }
        [Required(ErrorMessage = "Select Role")]
        public string Password { get; set; }

    }
    public class TokenModel
    {
        public string access_token { get; set; }
        public string userId { get; set; }
        public string profileName { get; set; }
        public string expires_in { get; set; }
        public string role { get; set; }
        public string userImage { get; set; }
        public string email { get; set; }
    }
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Enter Registered Email")]
        [EmailAddress(ErrorMessage = "Enter Valid Email Address")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Enter Registered Email")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter New Password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        //[RegularExpression(@"^(?=.*[A-Z].*[A-Z])(?=.*[!@#$&*])(?=.*[0-9].*[0-9])(?=.*[a-z].*[a-z].*[a-z]).{8}$")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        //[RegularExpression(@"^(?=.*[A-Z].*[A-Z])(?=.*[!@#$&*])(?=.*[0-9].*[0-9])(?=.*[a-z].*[a-z].*[a-z]).{8}$")]
        public string ConfirmPassword { get; set; }
        public string Code { get; set; }
    }
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Enter Your Current Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Enter New Password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
    public class ChangePasswordModel
    {
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }

    //following app fossword reset cycles
    public class ForgotPasswordModel
    {
        [Required(ErrorMessage ="Enter Mobile No")]
        public string MobileNo { get; set; }
        [Required(ErrorMessage ="Enter New Password")]
        public string Password { get; set; }
    }

    public class OTPModel
    {
        public string MobileNo { get; set; }
        public string Type { get; set; }
        public string OTP { get; set; }
    }
}
