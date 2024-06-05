using System.ComponentModel.DataAnnotations;

namespace CI_Platform_Backend_Presentation.DTO.CreateTheme;

public class CreateThemeDTO
{
    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    public string Name { get; set; }

    [Required]
    public bool IsActive { get; set; }
}
