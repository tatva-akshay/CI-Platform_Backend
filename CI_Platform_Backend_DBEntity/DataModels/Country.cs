using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform_Backend_DBEntity.DataModels;

[Table("country")]
public partial class Country
{
    [Key]
    [Column("country_id")]
    public long CountryId { get; set; }

    [Column("country")]
    [StringLength(50)]
    [Unicode(false)]
    public string Country1 { get; set; } = null!;

    [InverseProperty("Country")]
    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    [InverseProperty("Country")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
