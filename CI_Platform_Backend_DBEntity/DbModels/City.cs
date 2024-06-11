using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform_Backend_DBEntity.DbModels;

[Table("city")]
public partial class City
{
    [Key]
    [Column("city_id")]
    public long CityId { get; set; }

    [Column("country_id")]
    public long? CountryId { get; set; }

    [Column("city")]
    [StringLength(50)]
    public string City1 { get; set; } = null!;

    [ForeignKey("CountryId")]
    [InverseProperty("Cities")]
    public virtual Country? Country { get; set; }

    [InverseProperty("City")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
