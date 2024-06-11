using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform_Backend_DBEntity.DbModels;

[Table("theme")]
public partial class Theme
{
    [Key]
    [Column("theme_id")]
    public long ThemeId { get; set; }

    [Column("theme")]
    [StringLength(50)]
    public string Theme1 { get; set; } = null!;

    [Column("status")]
    public bool Status { get; set; }
}
