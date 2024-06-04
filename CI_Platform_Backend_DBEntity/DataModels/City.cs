using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform_Backend_DBEntity.DataModels;

[Table("city")]
public partial class City
{
    [Key]
    [Column("city_id")]
    public long CityId { get; set; }

    [Column("country_id")]
    public long CountryId { get; set; }

    [Column("city")]
    [StringLength(50)]
    [Unicode(false)]
    public string City1 { get; set; } = null!;

    [ForeignKey("CountryId")]
    [InverseProperty("Cities")]
    public virtual Country Country { get; set; } = null!;

    [InverseProperty("City")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
