using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform_Backend_DBEntity.DataModels;

[Table("story_media")]
public partial class StoryMedium
{
    [Key]
    [Column("media_id")]
    public long MediaId { get; set; }

    [Column("image")]
    public byte[]? Image { get; set; }

    [Column("document")]
    public byte[]? Document { get; set; }

    [Column("mission_id")]
    public long MissionId { get; set; }

    [Column("story_id")]
    public long StoryId { get; set; }

    [ForeignKey("MissionId")]
    [InverseProperty("StoryMedia")]
    public virtual Mission Mission { get; set; } = null!;

    [ForeignKey("StoryId")]
    [InverseProperty("StoryMedia")]
    public virtual Story Story { get; set; } = null!;
}
