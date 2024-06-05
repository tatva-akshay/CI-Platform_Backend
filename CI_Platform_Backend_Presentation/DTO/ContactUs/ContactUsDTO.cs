using System.ComponentModel.DataAnnotations;

namespace CI_Platform_Backend_Presentation.DTO.ContactUs;

public class ContactUsDTO
{
    [Required]
    [MaxLength(255)]
    public string Subject { get; set; }

    [Required]
    [MaxLength(60000)]
    public string Message { get; set; }
}
