using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform_Backend_DBEntity.DataModels;

[Table("skills")]
public partial class Skill
{
    [Key]
    [Column("skill_id")]
    public long SkillId { get; set; }

    [Column("skills")]
    [StringLength(20)]
    [Unicode(false)]
    public string Skills { get; set; } = null!;

    [Column("status")]
    public bool Status { get; set; }
}
