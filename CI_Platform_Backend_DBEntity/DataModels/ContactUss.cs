using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform_Backend_DBEntity.DataModels;

[Table("contact_uss")]
public partial class ContactUss
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("user_id")]
    public long UserId { get; set; }

    [Column("subject")]
    [StringLength(255)]
    [Unicode(false)]
    public string Subject { get; set; } = null!;

    [Column("message", TypeName = "text")]
    public string Message { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("ContactUsses")]
    public virtual User User { get; set; } = null!;
}
