using System;
using System.Collections.Generic;
using CI_Platform_Backend_DBEntity.DataModels;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform_Backend_DBEntity.Context;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<CmsPrivacyPolicy> CmsPrivacyPolicies { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<ContactUss> ContactUsses { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<LoginCarousel> LoginCarousels { get; set; }

    public virtual DbSet<Mission> Missions { get; set; }

    public virtual DbSet<MissionApplication> MissionApplications { get; set; }

    public virtual DbSet<MissionMedium> MissionMedia { get; set; }

    public virtual DbSet<RecentVolunteer> RecentVolunteers { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<Story> Stories { get; set; }

    public virtual DbSet<StoryMedium> StoryMedia { get; set; }

    public virtual DbSet<Theme> Themes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserInformation> UserInformations { get; set; }

    public virtual DbSet<VolunteeringTimesheet> VolunteeringTimesheets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=dbconnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__admin__43AA4141560E5F09");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("PK__city__031491A8BC9B7913");

            entity.HasOne(d => d.Country).WithMany(p => p.Cities)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__city__country_id__71D1E811");
        });

        modelBuilder.Entity<CmsPrivacyPolicy>(entity =>
        {
            entity.HasKey(e => e.CmsId).HasName("PK__cms_priv__760783F7C46F66B6");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__comments__E795768759769305");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__comments__user_i__03F0984C");
        });

        modelBuilder.Entity<ContactUss>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__contact___3213E83FD5BC3CF2");

            entity.HasOne(d => d.User).WithMany(p => p.ContactUsses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__contact_u__user___1EA48E88");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PK__country__7E8CD055BEAC166C");
        });

        modelBuilder.Entity<LoginCarousel>(entity =>
        {
            entity.HasKey(e => e.CarouselId).HasName("PK__login_ca__5EDD71781813EEFD");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Mission>(entity =>
        {
            entity.HasKey(e => e.MissionId).HasName("PK__mission__B5419AB20B1CDF4A");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.MissionRating).HasDefaultValue(0);
            entity.Property(e => e.MissionRatingCount).HasDefaultValue(0L);
        });

        modelBuilder.Entity<MissionApplication>(entity =>
        {
            entity.HasKey(e => e.ApplicationId).HasName("PK__mission___3BCBDCF22952D857");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Mission).WithMany(p => p.MissionApplications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__mission_a__missi__0B91BA14");

            entity.HasOne(d => d.User).WithMany(p => p.MissionApplications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__mission_a__user___0A9D95DB");
        });

        modelBuilder.Entity<MissionMedium>(entity =>
        {
            entity.HasKey(e => e.MediaId).HasName("PK__mission___D0A840F4D4FA430F");

            entity.HasOne(d => d.Mission).WithMany(p => p.MissionMedia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__mission_m__missi__07C12930");
        });

        modelBuilder.Entity<RecentVolunteer>(entity =>
        {
            entity.HasKey(e => e.VolunteerId).HasName("PK__recent_v__0FE766B1271098F9");

            entity.HasOne(d => d.Mission).WithMany(p => p.RecentVolunteers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__recent_vo__missi__0F624AF8");

            entity.HasOne(d => d.User).WithMany(p => p.RecentVolunteers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__recent_vo__user___10566F31");
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.SkillId).HasName("PK__skills__FBBA8379AF1AAB20");
        });

        modelBuilder.Entity<Story>(entity =>
        {
            entity.HasKey(e => e.StoryId).HasName("PK__story__66339C5655DCEB6C");

            entity.HasOne(d => d.User).WithMany(p => p.Stories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__story__user_id__1332DBDC");
        });

        modelBuilder.Entity<StoryMedium>(entity =>
        {
            entity.HasKey(e => e.MediaId).HasName("PK__story_me__D0A840F4849A0804");

            entity.HasOne(d => d.Mission).WithMany(p => p.StoryMedia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__story_med__missi__18EBB532");
        });

        modelBuilder.Entity<Theme>(entity =>
        {
            entity.HasKey(e => e.ThemeId).HasName("PK__theme__73CEC20A1F86FD7E");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__user__B9BE370FE496342A");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.City).WithMany(p => p.Users).HasConstraintName("FK__user__city_id__778AC167");

            entity.HasOne(d => d.Country).WithMany(p => p.Users).HasConstraintName("FK__user__country_id__787EE5A0");
        });

        modelBuilder.Entity<UserInformation>(entity =>
        {
            entity.HasKey(e => e.InformationId).HasName("PK__user_inf__26E2EF41C1B8A04F");

            entity.HasOne(d => d.User).WithMany(p => p.UserInformations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__user_info__user___2180FB33");
        });

        modelBuilder.Entity<VolunteeringTimesheet>(entity =>
        {
            entity.HasKey(e => e.VolunteeringId).HasName("PK__voluntee__571F1831F96C3B5F");

            entity.HasOne(d => d.Mission).WithMany(p => p.VolunteeringTimesheets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__volunteer__missi__160F4887");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
