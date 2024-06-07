using System.Text;
using CI_Platform_Backend_DBEntity.Context;
using CI_Platform_Backend_Repository.City;
using CI_Platform_Backend_Repository.CMSPrivacyPolicyRepo;
using CI_Platform_Backend_Repository.Comment;
using CI_Platform_Backend_Repository.ContactUs;
using CI_Platform_Backend_Repository.Country;
using CI_Platform_Backend_Repository.Mission;
using CI_Platform_Backend_Repository.Skill;
using CI_Platform_Backend_Repository.Story;
using CI_Platform_Backend_Repository.StoryView;
using CI_Platform_Backend_Repository.Theme;
using CI_Platform_Backend_Repository.User;
using CI_Platform_Backend_Repository.UserRepo;
using CI_Platform_Backend_Services;
using CI_Platform_Backend_Services.Auth;
using CI_Platform_Backend_Services.CMSPage;
using CI_Platform_Backend_Services.Comment;
using CI_Platform_Backend_Services.ContactUs;
using CI_Platform_Backend_Services.JwtService;
using CI_Platform_Backend_Services.Mission;
using CI_Platform_Backend_Services.Skill;
using CI_Platform_Backend_Services.Story;
using CI_Platform_Backend_Services.Theme;
using CI_Platform_Backend_Services.User;
using CI_Platform_Backend_Utilities.AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.MapType<DateOnly>(() => new OpenApiSchema { 
        Type = "string",
        Format = "date" });
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "JWT Authorization using Bearer scheme \n\n Add Like 'Bearer tokenValue'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
            {

            }
        }
    });
});
builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddAuthentication(x=>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;    
}
).AddJwtBearer(x=>{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("JWT:SecretKey")!)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddDbContext<ApplicationDbContext>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IThemeService, ThemeService>();
builder.Services.AddScoped<ISkillService, SkillService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserInformationRepo, UserInformationRepo>();
builder.Services.AddScoped<IContactUsService, ContactUsService>();
builder.Services.AddScoped<IMissionService, MissionService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IStoryService, StoryService>();
builder.Services.AddScoped<ICMSPageService, CMSPageService>();

builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<ICountryRepo, CountryRepo>();
builder.Services.AddScoped<ICityRepo, CityRepo>();
builder.Services.AddScoped<IThemeRepo, ThemeRepo>();
builder.Services.AddScoped<ISkillRepo, SkillRepo>();
builder.Services.AddScoped<IMissionRepo, MissionRepo>();
builder.Services.AddScoped<IContactUsRepo, ContactUsRepo>();
builder.Services.AddScoped<ICommentRepo, CommentRepo>();
builder.Services.AddScoped<IStoryRepo, StoryRepo>();
builder.Services.AddScoped<IStoryViewRepo, StoryViewRepo>();
builder.Services.AddScoped<ICMSPrivacyPolicyRepo, CMSPrivacyPolicyRepo>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/something-went-wrong");
app.UseHttpsRedirection();
app.MapControllers();
app.Run();