using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform_Backend_DBEntity.DbModels;

[Table("user_information")]
public partial class UserInformation
{
    [Key]
    [Column("information_id")]
    public long InformationId { get; set; }

    [Column("user_id")]
    public long UserId { get; set; }

    [Column("description")]
    [StringLength(255)]
    public string Description { get; set; } = null!;

    [Column("gender")]
    public int Gender { get; set; }

    [Column("availability")]
    public int Availability { get; set; }

    [Column("age_group")]
    public int AgeGroup { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("UserInformations")]
    public virtual User User { get; set; } = null!;
}
