// filepath: /D:/Data/Code/mapThis/DestinationClass.cs
namespace mapThis;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class DestinationClass
{
    [Key]
    public int Id { get; set; }
    public required string FullName { get; set; }
    public string[]? CategoryValues { get; set; }
    public List<DestinationValueClass>? DestinationValueClasses { get; set; }
}

public class DestinationValueClass
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("DestinationClass")]
    public int DestinationClassId { get; set; }
    public required string CategoryValue { get; set; }
    public required DestinationClass DestinationClass { get; set; }
}


