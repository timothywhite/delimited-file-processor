using System;
using System.Collections.Generic;
using System.Text;

namespace RunbeckTest.Dto
{
    public class ResultDto
    {
        public IEnumerable<string> ValidRows { get; set; }
        public IEnumerable<string> InvalidRows { get; set; }
    }
}
