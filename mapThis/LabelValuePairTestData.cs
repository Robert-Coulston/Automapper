using System;
using System.Collections.Generic;

namespace mapThis
{
    public static class LabelValuePairTestData
    {
        public static List<LabelValuePair> GetTestData()
        {
            return new List<LabelValuePair>
            {
                new() { Label = "Label1", Value = Guid.NewGuid() },
                new() { Label = "Label2", Value = Guid.NewGuid() },
                new() { Label = "Label3", Value = Guid.NewGuid() }
            };
        }
    }
}
