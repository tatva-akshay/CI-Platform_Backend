using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform_Backend_DBEntity.DbModels;

[Table("login_carousel")]
public partial class LoginCarousel
{
    [Key]
    [Column("carousel_id")]
    public long CarouselId { get; set; }

    [Column("carousel_image")]
    public byte[] CarouselImage { get; set; } = null!;

    [Column("carousel_head")]
    [StringLength(255)]
    public string? CarouselHead { get; set; }

    [Column("carousel_text")]
    [StringLength(255)]
    public string? CarouselText { get; set; }

    [Column("created_at", TypeName = "timestamp without time zone")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp without time zone")]
    public DateTime? UpdatedAt { get; set; }

    [Column("deleted_at", TypeName = "timestamp without time zone")]
    public DateTime? DeletedAt { get; set; }
}
