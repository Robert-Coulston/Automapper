using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mapThis
{
    public class LabelValuePair
    {
        public required string Label { get; set; }
        public required Guid Value { get; set; }
    }
}