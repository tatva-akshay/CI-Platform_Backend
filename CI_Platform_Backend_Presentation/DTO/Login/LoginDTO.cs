using System.ComponentModel.DataAnnotations;

namespace CI_Platform_Backend_Presentation.DTO.Login;

// 4 June - Dhruvil Bhojani
// This Model will be used to get Login Data in AuthController@Login
public class LoginDTO
{
    
    [Required(ErrorMessage = "Email is Required")]
    [EmailAddress(ErrorMessage = "Email is invalid")]
    [MaxLength(128, ErrorMessage = "Email can not be longer than 128 characters")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is Required")]
    [MinLength(8, ErrorMessage = "Password can not be shorter than 8 characters")]
    [MaxLength(255, ErrorMessage = "Password can not be longer than 16 characters")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\W).{8,}$", ErrorMessage = "Password must contain at least one digit, lowercase letter, uppercase letter, and special character.")]
    public string Password { get; set; }
}
