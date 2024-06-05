using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform_Backend_DBEntity.DataModels;

[Table("user")]
[Index("Email", Name = "UQ__user__AB6E6164BB139117", IsUnique = true)]
public partial class User
{
    [Key]
    [Column("user_id")]
    public long UserId { get; set; }

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

    [Column("password")]
    [StringLength(255)]
    [Unicode(false)]
    public string Password { get; set; } = null!;

    [Column("phone_number")]
    public long PhoneNumber { get; set; }

    [Column("skills")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Skills { get; set; }

    [Column("why_i_volunteer", TypeName = "text")]
    public string? WhyIVolunteer { get; set; }

    [Column("employee_id")]
    [StringLength(16)]
    [Unicode(false)]
    public string? EmployeeId { get; set; }

    [Column("department")]
    [StringLength(16)]
    [Unicode(false)]
    public string? Department { get; set; }

    [Column("city_id")]
    public long? CityId { get; set; }

    [Column("country_id")]
    public long? CountryId { get; set; }

    [Column("profile_text", TypeName = "text")]
    public string? ProfileText { get; set; }

    [Column("linked_in_url")]
    [StringLength(255)]
    [Unicode(false)]
    public string? LinkedInUrl { get; set; }

    [Column("title")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Title { get; set; }

    [Column("status")]
    public byte? Status { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column("deleted_at", TypeName = "datetime")]
    public DateTime? DeletedAt { get; set; }

    [Column("avatar")]
    [MaxLength(2048)]
    public byte[]? Avatar { get; set; }

    [ForeignKey("CityId")]
    [InverseProperty("Users")]
    public virtual City? City { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    [InverseProperty("User")]
    public virtual ICollection<ContactUss> ContactUsses { get; set; } = new List<ContactUss>();

    [ForeignKey("CountryId")]
    [InverseProperty("Users")]
    public virtual Country? Country { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<MissionApplication> MissionApplications { get; set; } = new List<MissionApplication>();

    [InverseProperty("User")]
    public virtual ICollection<RecentVolunteer> RecentVolunteers { get; set; } = new List<RecentVolunteer>();

    [InverseProperty("User")]
    public virtual ICollection<Story> Stories { get; set; } = new List<Story>();

    [InverseProperty("User")]
    public virtual ICollection<UserInformation> UserInformations { get; set; } = new List<UserInformation>();
}
