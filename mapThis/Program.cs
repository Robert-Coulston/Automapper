using System;
using AutoMapper;

namespace mapThis
{
    class Program
    {
        static void Main(string[] args)
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            var mapper = new Mapper(config);

            var sourceData = SourceClassTestData.GetTestData();
            var destinationData = mapper.Map<List<DestinationClass>>(sourceData);

            Console.WriteLine("Source Data:");
            foreach (var source in sourceData)
            {
                Console.WriteLine($"Id: {source.Id}, Name: {source.Name}, CategoryValues: {source.CategoryValues}");
            }

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