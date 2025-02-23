using System.Collections.Generic;

namespace mapThis
{
    public static class LabelValuePairTestData
    {
        public static List<LabelValuePair> GetTestData()
        {
            return new List<LabelValuePair>
            {
                new() { Label = "Label1", Value = "Value1" },
                new() { Label = "Label2", Value = "Value2" },
                new() { Label = "Label3", Value = "Value3" }
            };
        }
    }
}
