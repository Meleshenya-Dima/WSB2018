using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using WorldSkills.Model;

namespace WorldSkills.ViewModel
{
    class SearchForFlightsDataManager : INotifyPropertyChanged
    {
        public SearchForFlightsDataManager()
        {
            AllAirports = TakeAllAirports();
            CabinTypes = TakeAllCabinTypes();
            AllCountry = TakeAllCountries();
        }


        private ObservableCollection<string> _cabinTypes;
        private ObservableCollection<Schedules> _returnAiports;
        private Airport _selectedAirportsFrom;
        private Airport _selectedAirportsTo;
        private ObservableCollection<Schedules> _outboundAiports;
        private bool _selectedOneWay;
        private ObservableCollection<Airport> _allAirports;
        private ObservableCollection<Countries> _allCountry;
        private int _totalAmount;
        private Schedules _selectReturnAirports;
        private ObservableCollection<Tickets> _allTickets;
        private Command _searchForFlightApply;
        private Command _addTicket;
        private Command _removeTicket;
        private Tickets _selectedTicket;
        private Schedules _selectOutboundAiports;


        public ObservableCollection<Tickets> AllTickets
        {
            get => _allTickets;
            set
            {
                _allTickets = value;
                OnPropertyChanged("AllTickets");
            }
        }
        public ObservableCollection<Countries> AllCountry
        {
            get => _allCountry;
            set
            {
                _allCountry = value;
                OnPropertyChanged("AllCountry");
            }
        }
        public ObservableCollection<string> CabinTypes
        {
            get => _cabinTypes;
            set
            {
                _cabinTypes = value;
                OnPropertyChanged("CabinTypes");
            }
        }
        public Airport SelectedAirportsFrom
        {
            get => _selectedAirportsFrom;
            set
            {
                _selectedAirportsFrom = value;
                OnPropertyChanged("SelectedAirportsFrom");
            }
        }
        public Airport SelectedAirportsTo
        {
            get => _selectedAirportsTo;
            set
            {
                _selectedAirportsTo = value;
                OnPropertyChanged("SelectedAirportsTo");
            }
        }
        public Schedules SelectReturnAirports
        {
            get => _selectReturnAirports;
            set
            {
                _selectReturnAirports = value;
                OnPropertyChanged("SelectReturnAirports");
            }
        }
        public Schedules SelectOutboundAiports
        {
            get => _selectOutboundAiports;
            set
            {
                _selectOutboundAiports = value;
                OnPropertyChanged("SelectOutboundAiports");
            }
        }
        public int TotalAmount
        {
            get => _totalAmount;
            set
            {
                _totalAmount = value;
                OnPropertyChanged("TotalAmount");
            }
        }
        public ObservableCollection<Schedules> ReturnAirports
        {
            get => _returnAiports;
            set
            {
                _returnAiports = value;
                OnPropertyChanged("ReturnAirports");
            }
        }
        public ObservableCollection<Schedules> OutboundAiports
        {
            get => _outboundAiports;
            set
            {
                _outboundAiports = value;
                OnPropertyChanged("OutboundAiports");
            }
        }
        public bool SelectedOneWay
        {
            get => _selectedOneWay;
            set
            {
                _selectedOneWay = value;
                OnPropertyChanged("SelectedOneWay");
            }
        }
        public ObservableCollection<Airport> AllAirports
        {
            get => _allAirports;
            set
            {
                _allAirports = value;
                OnPropertyChanged("AllAirports");
            }
        }
        public Tickets SelectedTicket
        {
            get => _selectedTicket;
            set
            {
                _selectedTicket = value;
                OnPropertyChanged("SelectedTicket");
            }
        }


        public ObservableCollection<Countries> TakeAllCountries()
        {
            ObservableCollection<Countries> countries = new();
            WorkWithDatabase.SetSqlCommand("SELECT * FROM [Countries]");
            SqlDataReader sqlDataReader = WorkWithDatabase.SqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                countries.Add(new Countries { ID = int.Parse(sqlDataReader.GetValue(0).ToString()), Name = sqlDataReader.GetValue(1).ToString() });
            }
            sqlDataReader.Close();
            return countries;
        }
        public ObservableCollection<Airport> TakeAllAirports()
        {
            ObservableCollection<Airport> airports = new ObservableCollection<Airport>();
            WorkWithDatabase.SetSqlCommand("SELECT * FROM [Airports]");
            SqlDataReader sqlDataReader = WorkWithDatabase.SqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                airports.Add(new Airport { ID = int.Parse(sqlDataReader.GetValue(0).ToString()), CountryID = int.Parse(sqlDataReader.GetValue(1).ToString()), IATACode = sqlDataReader.GetValue(2).ToString(), Name = sqlDataReader.GetValue(3).ToString() });
            }
            sqlDataReader.Close();
            return airports;
        }
        public ObservableCollection<string> TakeAllCabinTypes()
        {
            ObservableCollection<string> cabinTypes = new ObservableCollection<string>();
            WorkWithDatabase.SetSqlCommand("SELECT * FROM [CabinTypes]");
            SqlDataReader sqlDataReader = WorkWithDatabase.SqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                cabinTypes.Add(sqlDataReader.GetValue(1).ToString());
            }
            sqlDataReader.Close();
            return cabinTypes;
        }


        public Command AddTicket => _addTicket ??= new Command(obj =>
        {
            if (obj is Tickets tickets)
            {
                AllTickets.Add(tickets);
            }
        });

        public Command RemoveTicket => _removeTicket ??= new Command(obj =>
        {
            if (obj is Tickets tickets)
            {
                AllTickets.Remove(tickets);
            }
        });

        public Command SearchForFlightApply => _searchForFlightApply ??= new Command(obj =>
        {
            if (obj is SearchFlightValue searchFlight)
            {
                SqlDataReader sqlDataReader = null;
                ObservableCollection<Schedules> outboundSchedules = new ObservableCollection<Schedules>();
                if (searchFlight.ThreeDaysOutbound)
                {
                    DateTime dateTime = DateTime.ParseExact(searchFlight.Outbound, "MM/dd/yyyy", null);
                    WorkWithDatabase.SetSqlCommand($"SELECT * FROM [Schedules] WHERE Date BETWEEN '{dateTime.AddDays(-3).ToString("yyyy/MM/dd")}' AND '{dateTime.AddDays(3).ToString("yyyy/MM/dd")}' AND RouteID = (SELECT ID FROM Routes WHERE DepartureAirportID = (SELECT ID FROM Airports WHERE IATACode = '{searchFlight.From}') AND ArrivalAirportID = (SELECT ID FROM Airports WHERE IATACode = '{searchFlight.To}'))");
                    sqlDataReader = WorkWithDatabase.SqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        outboundSchedules.Add(new Schedules { ID = int.Parse(sqlDataReader.GetValue(0).ToString()), Data = DateTime.Parse(sqlDataReader.GetValue(1).ToString()), Time = DateTime.Parse(sqlDataReader.GetValue(2).ToString()), AircraftID = int.Parse(sqlDataReader.GetValue(3).ToString()), RouteID = int.Parse(sqlDataReader.GetValue(4).ToString()), EconomyPrice = decimal.Parse(sqlDataReader.GetValue(5).ToString()), BusinessPrice = decimal.Parse(sqlDataReader.GetValue(6).ToString()), FirstClassPrice = decimal.Parse(sqlDataReader.GetValue(7).ToString()), Confirmed = bool.Parse(sqlDataReader.GetValue(8).ToString()), FlightNumber = sqlDataReader.GetValue(9).ToString(), DepartureAirport = searchFlight.From, ArrivalAirport = searchFlight.To, CabinePrice = searchFlight.CabinType });
                    }
                    sqlDataReader.Close();
                }
                else
                {
                    WorkWithDatabase.SetSqlCommand($"SELECT * FROM [Schedules] WHERE Date = '{searchFlight.Outbound}' AND RouteID = (SELECT ID FROM Routes WHERE DepartureAirportID = (SELECT ID FROM Airports WHERE IATACode = '{searchFlight.From}') AND ArrivalAirportID = (SELECT ID FROM Airports WHERE IATACode = '{searchFlight.To}'))");
                    sqlDataReader = WorkWithDatabase.SqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        outboundSchedules.Add(new Schedules { ID = int.Parse(sqlDataReader.GetValue(0).ToString()), Data = DateTime.Parse(sqlDataReader.GetValue(1).ToString()), Time = DateTime.Parse(sqlDataReader.GetValue(2).ToString()), AircraftID = int.Parse(sqlDataReader.GetValue(3).ToString()), RouteID = int.Parse(sqlDataReader.GetValue(4).ToString()), EconomyPrice = decimal.Parse(sqlDataReader.GetValue(5).ToString()), BusinessPrice = decimal.Parse(sqlDataReader.GetValue(6).ToString()), FirstClassPrice = decimal.Parse(sqlDataReader.GetValue(7).ToString()), Confirmed = bool.Parse(sqlDataReader.GetValue(8).ToString()), FlightNumber = sqlDataReader.GetValue(9).ToString(), DepartureAirport = searchFlight.From, ArrivalAirport = searchFlight.To, CabinePrice = searchFlight.CabinType });
                    }
                    sqlDataReader.Close();
                }
                ObservableCollection<Schedules> returnSchedules = new ObservableCollection<Schedules>();
                if (!string.IsNullOrEmpty(searchFlight.Return))
                {
                    if (searchFlight.ThreeDaysReturn)
                    {
                        DateTime dateTime = DateTime.ParseExact(searchFlight.Return, "MM/dd/yyyy", null);
                        WorkWithDatabase.SetSqlCommand($"SELECT * FROM [Schedules] WHERE Date BETWEEN '{dateTime.ToString("yyyy-MM-dd")}' AND '{dateTime.AddDays(3).ToString("yyyy-MM-dd")}' AND RouteID = (SELECT ID FROM Routes WHERE DepartureAirportID = (SELECT ID FROM Airports WHERE IATACode = '{searchFlight.To}') AND ArrivalAirportID = (SELECT ID FROM Airports WHERE IATACode = '{searchFlight.From}'))");
                        sqlDataReader = WorkWithDatabase.SqlCommand.ExecuteReader();
                        while (sqlDataReader.Read())
                        {
                            returnSchedules.Add(new Schedules { ID = int.Parse(sqlDataReader.GetValue(0).ToString()), Data = DateTime.Parse(sqlDataReader.GetValue(1).ToString()), Time = DateTime.Parse(sqlDataReader.GetValue(2).ToString()), AircraftID = int.Parse(sqlDataReader.GetValue(3).ToString()), RouteID = int.Parse(sqlDataReader.GetValue(4).ToString()), EconomyPrice = decimal.Parse(sqlDataReader.GetValue(5).ToString()), BusinessPrice = decimal.Parse(sqlDataReader.GetValue(6).ToString()), FirstClassPrice = decimal.Parse(sqlDataReader.GetValue(7).ToString()), Confirmed = bool.Parse(sqlDataReader.GetValue(8).ToString()), FlightNumber = sqlDataReader.GetValue(9).ToString(), DepartureAirport = searchFlight.To, ArrivalAirport = searchFlight.From, CabinePrice = searchFlight.CabinType });
                        }
                        sqlDataReader.Close();
                    }
                    else
                    {
                        WorkWithDatabase.SetSqlCommand($"SELECT * FROM [Schedules] WHERE Date = '{searchFlight.Return}' AND RouteID = (SELECT ID FROM Routes WHERE ArrivalAirportID = (SELECT ID FROM Airports WHERE IATACode = '{searchFlight.From}') AND DepartureAirportID = (SELECT ID FROM Airports WHERE IATACode = '{searchFlight.To}'))");
                        sqlDataReader = WorkWithDatabase.SqlCommand.ExecuteReader();
                        while (sqlDataReader.Read())
                        {
                            returnSchedules.Add(new Schedules { ID = int.Parse(sqlDataReader.GetValue(0).ToString()), Data = DateTime.Parse(sqlDataReader.GetValue(1).ToString()), Time = DateTime.Parse(sqlDataReader.GetValue(2).ToString()), AircraftID = int.Parse(sqlDataReader.GetValue(3).ToString()), RouteID = int.Parse(sqlDataReader.GetValue(4).ToString()), EconomyPrice = decimal.Parse(sqlDataReader.GetValue(5).ToString()), BusinessPrice = decimal.Parse(sqlDataReader.GetValue(6).ToString()), FirstClassPrice = decimal.Parse(sqlDataReader.GetValue(7).ToString()), Confirmed = bool.Parse(sqlDataReader.GetValue(8).ToString()), FlightNumber = sqlDataReader.GetValue(9).ToString(), DepartureAirport = searchFlight.To, ArrivalAirport = searchFlight.From, CabinePrice = searchFlight.CabinType });
                        }
                        sqlDataReader.Close();
                    }
                }
                OutboundAiports = outboundSchedules;
                ReturnAirports = returnSchedules;
            }
        });

        #region
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        #endregion
    }
}
