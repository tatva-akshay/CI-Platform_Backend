using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform_Backend_DBEntity.DataModels;

[Table("admin")]
[Index("Email", Name = "UQ__admin__AB6E6164FB52F48E", IsUnique = true)]
public partial class Admin
{
    [Key]
    [Column("admin_id")]
    public long AdminId { get; set; }

    [Column("first_name")]
    [StringLength(16)]
    [Unicode(false)]
    public string? FirstName { get; set; }

    [Column("last_name")]
    [StringLength(16)]
    [Unicode(false)]
    public string? LastName { get; set; }

    [Column("email")]
    [StringLength(128)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [Column("admin_avatar")]
    [StringLength(2048)]
    public string? AdminAvatar { get; set; }

    [Column("password")]
    [StringLength(255)]
    [Unicode(false)]
    public string Password { get; set; } = null!;

    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column("deleted_at", TypeName = "datetime")]
    public DateTime? DeletedAt { get; set; }
}
