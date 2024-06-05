using System.ComponentModel.DataAnnotations;

namespace CI_Platform_Backend_Presentation.DTO.CMSPages;

public class CMSPageDTO
{
    public long CMSPageID { get; set; }
    
    public string Title { get; set; }

    public string Description { get; set; }

    public string Slug { get; set; }

    public bool IsActive { get; set; }
}
