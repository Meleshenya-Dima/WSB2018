using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldSkills.Model
{
    class SearchFlightValue
    {
        public string From { get; set; }

        public string To { get; set; }

        public string CabinType { get; set; }

        public string Outbound { get; set; }

        public string Return { get; set; }

        public bool ThreeDaysOutbound { get; set; }

        public bool ThreeDaysReturn { get; set; }

    }
}
