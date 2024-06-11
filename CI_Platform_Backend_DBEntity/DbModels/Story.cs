using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform_Backend_DBEntity.DbModels;

[Table("story")]
public partial class Story
{
    [Key]
    [Column("story_id")]
    public long StoryId { get; set; }

    [Column("story_title")]
    [StringLength(50)]
    public string StoryTitle { get; set; } = null!;

    [Column("mission_title")]
    [StringLength(50)]
    public string MissionTitle { get; set; } = null!;

    [Column("user_id")]
    public long UserId { get; set; }

    [Column("story_description")]
    [StringLength(1000)]
    public string StoryDescription { get; set; } = null!;

    [Column("publish")]
    public bool? Publish { get; set; }

    [Column("created_at", TypeName = "timestamp without time zone")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp without time zone")]
    public DateTime? UpdatedAt { get; set; }

    [Column("deleted_at", TypeName = "timestamp without time zone")]
    public DateTime? DeletedAt { get; set; }

    [InverseProperty("Story")]
    public virtual ICollection<StoryMedium> StoryMedia { get; set; } = new List<StoryMedium>();

    [InverseProperty("Story")]
    public virtual ICollection<StoryView> StoryViews { get; set; } = new List<StoryView>();

    [ForeignKey("UserId")]
    [InverseProperty("Stories")]
    public virtual User User { get; set; } = null!;
}
