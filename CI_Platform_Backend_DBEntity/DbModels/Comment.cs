﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform_Backend_DBEntity.DbModels;

[Table("comments")]
public partial class Comment
{
    [Key]
    [Column("comment_id")]
    public long CommentId { get; set; }

    [Column("mission_title")]
    [StringLength(50)]
    public string MissionTitle { get; set; } = null!;

    [Column("user_id")]
    public long UserId { get; set; }

    [Column("user_name")]
    [StringLength(50)]
    public string UserName { get; set; } = null!;

    [Column("comment")]
    [StringLength(256)]
    public string? Comment1 { get; set; }

    [Column("created_at", TypeName = "timestamp without time zone")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp without time zone")]
    public DateTime? UpdatedAt { get; set; }

    [Column("deleted_at", TypeName = "timestamp without time zone")]
    public DateTime? DeletedAt { get; set; }

    [Column("mission_id")]
    public long? MissionId { get; set; }

    [ForeignKey("MissionId")]
    [InverseProperty("Comments")]
    public virtual Mission? Mission { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Comments")]
    public virtual User User { get; set; } = null!;
}
