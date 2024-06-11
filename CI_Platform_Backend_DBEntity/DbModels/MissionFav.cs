using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform_Backend_DBEntity.DbModels;

[Table("mission_favs")]
public partial class MissionFav
{
    [Key]
    [Column("favourite_id")]
    public long FavouriteId { get; set; }

    [Column("mission_id")]
    public long MissionId { get; set; }

    [Column("user_id")]
    public long UserId { get; set; }

    [ForeignKey("MissionId")]
    [InverseProperty("MissionFavs")]
    public virtual Mission Mission { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("MissionFavs")]
    public virtual User User { get; set; } = null!;
}
