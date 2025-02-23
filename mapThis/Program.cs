using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace mapThis
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddDbContext<DestinationContext>(options =>
                    options.UseSqlite("Data Source=destination.db"))
                .AddAutoMapper(typeof(MappingProfile))
                .BuildServiceProvider();

            var mapper = serviceProvider.GetRequiredService<IMapper>();

            using var context = serviceProvider.GetRequiredService<DestinationContext>();

            // create and seed the database
            // context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            DestinationContext.SeedDatabase(context, mapper);

            // query the database after seeding
            var destinationData = context.DestinationClasses
                .Include(d => d.DestinationValueClasses)
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

                Console.WriteLine("DestinationValueClasses:");
                foreach (var destinationValueClass in destination.DestinationValueClasses ?? new List<DestinationValueClass>())
                {
                    Console.WriteLine($"  Id: {destinationValueClass.Id}, CategoryValue: {destinationValueClass.CategoryValue}, DestinationClassId: {destinationValueClass.DestinationClassId}");
                }
            }
        }
    }
}