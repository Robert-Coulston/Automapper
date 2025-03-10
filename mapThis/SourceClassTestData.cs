using System.Collections.Generic;
using AutoMapper;
using Newtonsoft.Json;

namespace mapThis
{
    public static class SourceClassTestData
    {
        public static List<SourceClass> GetTestData()
        {
            var labelValuePairs = LabelValuePairTestData.GetTestData();
            var categoryValuesJson = JsonConvert.SerializeObject(labelValuePairs);

            return
            [
                new() { Id = 1, Name = "Source1", CategoryValues = categoryValuesJson },
                new() { Id = 2, Name = "Source2", CategoryValues = categoryValuesJson },
                new() { Id = 3, Name = "Source3", CategoryValues = categoryValuesJson }
            ];
        }
    }
}
