using System;
using System.Collections.Generic;
using CI_Platform_Backend_DBEntity.DbModels;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform_Backend_DBEntity.Context;

public partial class CIPlatformDbContext : DbContext
{
    public CIPlatformDbContext()
    {
    }

    public CIPlatformDbContext(DbContextOptions<CIPlatformDbContext> options)
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

    public virtual DbSet<MissionFav> MissionFavs { get; set; }

    public virtual DbSet<MissionGoal> MissionGoals { get; set; }

    public virtual DbSet<MissionMedium> MissionMedia { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<Story> Stories { get; set; }

    public virtual DbSet<StoryMedium> StoryMedia { get; set; }

    public virtual DbSet<StoryView> StoryViews { get; set; }

    public virtual DbSet<Theme> Themes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserInformation> UserInformations { get; set; }

    public virtual DbSet<Volunteer> Volunteers { get; set; }

    public virtual DbSet<VolunteeringTimesheet> VolunteeringTimesheets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("name=dbconnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("admin_pkey");

            entity.Property(e => e.AdminId).UseIdentityAlwaysColumn();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("city_pkey");

            entity.Property(e => e.CityId).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.Country).WithMany(p => p.Cities).HasConstraintName("city_country_id_fkey");
        });

        modelBuilder.Entity<CmsPrivacyPolicy>(entity =>
        {
            entity.HasKey(e => e.CmsId).HasName("cms_privacy_policy_pkey");

            entity.Property(e => e.CmsId).UseIdentityAlwaysColumn();
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("comments_pkey");

            entity.Property(e => e.CommentId).UseIdentityAlwaysColumn();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");

            entity.HasOne(d => d.Mission).WithMany(p => p.Comments).HasConstraintName("comments_mission_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("comments_user_id_fkey");
        });

        modelBuilder.Entity<ContactUss>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("contact_uss_pkey");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.User).WithMany(p => p.ContactUsses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("contact_uss_user_id_fkey");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("country_pkey");

            entity.Property(e => e.CountryId).UseIdentityAlwaysColumn();
        });

        modelBuilder.Entity<LoginCarousel>(entity =>
        {
            entity.HasKey(e => e.CarouselId).HasName("login_carousel_pkey");

            entity.Property(e => e.CarouselId).UseIdentityAlwaysColumn();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
        });

        modelBuilder.Entity<Mission>(entity =>
        {
            entity.HasKey(e => e.MissionId).HasName("mission_pkey");

            entity.Property(e => e.MissionId).UseIdentityAlwaysColumn();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
            entity.Property(e => e.MissionRating).HasDefaultValue(0);
            entity.Property(e => e.MissionRatingCount).HasDefaultValue(0L);
            entity.Property(e => e.Status).HasDefaultValue(1);
        });

        modelBuilder.Entity<MissionApplication>(entity =>
        {
            entity.HasKey(e => e.ApplicationId).HasName("mission_application_pkey");

            entity.Property(e => e.ApplicationId).UseIdentityAlwaysColumn();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");

            entity.HasOne(d => d.Mission).WithMany(p => p.MissionApplications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("mission_application_mission_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.MissionApplications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("mission_application_user_id_fkey");
        });

        modelBuilder.Entity<MissionFav>(entity =>
        {
            entity.HasKey(e => e.FavouriteId).HasName("mission_favs_pkey");

            entity.Property(e => e.FavouriteId).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.Mission).WithMany(p => p.MissionFavs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("mission_favs_mission_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.MissionFavs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("mission_favs_user_id_fkey");
        });

        modelBuilder.Entity<MissionGoal>(entity =>
        {
            entity.HasKey(e => e.GoalId).HasName("mission_goals_pkey");

            entity.Property(e => e.GoalId).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.Mission).WithMany(p => p.MissionGoals)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("mission_goals_mission_id_fkey");
        });

        modelBuilder.Entity<MissionMedium>(entity =>
        {
            entity.HasKey(e => e.MediaId).HasName("mission_media_pkey");

            entity.Property(e => e.MediaId).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.Mission).WithMany(p => p.MissionMedia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("mission_media_mission_id_fkey");
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.SkillId).HasName("skills_pkey");

            entity.Property(e => e.SkillId).UseIdentityAlwaysColumn();
        });

        modelBuilder.Entity<Story>(entity =>
        {
            entity.HasKey(e => e.StoryId).HasName("story_pkey");

            entity.Property(e => e.StoryId).UseIdentityAlwaysColumn();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");

            entity.HasOne(d => d.User).WithMany(p => p.Stories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("story_user_id_fkey");
        });

        modelBuilder.Entity<StoryMedium>(entity =>
        {
            entity.HasKey(e => e.MediaId).HasName("story_media_pkey");

            entity.Property(e => e.MediaId).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.Mission).WithMany(p => p.StoryMedia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("story_media_mission_id_fkey");

            entity.HasOne(d => d.Story).WithMany(p => p.StoryMedia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("story_media_story_id_fkey");
        });

        modelBuilder.Entity<StoryView>(entity =>
        {
            entity.HasKey(e => e.ViewId).HasName("story_views_pkey");

            entity.Property(e => e.ViewId).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.Story).WithMany(p => p.StoryViews)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("story_views_story_id_fkey");
        });

        modelBuilder.Entity<Theme>(entity =>
        {
            entity.HasKey(e => e.ThemeId).HasName("theme_pkey");

            entity.Property(e => e.ThemeId).UseIdentityAlwaysColumn();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("user_pkey");

            entity.Property(e => e.UserId).UseIdentityAlwaysColumn();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");

            entity.HasOne(d => d.City).WithMany(p => p.Users).HasConstraintName("user_city_id_fkey");

            entity.HasOne(d => d.Country).WithMany(p => p.Users).HasConstraintName("user_country_id_fkey");
        });

        modelBuilder.Entity<UserInformation>(entity =>
        {
            entity.HasKey(e => e.InformationId).HasName("user_information_pkey");

            entity.Property(e => e.InformationId).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.User).WithMany(p => p.UserInformations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_information_user_id_fkey");
        });

        modelBuilder.Entity<Volunteer>(entity =>
        {
            entity.HasKey(e => e.VolunteerId).HasName("volunteers_pkey");

            entity.Property(e => e.VolunteerId).UseIdentityAlwaysColumn();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
            entity.Property(e => e.Status).HasDefaultValue(1);

            entity.HasOne(d => d.Mission).WithMany(p => p.Volunteers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("volunteers_mission_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Volunteers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("volunteers_user_id_fkey");
        });

        modelBuilder.Entity<VolunteeringTimesheet>(entity =>
        {
            entity.HasKey(e => e.VolunteeringId).HasName("volunteering_timesheet_pkey");

            entity.Property(e => e.VolunteeringId).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.Mission).WithMany(p => p.VolunteeringTimesheets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("volunteering_timesheet_mission_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
