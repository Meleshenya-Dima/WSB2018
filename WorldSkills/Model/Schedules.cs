using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using WorldSkills.ViewModel;

namespace WorldSkills.Model
{
    class Schedules : INotifyPropertyChanged
    {
        private DateTime _time;
        private DateTime _date;
        private int _routeID;
        private bool _confirmed;
        private string _departureAirport;
        private string _arrivalAirport;
        private decimal _economyPrice;
        private string _cabinePrice;
        private string _cabineName;
        public int ID { get; set; }

        public DateTime Data
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged("Data");
            }
        }

        public DateTime Time
        {
            get => _time;
            set
            {
                _time = value;
                OnPropertyChanged("Time");
            }
        }

        public int AircraftID { get; set; }

        public int RouteID
        {
            get => _routeID;
            set
            {
                _routeID = value;
            }
        }

        public decimal EconomyPrice
        {
            get => _economyPrice;
            set
            {
                _economyPrice = value;
                OnPropertyChanged("EconomyPrice");
            }
        }

        public decimal BusinessPrice { get; set; }

        public decimal FirstClassPrice { get; set; }

        public string DepartureAirport
        {
            get => _departureAirport;
            set
            {
                _departureAirport = value;
                OnPropertyChanged("DepartureAirport");  
            }
        }

        public string ArrivalAirport
        {
            get => _arrivalAirport;
            set
            {
                _arrivalAirport = value;
                OnPropertyChanged("ArrivalAirport");
            }
        }

        public bool Confirmed
        {
            get => _confirmed;
            set
            {
                _confirmed = value;
                OnPropertyChanged("Confirmed");
            }
        }

        public string FlightNumber { get; set; }

        public string CabineName
        {
            get => _cabineName;
            set
            {
                _cabineName = value;
                OnPropertyChanged("CabineName");
            }
        }

        public string CabinePrice
        {
            get => _cabinePrice;
            set
            {
                CabineName = value;
                _cabinePrice = "$";
                if (value == "Business")
                {
                    _cabinePrice = (EconomyPrice + (EconomyPrice * (decimal)0.35)).ToString();
                }
                else if (value == "First Class")
                {
                    _cabinePrice = (EconomyPrice + (EconomyPrice * (decimal)0.30)).ToString();
                }
                else
                {
                    _cabinePrice = EconomyPrice.ToString();
                }
                OnPropertyChanged("CabinePrice");
            }
        }

        public void SelectAirports()
        {
            WorkWithDatabase.SetSqlCommand($"SELECT IATACode FROM [Airports] WHERE ID = (SELECT DepartureAirportID FROM [Routes] WHERE ID = (SELECT RouteID FROM [Schedules] WHERE ID = {ID}))");
            SqlDataReader sqlDataReader = WorkWithDatabase.SqlCommand.ExecuteReader();
            if (sqlDataReader.Read())
            {
                DepartureAirport = sqlDataReader.GetValue(0).ToString();
            }
            sqlDataReader.Close();
            WorkWithDatabase.SetSqlCommand($"SELECT IATACode FROM [Airports] WHERE ID = (SELECT ArrivalAirportID FROM [Routes] WHERE ID = (SELECT RouteID FROM [Schedules] WHERE ID = {ID}))");
            sqlDataReader = WorkWithDatabase.SqlCommand.ExecuteReader();
            if (sqlDataReader.Read())
            {
                ArrivalAirport = sqlDataReader.GetValue(0).ToString();
            }
            sqlDataReader.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

    }
}
