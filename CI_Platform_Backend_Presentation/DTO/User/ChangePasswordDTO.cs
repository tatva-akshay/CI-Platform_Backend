using System.ComponentModel.DataAnnotations;

namespace CI_Platform_Backend_Presentation.DTO.User;

public class ChangePasswordDTO
{
    [Required(ErrorMessage = "Old Password is Required")]
    [MinLength(8, ErrorMessage = "Old Password can not be shorter than 8 characters")]
    [MaxLength(255, ErrorMessage = "Old Password can not be longer than 16 characters")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\W).{8,}$", ErrorMessage = "Old Password must contain at least one digit, lowercase letter, uppercase letter, and special character.")]
    public string OldPassword { get; set; }

    [Required(ErrorMessage = "New Password is Required")]
    [MinLength(8, ErrorMessage = "New Password can not be shorter than 8 characters")]
    [MaxLength(255, ErrorMessage = "New Password can not be longer than 16 characters")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\W).{8,}$", ErrorMessage = "New Password must contain at least one digit, lowercase letter, uppercase letter, and special character.")]
    public string NewPassword { get; set; }

    [Required(ErrorMessage = "Confirm Password is Required")]
    [MinLength(8, ErrorMessage = "Confirm Password can not be shorter than 8 characters")]
    [MaxLength(255, ErrorMessage = "Confirm Password can not be longer than 16 characters")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\W).{8,}$", ErrorMessage = "Confirm Password must contain at least one digit, lowercase letter, uppercase letter, and special character.")]
    [Compare("NewPassword", ErrorMessage = "New Password and Confirm Password Should be same")]
    public string ConfirmPassword { get; set; }
}
