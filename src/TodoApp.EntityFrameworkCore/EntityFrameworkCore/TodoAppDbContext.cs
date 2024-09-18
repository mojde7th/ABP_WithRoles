using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TodoApp.Domain;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using Task = TodoApp.Domain.Task;
using Layer = TodoApp.Domain.PlanLayer;
namespace TodoApp.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class TodoAppDbContext :
    AbpDbContext<TodoAppDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */
    public DbSet<Book> Books { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    #region Entities from the modules

   
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }

    // Tenant Management
    public DbSet<Task> Tasks { get; set; }
    public DbSet<Layer> Layers { get; set; }
    public DbSet<DailyPlan> DailyPlans { get; set; }
    

    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public TodoAppDbContext(DbContextOptions<TodoAppDbContext> options)
        : base(options)
    {

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
       
            optionsBuilder.EnableSensitiveDataLogging();
        
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */
        builder.Entity<Book>(b =>
        {
            b.ToTable("Books");
            b.ConfigureByConvention(); // auto configure for the base class props
            b.Property(x => x.Name).IsRequired().HasMaxLength(128);
            b.HasOne(book => book.Publisher)
             .WithMany(publisher => publisher.Books)
             .HasForeignKey(book => book.PublisherId)
             .IsRequired();
        });
        builder.Entity<DailyPlan>(b => {
            b.ToTable("NewDailyPlans");
            b.ConfigureByConvention();
            b.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            b.HasMany(dp=>dp.Tasks).WithOne(t => t.DailyPlan)
            .HasForeignKey(t=>t.DailyPlanId)
            .IsRequired();
            b.HasMany(dp => dp.Layers)
            .WithOne(l => l.DailyPlan)
            .HasForeignKey(l=>l.DailyPlanId).
            IsRequired();
        });
        builder.Entity<Task>(b =>
        {
            b.ToTable("Tasks");
            b.ConfigureByConvention();
            b.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            b.HasOne(t => t.DailyPlan)
            .WithMany(dp => dp.Tasks)
            .HasForeignKey(t => t.DailyPlanId);
        });
        builder.Entity<PlanLayer>(b =>
        {
            b.ToTable("Layers");
            b.ConfigureByConvention();
            b.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            b.HasOne(l => l.DailyPlan)
            .WithMany(dp => dp.Layers)
            .HasForeignKey(L => L.DailyPlanId);
        });
        builder.Entity<Publisher>(b =>
        {
            b.ToTable("Publishers");
            b.ConfigureByConvention(); // auto configure for the base class props
            b.Property(x => x.Name).IsRequired().HasMaxLength(128);
        });
        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

      
    }
}
