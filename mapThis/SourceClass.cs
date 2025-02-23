// filepath: /D:/Data/Code/mapThis/SourceClass.cs
using Newtonsoft.Json;

namespace mapThis;
public class SourceClass
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? CategoryValues { get; set; }

    public string[]? GetCategoryValuesArray()
    {
        if (string.IsNullOrEmpty(CategoryValues))
        {
            return Array.Empty<string>();
        }

        var labelValuePairs = JsonConvert.DeserializeObject<List<LabelValuePair>>(CategoryValues);
        var guids = labelValuePairs?.Select(lvp => lvp.Value).ToArray();
        return guids?.Select(guid => guid.ToString()).ToArray();
    }
}



