using System.ComponentModel.DataAnnotations;

namespace CI_Platform_Backend_Presentation.DTO.ResetPassword;

public class ResetPasswordDTO
{
    [Required(ErrorMessage = "Token is Required")]
    public string Token { get; set; }

    [Required(ErrorMessage = "Password is Required")]
    [MinLength(8, ErrorMessage = "Password can not be shorter than 8 characters")]
    [MaxLength(255, ErrorMessage = "Password can not be longer than 16 characters")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\W).{8,}$", ErrorMessage = "Password must contain at least one digit, lowercase letter, uppercase letter, and special character.")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Confirm Password is Required")]
    [MinLength(8, ErrorMessage = "Confirm Password can not be shorter than 8 characters")]
    [MaxLength(255, ErrorMessage = "Confirm Password can not be longer than 16 characters")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\W).{8,}$", ErrorMessage = "Confirm Password must contain at least one digit, lowercase letter, uppercase letter, and special character.")]
    [Compare("Password", ErrorMessage = "Password and Confirm Password Should be same")]
    public string ConfirmPassword { get; set; }
}
