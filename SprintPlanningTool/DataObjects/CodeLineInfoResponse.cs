using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprintPlanningTool.DataObjects
{
    internal class CodeLineInfoResponse
    {
        public bool IsLastPage { get; set; }

        public List<CodeLineInfoValues> Values { get; set; }
    }

    internal class CodeLineInfoValues
    {
        public CodeLineInfoValuesUser User { get; set; }

        public CodeLineInfoLinesOfCode LinesOfCode { get; set; }

        public CodeLineInfoValuesAuthor Author { get; set; }
    }

    internal class CodeLineInfoLinesOfCode
    {
        public int Added { get; set; }

        public int Deleted { get; set; }
    }

    internal class CodeLineInfoValuesUser
    {
        public string Name { get; set; }
    }

    internal class CodeLineInfoValuesAuthor
    {
        public string EmailAddress { get; set; }
    }
}
