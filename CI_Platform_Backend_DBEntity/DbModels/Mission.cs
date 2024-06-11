using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform_Backend_DBEntity.DbModels;

[Table("mission")]
public partial class Mission
{
    [Key]
    [Column("mission_id")]
    public long MissionId { get; set; }

    [Column("mission_title")]
    [StringLength(128)]
    public string MissionTitle { get; set; } = null!;

    [Column("mission_short_description")]
    [StringLength(256)]
    public string MissionShortDescription { get; set; } = null!;

    [Column("mission_description")]
    [StringLength(2048)]
    public string MissionDescription { get; set; } = null!;

    [Column("country")]
    [StringLength(50)]
    public string Country { get; set; } = null!;

    [Column("city")]
    [StringLength(50)]
    public string City { get; set; } = null!;

    [Column("mission_organisation_name")]
    [StringLength(50)]
    public string MissionOrganisationName { get; set; } = null!;

    [Column("mission_organisation_detail")]
    [StringLength(2048)]
    public string MissionOrganisationDetail { get; set; } = null!;

    [Column("mission_start_date")]
    public DateOnly MissionStartDate { get; set; }

    [Column("mission_end_date")]
    public DateOnly MissionEndDate { get; set; }

    [Column("mission_type")]
    [StringLength(10)]
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
    public string MissionTheme { get; set; } = null!;

    [Column("mission_skills")]
    [StringLength(20)]
    public string? MissionSkills { get; set; }

    [Column("mission_availability")]
    [StringLength(10)]
    public string MissionAvailability { get; set; } = null!;

    [Column("mission_video")]
    public byte[]? MissionVideo { get; set; }

    [Column("created_at", TypeName = "timestamp without time zone")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp without time zone")]
    public DateTime? UpdatedAt { get; set; }

    [Column("deleted_at", TypeName = "timestamp without time zone")]
    public DateTime? DeletedAt { get; set; }

    [Column("status")]
    public int Status { get; set; }

    [InverseProperty("Mission")]
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    [InverseProperty("Mission")]
    public virtual ICollection<MissionApplication> MissionApplications { get; set; } = new List<MissionApplication>();

    [InverseProperty("Mission")]
    public virtual ICollection<MissionFav> MissionFavs { get; set; } = new List<MissionFav>();

    [InverseProperty("Mission")]
    public virtual ICollection<MissionGoal> MissionGoals { get; set; } = new List<MissionGoal>();

    [InverseProperty("Mission")]
    public virtual ICollection<MissionMedium> MissionMedia { get; set; } = new List<MissionMedium>();

    [InverseProperty("Mission")]
    public virtual ICollection<StoryMedium> StoryMedia { get; set; } = new List<StoryMedium>();

    [InverseProperty("Mission")]
    public virtual ICollection<VolunteeringTimesheet> VolunteeringTimesheets { get; set; } = new List<VolunteeringTimesheet>();

    [InverseProperty("Mission")]
    public virtual ICollection<Volunteer> Volunteers { get; set; } = new List<Volunteer>();
}
