using System.Collections.Generic;

namespace mapThis
{
    public static class LabelValuePairTestData
    {
        public static List<LabelValuePair> GetTestData()
        {
            return new List<LabelValuePair>
            {
                new LabelValuePair { Label = "Label1", Value = "Value1" },
                new LabelValuePair { Label = "Label2", Value = "Value2" },
                new LabelValuePair { Label = "Label3", Value = "Value3" }
            };
        }
    }
}
