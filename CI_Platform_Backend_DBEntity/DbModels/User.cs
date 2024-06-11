using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform_Backend_DBEntity.DbModels;

[Table("user")]
[Index("Email", Name = "user_email_key", IsUnique = true)]
public partial class User
{
    [Key]
    [Column("user_id")]
    public long UserId { get; set; }

    [Column("first_name")]
    [StringLength(16)]
    public string? FirstName { get; set; }

    [Column("last_name")]
    [StringLength(16)]
    public string? LastName { get; set; }

    [Column("email")]
    [StringLength(128)]
    public string Email { get; set; } = null!;

    [Column("password")]
    [StringLength(255)]
    public string Password { get; set; } = null!;

    [Column("phone_number")]
    public long PhoneNumber { get; set; }

    [Column("skills")]
    [StringLength(255)]
    public string? Skills { get; set; }

    [Column("why_i_volunteer")]
    public string? WhyIVolunteer { get; set; }

    [Column("employee_id")]
    [StringLength(16)]
    public string? EmployeeId { get; set; }

    [Column("department")]
    [StringLength(16)]
    public string? Department { get; set; }

    [Column("city_id")]
    public long? CityId { get; set; }

    [Column("country_id")]
    public long? CountryId { get; set; }

    [Column("profile_text")]
    public string? ProfileText { get; set; }

    [Column("linked_in_url")]
    [StringLength(255)]
    public string? LinkedInUrl { get; set; }

    [Column("title")]
    [StringLength(255)]
    public string? Title { get; set; }

    [Column("status")]
    public short? Status { get; set; }

    [Column("created_at", TypeName = "timestamp without time zone")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp without time zone")]
    public DateTime? UpdatedAt { get; set; }

    [Column("deleted_at", TypeName = "timestamp without time zone")]
    public DateTime? DeletedAt { get; set; }

    [Column("avatar")]
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
    public virtual ICollection<MissionFav> MissionFavs { get; set; } = new List<MissionFav>();

    [InverseProperty("User")]
    public virtual ICollection<Story> Stories { get; set; } = new List<Story>();

    [InverseProperty("User")]
    public virtual ICollection<UserInformation> UserInformations { get; set; } = new List<UserInformation>();

    [InverseProperty("User")]
    public virtual ICollection<Volunteer> Volunteers { get; set; } = new List<Volunteer>();
}
