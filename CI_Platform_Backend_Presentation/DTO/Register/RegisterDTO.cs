using System.ComponentModel.DataAnnotations;

namespace CI_Platform_Backend_Presentation.DTO.Register;


// 4 June - Dhruvil Bhojani
// This Model will be used to get Data to Register the User at AuthController@Register
public class RegisterDTO
{
    [Required(ErrorMessage = "First name is Required")]
    [MinLength(2, ErrorMessage = "First Name can not be shorter than 2 characters")]
    [MaxLength(16, ErrorMessage = "First name can not be longer than 16 characters")]
    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only Letters Allowed in First Name")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last name is Required")]
    [MinLength(2, ErrorMessage = "Last Name can not be shorter than 2 characters")]
    [MaxLength(16, ErrorMessage = "Last name can not be longer than 16 characters")]
    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only Letters Allowed in Last Name")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Email is Required")]
    [EmailAddress(ErrorMessage = "Email is invalid")]
    [MaxLength(128, ErrorMessage = "Email can not be longer than 128 characters")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Phone Number is Required")]
    [RegularExpression(@"^[2-9]{2}[0-9]{8}$", ErrorMessage ="Phone Number is invalid")]
    public long PhoneNumber { get; set; }

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
