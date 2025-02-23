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

    public static void SetSeedDatabase(DestinationContext context, IMapper mapper)
    {
        var sourceData = SourceClassTestData.GetTestData();
        // var sourceData = GetTestData();
        // var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        // var mapper = new Mapper(mapperConfig);
        var destinationData = mapper.Map<List<Account>>(sourceData);

        foreach (var account in destinationData)
        {
            var existingAccount = context.Accounts.Include(a => a.AccountValues).SingleOrDefault(a => a.Id == account.Id);
            if (existingAccount != null)
            {
                context.Entry(existingAccount).CurrentValues.SetValues(account);

                // Update AccountValues
                foreach (var accountValue in account.AccountValues ?? new List<AccountValue>())
                {
                    var existingAccountValue = existingAccount.AccountValues?
                        .SingleOrDefault(av => av.AccountId == accountValue.AccountId && av.CategoryValue == accountValue.CategoryValue);
                    if (existingAccountValue == null)
                    {
                        existingAccount.AccountValues?.Add(accountValue);
                    }
                }

                // Remove AccountValues that are no longer present
                foreach (var existingAccountValue in existingAccount.AccountValues?.ToList() ?? new List<AccountValue>())
                {
                    if (!(account.AccountValues?.Any(av => av.AccountId == existingAccountValue.AccountId && av.CategoryValue == existingAccountValue.CategoryValue) ?? false))
                    {
                        context.AccountValues.Remove(existingAccountValue);
                    }
                }
            }
            else
            {
                context.Accounts.Add(account);
            }
        }

        context.SaveChanges();
    }
}


