using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform_Backend_DBEntity.DbModels;

[Table("admin")]
[Index("Email", Name = "admin_email_key", IsUnique = true)]
public partial class Admin
{
    [Key]
    [Column("admin_id")]
    public long AdminId { get; set; }

    [Column("first_name")]
    [StringLength(16)]
    public string? FirstName { get; set; }

    [Column("last_name")]
    [StringLength(16)]
    public string? LastName { get; set; }

    [Column("email")]
    [StringLength(128)]
    public string Email { get; set; } = null!;

    [Column("admin_avatar")]
    public byte[]? AdminAvatar { get; set; }

    [Column("password")]
    [StringLength(255)]
    public string Password { get; set; } = null!;

    [Column("created_at", TypeName = "timestamp without time zone")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp without time zone")]
    public DateTime? UpdatedAt { get; set; }

    [Column("deleted_at", TypeName = "timestamp without time zone")]
    public DateTime? DeletedAt { get; set; }
}
