using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using WorldSkills.Model;

namespace WorldSkills.ViewModel
{
    class ManageFlightSchedulesDataManager : INotifyPropertyChanged
    {

        public ManageFlightSchedulesDataManager()
        {
            AllAirport = TakeAllAirport();
            Schedules = TakeAllSchedules();
        }

        public ObservableCollection<Airport> AllAirport { get; set; }

        public ObservableCollection<Airport> TakeAllAirport()
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


        private ObservableCollection<Schedules> _schedules;
        public ObservableCollection<Schedules> Schedules
        {
            get => _schedules;
            set
            {
                _schedules = value;
                OnPropertyChanged("Schedules");
            }
        }

        private int _successfulChangesApplied = 0;
        public int SuccessfulChangesApplied
        {
            get
            {
                return _successfulChangesApplied;
            }
            set
            {
                _successfulChangesApplied++;
                OnPropertyChanged("SuccessfulChangesApplied");
            }
        }

        private int _duplicateRecordsDiscarded = 0;
        public int DuplicateRecordsDiscarded
        {
            get
            {
                return _duplicateRecordsDiscarded;
            }
            set
            {
                _duplicateRecordsDiscarded++;
                OnPropertyChanged("DuplicateRecordsDiscarded");
            }
        }

        private int _recordwithmissingfliedsdiscarded = 0;
        public int Recordwithmissingfliedsdiscarded
        {
            get
            {
                return _recordwithmissingfliedsdiscarded;
            }
            set
            {
                _recordwithmissingfliedsdiscarded++;
                OnPropertyChanged("Recordwithmissingfliedsdiscarded");
            }
        }


        public ObservableCollection<Schedules> TakeAllSchedules()
        {
            ObservableCollection<Schedules> schedules = new();
            WorkWithDatabase.SetSqlCommand("SELECT * FROM [Schedules]");
            SqlDataReader sqlDataReader = WorkWithDatabase.SqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                schedules.Add(new Schedules { ID = int.Parse(sqlDataReader.GetValue(0).ToString()), Data = DateTime.Parse(sqlDataReader.GetValue(1).ToString()), Time = DateTime.Parse(sqlDataReader.GetValue(2).ToString()), AircraftID = int.Parse(sqlDataReader.GetValue(3).ToString()), RouteID = int.Parse(sqlDataReader.GetValue(4).ToString()), EconomyPrice = decimal.Parse(sqlDataReader.GetValue(5).ToString()), BusinessPrice = decimal.Parse(sqlDataReader.GetValue(6).ToString()), FirstClassPrice = decimal.Parse(sqlDataReader.GetValue(7).ToString()), Confirmed = bool.Parse(sqlDataReader.GetValue(8).ToString()), FlightNumber = sqlDataReader.GetValue(9).ToString() });
            }
            sqlDataReader.Close();
            foreach (var item in schedules)
            {
                item.SelectAirports();
            }
            return schedules;
        }


        private Command _enableDisableConfirmed;

        private Schedules _selectedSchedules;

        private Command _searchInformationSchedules;
        public Schedules SelectedSchedules
        {
            get => _selectedSchedules;
            set
            {
                _selectedSchedules = value;
                OnPropertyChanged("SelectedSchedules");
            }
        }
        public Command EnableDisableConfirmed => _enableDisableConfirmed ??= new Command(obj =>
        {
            if (obj is Schedules schedules)
            {
                string data = schedules.Data.ToString("yyyy/MM/dd").Replace(".", "-");
                var time = schedules.Time.TimeOfDay;
                if (schedules.Confirmed)
                {
                    WorkWithDatabase.SetSqlCommand($"UPDATE Schedules SET Confirmed = {0} WHERE Date = '{data}' AND Time = '{time}' AND AircraftID = {schedules.AircraftID} AND RouteID = {schedules.RouteID}");
                    WorkWithDatabase.SqlCommand.ExecuteNonQuery();
                }
                else
                {
                    WorkWithDatabase.SetSqlCommand($"UPDATE Schedules SET Confirmed = {1} WHERE Date = '{data}' AND Time = '{time}' AND AircraftID = {schedules.AircraftID} AND RouteID = {schedules.RouteID}");
                    WorkWithDatabase.SqlCommand.ExecuteNonQuery();
                }
                schedules.Confirmed = !schedules.Confirmed;
            }
        });

        private Command _updateSchedules;
        public Command UpdateSchedules => _updateSchedules ??= new Command(obj =>
        {
            if (obj is Schedules schedules)
            {
                if (!string.IsNullOrEmpty(schedules.Data.ToString()))
                {
                    SelectedSchedules.Data = schedules.Data;
                }
                if (!string.IsNullOrEmpty(schedules.Time.ToString()))
                {
                    SelectedSchedules.Time = schedules.Time;
                }
                if (!string.IsNullOrEmpty(schedules.EconomyPrice.ToString()))
                {
                    SelectedSchedules.EconomyPrice = schedules.EconomyPrice;
                }
            }
        });

        private Command _updateTextSchedules;
        public Command UpdateTextSchedules => _updateTextSchedules ??= new Command(obj =>
        {
            if (obj is string URLString)
            {
                _duplicateRecordsDiscarded = 0;
                _recordwithmissingfliedsdiscarded = 0;
                _successfulChangesApplied = 0;
                List<string> AddInformation = new List<string>();
                string[] updateText;
                using (StreamReader sw = new StreamReader(URLString))
                {
                    updateText = sw.ReadToEnd().Split("/");
                }
                foreach (var Text in updateText)
                {
                    if (Text.Split(" ").Length == 9)
                    {
                        if (!AddInformation.Contains(Text))
                        {
                            AddInformation.Add(Text);
                        }
                        else
                        {
                            DuplicateRecordsDiscarded += 1;
                        }
                    }
                    else
                    {
                        Recordwithmissingfliedsdiscarded += 1;
                    }
                }

                foreach (var newschedules in AddInformation)
                {
                    string[] schedulesInformation = newschedules.Split(" ");
                    DateTime dateTime = DateTime.ParseExact(schedulesInformation[0], "MM/dd/yyyy", null);
                    WorkWithDatabase.SetSqlCommand($"INSERT INTO [Schedules] VALUES ('{schedulesInformation[0]}', '{schedulesInformation[1]}', {int.Parse(schedulesInformation[2])}, {int.Parse(schedulesInformation[3])}, {float.Parse(schedulesInformation[4])}, {float.Parse(schedulesInformation[5])}, {float.Parse(schedulesInformation[6])},'{bool.Parse(schedulesInformation[7])}', {int.Parse(schedulesInformation[8])})");
                    WorkWithDatabase.SqlCommand.ExecuteNonQuery();
                }
            }
        });

        public Command SearchInformationSchedules => _searchInformationSchedules ??= new Command(obj =>
        {

            if (obj is FilterValues information)
            {
                ObservableCollection<Schedules> schedules = new ObservableCollection<Schedules>();
                int DepartureAirport = 0;
                int ArrivalAirport = 0;
                foreach (var item in AllAirport)
                {
                    if (item.Name == information.From)
                    {
                        DepartureAirport = item.ID;
                    }
                    else if (item.Name == information.To)
                    {
                        ArrivalAirport = item.ID;
                    }
                }

                if (!string.IsNullOrEmpty(information.From) && !string.IsNullOrEmpty(information.To))
                {
                    WorkWithDatabase.SetSqlCommand($"SELECT * FROM [Schedules] WHERE RouteID = (SELECT ID FROM [Routes] WHERE DepartureAirportID = {DepartureAirport} AND ArrivalAirportID = {ArrivalAirport})");
                }
                else
                {
                    WorkWithDatabase.SetSqlCommand($"SELECT * FROM [Schedules] WHERE FlightNumber = {information.FlightNumber} AND Date = '{information.Outbound}'");
                }

                SqlDataReader sqlDataReader = WorkWithDatabase.SqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    schedules.Add(new Schedules { ID = int.Parse(sqlDataReader.GetValue(0).ToString()), Data = DateTime.Parse(sqlDataReader.GetValue(1).ToString()), Time = DateTime.Parse(sqlDataReader.GetValue(2).ToString()), AircraftID = int.Parse(sqlDataReader.GetValue(3).ToString()), RouteID = int.Parse(sqlDataReader.GetValue(4).ToString()), EconomyPrice = decimal.Parse(sqlDataReader.GetValue(5).ToString()), BusinessPrice = decimal.Parse(sqlDataReader.GetValue(6).ToString()), FirstClassPrice = decimal.Parse(sqlDataReader.GetValue(7).ToString()), Confirmed = bool.Parse(sqlDataReader.GetValue(8).ToString()), FlightNumber = sqlDataReader.GetValue(9).ToString() });
                }
                IEnumerable<Schedules> sortedVaue;
                ObservableCollection<Schedules> obs;
                sqlDataReader.Close();
                if (information.SortValue == "Economy Price")
                {
                    sortedVaue = schedules.OrderBy(i => i.EconomyPrice).AsEnumerable();
                    obs = new ObservableCollection<Schedules>(sortedVaue);
                    foreach (var item in obs)
                    {
                        item.SelectAirports();
                    }
                    Schedules = obs;
                }
                else if (information.SortValue == "Confirmed")
                {
                    sortedVaue = schedules.OrderBy(i => i.Confirmed).AsEnumerable();
                    obs = new ObservableCollection<Schedules>(sortedVaue);
                    foreach (var item in obs)
                    {
                        item.SelectAirports();
                    }
                    Schedules = obs;
                }
                else
                {
                    sortedVaue = schedules.OrderBy(i => i.Data).AsEnumerable();
                    obs = new ObservableCollection<Schedules>(sortedVaue);
                    foreach (var item in obs)
                    {
                        item.SelectAirports();
                    }
                    Schedules = obs;
                }
            }
        });


        #region INotifyPropertyChanged Members implementation 

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        #endregion
    }
}
