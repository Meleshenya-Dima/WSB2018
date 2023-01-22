using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldSkills.Model
{
    class Tickets
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        public int ScheduleID { get; set; }

        public int CabinTypeID { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string PassportNumber { get; set; }

        public int PassportCountryID { get; set; }

        public string BookingReference { get; set; }

        public bool Confirmed { get; set; }

        public string Birthday { get; set; }

        public string PassportCountryName { get; set; }

    }
}
