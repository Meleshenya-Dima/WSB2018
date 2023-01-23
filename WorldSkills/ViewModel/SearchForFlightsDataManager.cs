using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using WorldSkills.Model;
using WorldSkills.Page;

namespace WorldSkills.ViewModel
{
    class SearchForFlightsDataManager : INotifyPropertyChanged
    {
        public SearchForFlightsDataManager()
        {
            AllAirports = TakeAllAirports();
            CabinTypes = TakeAllCabinTypes();
            AllCountry = TakeAllCountries();
            AllTickets = TakeAllTickets();
            AllAircrafts = TakeAircrafts();
        }

        private ObservableCollection<Aircrafts> _aircrafts;
        private ObservableCollection<string> _cabinTypes;
        private ObservableCollection<Schedules> _returnAiports;
        private Airport _selectedAirportsFrom;
        private Airport _selectedAirportsTo;
        private ObservableCollection<Schedules> _outboundAiports;
        private bool _selectedOneWay;
        private ObservableCollection<Airport> _allAirports;
        private ObservableCollection<Countries> _allCountry;
        private float _totalAmount;
        private Schedules _selectReturnAirports;
        private ObservableCollection<Tickets> _newTickets;
        private ObservableCollection<Tickets> _allTickets;
        private Command _searchForFlightApply;
        private Command _addTicket;
        private Command _removeTicket;
        private Tickets _selectedTicket;
        private Schedules _selectOutboundAiports;
        private string _generationCode;
        private Command _generationCodeCommand;
        private static int _selectOutboundAiportsCountTicket;
        private static int _selectReturnAirportsCountTicket;
        public static int CountTicket;

        public static int SelectReturnAirportsCountTicket
        {
            get
            {
                return _selectReturnAirportsCountTicket;
            }
            set
            {
                _selectReturnAirportsCountTicket = value;
            }
        }

        public static int SelectOutboundAiportscountTicket
        {
            get
            {
                return _selectOutboundAiportsCountTicket;
            }
            set
            {
                _selectOutboundAiportsCountTicket = value;
            }
        }

        public ObservableCollection<Aircrafts> AllAircrafts
        {
            get
            {
                return _aircrafts;
            }
            set
            {
                _aircrafts = value;
                OnPropertyChanged("Aircrafts");
            }
        }

        public ObservableCollection<Tickets> AllTickets
        {
            get
            {
                if (_allTickets is null)
                {
                    _allTickets = new ObservableCollection<Tickets>();
                }
                return _allTickets;
            }
            set
            {
                _allTickets = value;
                OnPropertyChanged("AllTickets");
            }
        }
        public ObservableCollection<Tickets> NewTickets
        {
            get
            {
                if (_newTickets is null)
                {
                    _newTickets = new ObservableCollection<Tickets>();
                }
                return _newTickets;
            }
            set
            {

                _newTickets = value;
                OnPropertyChanged("NewTickets");
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
                int index = 0;
                _selectReturnAirports = value;
                OnPropertyChanged("SelectReturnAirports");
                for (int i = 0; i < CabinTypes.Count; i++)
                {
                    if (CabinTypes[i] == SelectReturnAirports.CabineName)
                    {
                        index = i + 1;
                        break;
                    }
                }
                WorkWithDatabase.SetSqlCommand($"SELECT COUNT(*) FROM Tickets WHERE ScheduleID = {SelectReturnAirports.ID} AND CabinTypeID = {index}");
                SqlDataReader countTikets = WorkWithDatabase.SqlCommand.ExecuteReader();
                if (countTikets.Read())
                {
                    int thisCountTickets = int.Parse(countTikets.GetValue(0).ToString());
                    if (SelectReturnAirports.CabineName == "First Class")
                    {
                        for (int i = 0; i < AllAircrafts.Count; i++)
                        {
                            int x = AllAircrafts[i].TotalSeats - (thisCountTickets + AllAircrafts[i].BusinessSeats + AllAircrafts[i].EconomySeats);
                            SelectReturnAirportsCountTicket = x;

                        }
                    }
                    else if (SelectReturnAirports.CabineName == "Business")
                    {
                        for (int i = 0; i < AllAircrafts.Count; i++)
                        {
                            if (SelectReturnAirports.AircraftID == AllAircrafts[i].ID)
                            {
                                SelectReturnAirportsCountTicket = AllAircrafts[i].BusinessSeats - thisCountTickets;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < AllAircrafts.Count; i++)
                        {
                            if (SelectReturnAirports.AircraftID == AllAircrafts[i].ID)
                            {
                                SelectReturnAirportsCountTicket = AllAircrafts[i].EconomySeats - thisCountTickets;
                            }
                        }
                    }
                }
                else
                {
                    SelectReturnAirportsCountTicket = 161;
                }
                countTikets.Close();
            }
        }
        public Schedules SelectOutboundAiports
        {
            get => _selectOutboundAiports;
            set
            {
                int index = 0;
                _selectOutboundAiports = value;
                OnPropertyChanged("SelectOutboundAiports");
                for (int i = 0; i < CabinTypes.Count; i++)
                {
                    if (CabinTypes[i] == SelectOutboundAiports.CabineName)
                    {
                        index = i + 1;
                        break;
                    }
                }
                WorkWithDatabase.SetSqlCommand($"SELECT COUNT(*) FROM Tickets WHERE ScheduleID = {SelectOutboundAiports.ID} AND CabinTypeID = {index}");
                SqlDataReader countTikets = WorkWithDatabase.SqlCommand.ExecuteReader();
                if (countTikets.Read())
                {
                    int thisCountTickets = int.Parse(countTikets.GetValue(0).ToString());
                    if (SelectOutboundAiports.CabineName == "First Class")
                    {
                        for (int i = 0; i < AllAircrafts.Count; i++)
                        {
                            int x = AllAircrafts[i].TotalSeats - (thisCountTickets + AllAircrafts[i].BusinessSeats + AllAircrafts[i].EconomySeats);
                            SelectOutboundAiportscountTicket = x;

                        }
                    }
                    else if (SelectOutboundAiports.CabineName == "Business")
                    {
                        for (int i = 0; i < AllAircrafts.Count; i++)
                        {
                            if (SelectOutboundAiports.AircraftID == AllAircrafts[i].ID)
                            {
                                SelectOutboundAiportscountTicket = AllAircrafts[i].BusinessSeats - thisCountTickets;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < AllAircrafts.Count; i++)
                        {
                            if (SelectOutboundAiports.AircraftID == AllAircrafts[i].ID)
                            {
                                SelectOutboundAiportscountTicket = AllAircrafts[i].EconomySeats - thisCountTickets;
                            }
                        }
                    }
                }
                else
                {
                    SelectReturnAirportsCountTicket = 161;
                }
                countTikets.Close();
            }
        }




        public float TotalAmount
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
        public string GenerationCode
        {
            get
            {
                return _generationCode;
            }
            set
            {
                _generationCode = value;
                OnPropertyChanged("GenerationCode");
            }
        }

        public ObservableCollection<Tickets> TakeAllTickets()
        {
            ObservableCollection<Tickets> tickets = new();
            WorkWithDatabase.SetSqlCommand("SELECT * FROM [Tickets]");
            SqlDataReader sqlDataReader = WorkWithDatabase.SqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                tickets.Add(new Tickets { ID = int.Parse(sqlDataReader.GetValue(0).ToString()), UserID = int.Parse(sqlDataReader.GetValue(1).ToString()), ScheduleID = int.Parse(sqlDataReader.GetValue(2).ToString()), CabinTypeID = int.Parse(sqlDataReader.GetValue(3).ToString()), Firstname = sqlDataReader.GetValue(4).ToString(), Lastname = sqlDataReader.GetValue(5).ToString(), Email = sqlDataReader.GetValue(6).ToString(), Phone = sqlDataReader.GetValue(7).ToString(), PassportNumber = sqlDataReader.GetValue(8).ToString(), PassportCountryID = int.Parse(sqlDataReader.GetValue(9).ToString()), BookingReference = sqlDataReader.GetValue(10).ToString(), Confirmed = bool.Parse(sqlDataReader.GetValue(11).ToString()) });
            }
            sqlDataReader.Close();
            return tickets;

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

        public ObservableCollection<Aircrafts> TakeAircrafts()
        {
            ObservableCollection<Aircrafts> aircrafts = new ObservableCollection<Aircrafts>();
            WorkWithDatabase.SetSqlCommand("SELECT * FROM [Aircrafts]");
            SqlDataReader sqlDataReader = WorkWithDatabase.SqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                aircrafts.Add(new Aircrafts { ID = int.Parse(sqlDataReader.GetValue(0).ToString()), Name = sqlDataReader.GetValue(1).ToString(), MakeModel = sqlDataReader.GetValue(2).ToString(), TotalSeats = int.Parse(sqlDataReader.GetValue(3).ToString()), EconomySeats = int.Parse(sqlDataReader.GetValue(4).ToString()), BusinessSeats = int.Parse(sqlDataReader.GetValue(5).ToString()) });
            }
            sqlDataReader.Close();
            return aircrafts;
        }

        public Command AddTicket => _addTicket ??= new Command(obj =>
        {
            if (obj is Tickets tickets)
            {
                tickets.UserID = DefaultUserMainMenu.User.ID;
                if (NewTickets.Count <= CountTicket)
                {
                    foreach (var Tickets in AllCountry)
                    {
                        if (tickets.PassportCountryName == Tickets.Name)
                        {
                            for (int i = 0; i < CabinTypes.Count; i++)
                            {
                                if (SelectOutboundAiports.CabineName == CabinTypes[i])
                                {
                                    tickets.CabinTypeID = i;
                                }
                            }
                            tickets.PassportCountryID = Tickets.ID;
                            float sum = float.Parse(SelectOutboundAiports.CabinePrice);
                            if (!SelectedOneWay)
                            {
                                tickets.ScheduleID = SelectOutboundAiports.ID;
                                _totalAmount += sum;
                                NewTickets.Add(tickets);
                            }
                            else
                            {
                                tickets.ScheduleID = SelectOutboundAiports.ID;
                                NewTickets.Add(tickets);
                                tickets.ScheduleID = SelectReturnAirports.ID;
                                NewTickets.Add(tickets);
                                _totalAmount += sum * 2;
                            }
                            break;
                        }
                    }
                }
            }
        });

        public Command RemoveTicket => _removeTicket ??= new Command(obj =>
        {
            if (obj is Tickets tickets)
            {
                if (SelectedOneWay)
                {
                    _totalAmount -= float.Parse(SelectOutboundAiports.CabinePrice);
                }
                else
                {
                    _totalAmount -= float.Parse(SelectOutboundAiports.CabinePrice) * 2;
                }
                NewTickets.Remove(tickets);
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

        public Command GenerationCodeCommand => _generationCodeCommand ??= new Command(obj =>
        {
            GenerationCode = DataProtection.GenerationNumber();
            foreach (Tickets ticket in NewTickets)
            {
                ticket.BookingReference = GenerationCode;
                WorkWithDatabase.SetSqlCommand($"INSERT INTO [Tickets] (UserID, ScheduleID, CabinTypeID, Firstname, Lastname, Email, Phone, PassportNumber, PassportCountryID, BookingReference, Confirmed) VALUES ({ticket.UserID}, {ticket.ScheduleID}, {ticket.CabinTypeID + 1}, '{ticket.Firstname}', '{ticket.Lastname}', '{ticket.Email}', '{ticket.Phone}', '{ticket.PassportNumber}', {ticket.PassportCountryID}, '{ticket.BookingReference}', '{ticket.Confirmed}' )");
                WorkWithDatabase.SqlCommand.ExecuteNonQuery();
            }
        });



        #region
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        #endregion
    }
}
