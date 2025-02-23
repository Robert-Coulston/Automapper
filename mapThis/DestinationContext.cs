using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace mapThis;

public class DestinationContext : DbContext
{
    public DestinationContext(DbContextOptions<DestinationContext> options) : base(options) { }

    public DbSet<DestinationClass> DestinationClasses { get; set; }
    public DbSet<DestinationValueClass> DestinationValueClasses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DestinationClass>()
            .Property(d => d.CategoryValues)
            .HasConversion(
                v => string.Join(',', v ?? Array.Empty<string>()),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));
    }

    public static void SeedDatabase(DestinationContext context, IMapper mapper)
    {
        // var destinationData = mapper.Map<List<DestinationClass>>(sourceData);

        var sourceData = SourceClassTestData.GetTestData();
        // var sourceData = GetTestData();
        // var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        // var mapper = new Mapper(mapperConfig);
        var destinationData = mapper.Map<List<DestinationClass>>(sourceData);

        // Check if the destination data is already on the database
        if (context.DestinationClasses.Any())
        {
            return;
        }
        context.DestinationClasses.AddRange(destinationData);
        context.SaveChanges();
    }
}


