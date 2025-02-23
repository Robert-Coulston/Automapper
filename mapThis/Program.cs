using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace mapThis
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<DestinationContext>();
            var mapper = services.GetRequiredService<IMapper>();

            // create and seed the database
            // context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            DestinationContext.SetSeedDatabase(context, mapper);

            // query the database after seeding
            var destinationData = context.Accounts
                .Include(d => d.AccountValues)
                .ToList();

            Console.WriteLine("\nDestination Data:");
            foreach (var destination in destinationData)
            {
                Console.WriteLine($"Id: {destination.Id}, FullName: {destination.FullName}");
                Console.WriteLine("CategoryValues:");
                foreach (var categoryValue in destination.CategoryValues ?? Array.Empty<string>())
                {
                    Console.WriteLine($"  {categoryValue}");
                }

                Console.WriteLine("AccountValues:");
                foreach (var AccountValue in destination.AccountValues ?? new List<AccountValue>())
                {
                    Console.WriteLine($"  Id: {AccountValue.Id}, CategoryValue: {AccountValue.CategoryValue}, AccountId: {AccountValue.AccountId}");
                }
            }
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services
                        .AddDbContext<DestinationContext>(options =>
                            options.UseSqlite("Data Source=destination.db"))
                        .AddAutoMapper(typeof(MappingProfile)));
    }
}