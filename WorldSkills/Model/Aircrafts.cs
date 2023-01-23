using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldSkills.Model
{
    class Aircrafts
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string MakeModel { get; set; }

        public int TotalSeats { get; set; }

        public int EconomySeats { get; set; }

        public int BusinessSeats { get; set; }
    }
}
