using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform_Backend_DBEntity.DataModels;

[Table("story_views")]
public partial class StoryView
{
    [Key]
    [Column("view_id")]
    public long ViewId { get; set; }

    [Column("story_id")]
    public long StoryId { get; set; }

    [Column("user_ids", TypeName = "text")]
    public string? UserIds { get; set; }

    [ForeignKey("StoryId")]
    [InverseProperty("StoryViews")]
    public virtual Story Story { get; set; } = null!;
}
