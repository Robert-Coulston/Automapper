// filepath: /D:/Data/Code/mapThis/DestinationClass.cs
namespace mapThis;

public class DestinationClass
{
    public int Id { get; set; }
    public required string FullName { get; set; }
    public string[]? CategoryValues { get; set; }
    public List<DestinationValueClass>? DestinationValueClasses { get; set; }
}

public class DestinationValueClass
{
    public int Id { get; set; }
    public int DestinationClassId { get; set; }
    public required string CategoryValue { get; set; }
}


