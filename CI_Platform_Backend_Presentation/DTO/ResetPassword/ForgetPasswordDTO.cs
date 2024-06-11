using System.ComponentModel.DataAnnotations;

namespace CI_Platform_Backend_Presentation.DTO.ForgetPassword;

public class ForgetPasswordDTO
{
    [Required(ErrorMessage = "Email is Required")]
    [EmailAddress(ErrorMessage = "Email is invalid")]
    [MaxLength(128, ErrorMessage = "Email can not be longer than 128 characters")]
    public string Email { get; set; }
}
