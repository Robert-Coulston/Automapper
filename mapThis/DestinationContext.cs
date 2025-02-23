using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace mapThis;

public class DestinationContext : DbContext
{
    public DestinationContext(DbContextOptions<DestinationContext> options) : base(options) { }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountValue> AccountValues { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>()
            .Property(d => d.CategoryValues)
            .HasConversion(
                v => string.Join(',', v ?? Array.Empty<string>()),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=destination.db");
        }
    }

    public static void SeedDatabase(DestinationContext context, IMapper mapper)
    {
        var sourceData = SourceClassTestData.GetTestData();
        // var sourceData = GetTestData();
        // var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        // var mapper = new Mapper(mapperConfig);
        var destinationData = mapper.Map<List<Account>>(sourceData);

        // Check if the destination data is already on the database
        if (context.Accounts.Any())
        {
            return;
        }
        context.Accounts.AddRange(destinationData);
        context.SaveChanges();
    }
}


