using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform_Backend_DBEntity.DataModels;

[Table("cms_privacy_policy")]
public partial class CmsPrivacyPolicy
{
    [Key]
    [Column("cms_id")]
    public long CmsId { get; set; }

    [Column("page_title")]
    [StringLength(50)]
    [Unicode(false)]
    public string PageTitle { get; set; } = null!;

    [Column("page_description")]
    [StringLength(2048)]
    [Unicode(false)]
    public string PageDescription { get; set; } = null!;

    [Column("slug")]
    [StringLength(1000)]
    [Unicode(false)]
    public string Slug { get; set; } = null!;

    [Column("status")]
    public bool Status { get; set; }
}
