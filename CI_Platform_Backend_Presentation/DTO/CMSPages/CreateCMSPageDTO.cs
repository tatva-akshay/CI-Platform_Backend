using System.ComponentModel.DataAnnotations;

namespace CI_Platform_Backend_Presentation.DTO.CMSPages;

public class CreateCMSPageDTO
{
    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    public string Title { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(2048)]
    public string Description { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(1000)]
    public string Slug { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(1000)]
    public bool IsActive { get; set; }
}
