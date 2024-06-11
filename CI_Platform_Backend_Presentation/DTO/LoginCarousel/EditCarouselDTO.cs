using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CI_Platform_Backend_Presentation.DTO.Carousel;

public class EditCarouselDTO
{
    [Required]
    public IFormFile Image { get; set; }

    [MinLength(3)]
    [MaxLength(255)]
    public string Header { get; set; }

    [MinLength(3)]
    [MaxLength(255)]
    public string Description { get; set; }

}
