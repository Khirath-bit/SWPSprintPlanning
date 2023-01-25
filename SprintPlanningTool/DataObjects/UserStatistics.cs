using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprintPlanningTool.DataObjects
{
    internal class UserStatistics
    {
        public string Name { get; set; }

        public int LinesOfCode { get; set; }

        public int StoryPoints { get; set; }

        public override string ToString()
        {
            return Name + " LoC:" + LinesOfCode + " SP:" + StoryPoints;
        }
    }
}
