using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform_Backend_DBEntity.DataModels;

[Table("mission")]
public partial class Mission
{
    [Key]
    [Column("mission_id")]
    public long MissionId { get; set; }

    [Column("mission_title")]
    [StringLength(128)]
    [Unicode(false)]
    public string MissionTitle { get; set; } = null!;

    [Column("mission_short_description")]
    [StringLength(256)]
    [Unicode(false)]
    public string MissionShortDescription { get; set; } = null!;

    [Column("mission_description")]
    [StringLength(2048)]
    [Unicode(false)]
    public string MissionDescription { get; set; } = null!;

    [Column("country")]
    [StringLength(50)]
    [Unicode(false)]
    public string Country { get; set; } = null!;

    [Column("city")]
    [StringLength(50)]
    [Unicode(false)]
    public string City { get; set; } = null!;

    [Column("mission_organisation_name")]
    [StringLength(50)]
    [Unicode(false)]
    public string MissionOrganisationName { get; set; } = null!;

    [Column("mission_organisation_detail")]
    [StringLength(2048)]
    [Unicode(false)]
    public string MissionOrganisationDetail { get; set; } = null!;

    [Column("mission_start_date")]
    public DateOnly MissionStartDate { get; set; }

    [Column("mission_end_date")]
    public DateOnly MissionEndDate { get; set; }

    [Column("mission_type")]
    [StringLength(10)]
    [Unicode(false)]
    public string MissionType { get; set; } = null!;

    [Column("total_seats")]
    public long? TotalSeats { get; set; }

    [Column("mission_rating")]
    public int? MissionRating { get; set; }

    [Column("mission_rating_count")]
    public long? MissionRatingCount { get; set; }

    [Column("mission_registration_deadline")]
    public DateOnly? MissionRegistrationDeadline { get; set; }

    [Column("mission_theme")]
    [StringLength(20)]
    [Unicode(false)]
    public string MissionTheme { get; set; } = null!;

    [Column("mission_skills")]
    [StringLength(20)]
    [Unicode(false)]
    public string? MissionSkills { get; set; }

    [Column("mission_availability")]
    [StringLength(10)]
    [Unicode(false)]
    public string MissionAvailability { get; set; } = null!;

    [Column("mission_video")]
    [MaxLength(2048)]
    public byte[]? MissionVideo { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column("deleted_at", TypeName = "datetime")]
    public DateTime? DeletedAt { get; set; }

    [InverseProperty("Mission")]
    public virtual ICollection<MissionApplication> MissionApplications { get; set; } = new List<MissionApplication>();

    [InverseProperty("Mission")]
    public virtual ICollection<MissionMedium> MissionMedia { get; set; } = new List<MissionMedium>();

    [InverseProperty("Mission")]
    public virtual ICollection<RecentVolunteer> RecentVolunteers { get; set; } = new List<RecentVolunteer>();

    [InverseProperty("Mission")]
    public virtual ICollection<StoryMedium> StoryMedia { get; set; } = new List<StoryMedium>();

    [InverseProperty("Mission")]
    public virtual ICollection<VolunteeringTimesheet> VolunteeringTimesheets { get; set; } = new List<VolunteeringTimesheet>();
}
