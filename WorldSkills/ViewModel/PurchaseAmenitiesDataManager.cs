using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Windows;
using WorldSkills.Model;

namespace WorldSkills.ViewModel
{
    class PurchaseAmenitiesDataManager : INotifyPropertyChanged
    {
        private int _countSelectedItems = 0;
        public int CountSelectedItems
        {
            get
            {
                return _countSelectedItems;
            }
            set
            {
                _countSelectedItems = value;
                OnPropertyChanged("CountSelectedItems");
            }
        }

        private float _dutiesAndTaxesSelectedItems = 0;
        public float DutiesAndTaxesSelectedItems
        {
            get
            {
                return _dutiesAndTaxesSelectedItems;
            }
            set
            {
                _dutiesAndTaxesSelectedItems = value;
                OnPropertyChanged("DutiesAndTaxesSelectedItems");
            }
        }

        private float _totalPaybleSelectedItems = 0;
        public float TotalPaybleSelectedItems
        {
            get
            {
                return _totalPaybleSelectedItems;
            }
            set
            {
                _totalPaybleSelectedItems = value;
                OnPropertyChanged("TotalPaybleSelectedItems");
            }
        }

        private Tickets _selectedTicket;
        public Tickets SelectedTicket
        {
            get
            {
                return _selectedTicket;
            }
            set
            {
                _selectedTicket = value;
                OnPropertyChanged("SelectedTicket");
            }
        }

        private ObservableCollection<AircraftsDateAmenities> _aircraftsDateAmenities;
        public ObservableCollection<AircraftsDateAmenities> AircraftsDateAmenities
        {
            get
            {
                return _aircraftsDateAmenities;
            }
            set
            {
                _aircraftsDateAmenities = value;
                OnPropertyChanged("AircraftsDateAmenities");
            }
        }

        private string _selectedTextTicket;
        public string SelectedTextTicket
        {
            get
            {
                return _selectedTextTicket;
            }
            set
            {
                _selectedTextTicket = value;
                string[] ticket = _selectedTextTicket.Split(" ");
                SelectedTicket = new Tickets { ID = int.Parse(ticket[0]), UserID = int.Parse(ticket[1]), ScheduleID = int.Parse(ticket[2]), CabinTypeID = int.Parse(ticket[3]), Firstname = ticket[4], Lastname = ticket[5], Email = ticket[6], Phone = ticket[7], PassportNumber = ticket[8], PassportCountryID = int.Parse(ticket[9]), BookingReference = ticket[10], Confirmed = bool.Parse(ticket[11]) };
                OnPropertyChanged("SelectedTicket");
            }
        }
        private Amenities _selectedAmenities;
        public Amenities SelectedAmenities
        {
            get
            {
                return _selectedAmenities;
            }
            set
            {
                _selectedAmenities = value;
                OnPropertyChanged("SelectedAmenities");
            }
        }
        public ObservableCollection<Amenities> GetAmenities()
        {
            ObservableCollection<Amenities> amenities = new ObservableCollection<Amenities>();
            WorkWithDatabase.SetSqlCommand($"SELECT * FROM [Amenities]");
            SqlDataReader reader = WorkWithDatabase.SqlCommand.ExecuteReader();
            while (reader.Read())
            {
                amenities.Add(new Amenities { ID = int.Parse(reader.GetValue(0).ToString()), Service = reader.GetValue(1).ToString(), Price = float.Parse(reader.GetValue(2).ToString()), Take = false });
            }
            reader.Close();
            AmenitiesTickets = TakeAmentiesTickets();

            for (int i = 0; i < amenities.Count; i++)
            {
                if (amenities[i].Price == 0f)
                {
                    amenities[i].Take = true;
                }
            }
            if (AmenitiesTickets.Count == 0)
            {
                return amenities;
            }
            else
            {
                for (int i = 0; i < AmenitiesTickets.Count; i++)
                {
                    for (int j = 0; j < amenities.Count; j++)
                    {
                        if (amenities[j].ID == AmenitiesTickets[i].AmenityID)
                        {
                            amenities[j].Take = true;
                        }
                    }
                }
                return amenities;
            }
        }
        public ObservableCollection<AmenitiesTickets> TakeAmentiesTickets()
        {
            int ticketId = SelectedTicket.ID;
            ObservableCollection<AmenitiesTickets> amenitiesTickets = new ObservableCollection<AmenitiesTickets>();
            WorkWithDatabase.SetSqlCommand($"SELECT * FROM [AmenitiesTickets] WHERE TicketID = {ticketId}");
            SqlDataReader reader = WorkWithDatabase.SqlCommand.ExecuteReader();
            while (reader.Read())
            {
                amenitiesTickets.Add(new AmenitiesTickets { AmenityID = int.Parse(reader.GetValue(0).ToString()), TicketID = int.Parse(reader.GetValue(1).ToString()), Money = float.Parse(reader.GetValue(2).ToString()) });
            }
            reader.Close();
            return amenitiesTickets;
        }

        private ObservableCollection<AmenitiesTickets> _amenitiesTickets;
        public ObservableCollection<AmenitiesTickets> AmenitiesTickets
        {
            get
            {
                return _amenitiesTickets;
            }
            set
            {
                _amenitiesTickets = value;
                OnPropertyChanged("AmenitiesTickets");
            }
        }

        private ObservableCollection<Amenities> _allAmenities;
        public ObservableCollection<Amenities> AllAmenities
        {
            get
            {
                return _allAmenities;
            }
            set
            {
                _allAmenities = value;
                OnPropertyChanged("AllAmenities");
            }
        }

        private ObservableCollection<string> _comboBoxText;
        public ObservableCollection<string> ComboBoxText
        {
            get
            {
                return _comboBoxText;
            }
            set
            {
                _comboBoxText = value;
                OnPropertyChanged("ComboBoxText");
            }
        }

        private ObservableCollection<Tickets> personTickets;
        public ObservableCollection<Tickets> PersonTickets
        {
            get => personTickets;
            set
            {
                personTickets = value;
                OnPropertyChanged("PersonTickets");
            }
        }

        private Command _selectTickets;
        public Command SelectTickets => _selectTickets ??= new Command(obj =>
        {
            if (obj is string code)
            {
                ObservableCollection<Tickets> personTickets = new ObservableCollection<Tickets>();
                ObservableCollection<string> comboBoxText = new ObservableCollection<string>();
                WorkWithDatabase.SetSqlCommand($"SELECT * FROM [Tickets] WHERE BookingReference = '{code}'");
                SqlDataReader reader = WorkWithDatabase.SqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    personTickets.Add(new Tickets { ID = int.Parse(reader.GetValue(0).ToString()), UserID = int.Parse(reader.GetValue(1).ToString()), ScheduleID = int.Parse(reader.GetValue(2).ToString()), CabinTypeID = int.Parse(reader.GetValue(3).ToString()), Firstname = reader.GetValue(4).ToString(), Lastname = reader.GetValue(5).ToString(), Email = reader.GetValue(6).ToString(), Phone = reader.GetValue(7).ToString(), PassportNumber = reader.GetValue(8).ToString(), PassportCountryID = int.Parse(reader.GetValue(9).ToString()), BookingReference = reader.GetValue(10).ToString(), Confirmed = bool.Parse(reader.GetValue(11).ToString()) });
                    comboBoxText.Add($"{reader.GetValue(0)} {reader.GetValue(1)} {reader.GetValue(2)} {reader.GetValue(3)} {reader.GetValue(4)} {reader.GetValue(5)} {reader.GetValue(6)} {reader.GetValue(7)} {reader.GetValue(8)} {reader.GetValue(9)} {reader.GetValue(10)} {reader.GetValue(11)}");
                }
                ComboBoxText = comboBoxText;
                reader.Close();
                PersonTickets = personTickets;
            }
        });

        private Command _showAmenities;
        public Command ShowAmenities => _showAmenities ??= new Command(obj =>
        {
            if (obj is Tickets ticket)
            {
                AllAmenities = GetAmenities();
                UpdateInformation();
            }
        });

        public void UpdateInformation()
        {
            for (int i = 0; i < AllAmenities.Count; i++)
            {
                if (AllAmenities[i].Take)
                {
                    CountSelectedItems += 1;
                    DutiesAndTaxesSelectedItems += AllAmenities[i].Price * 0.03f;
                    TotalPaybleSelectedItems += AllAmenities[i].Price;
                }
            }

        }

        private Command _updateBuyAmenties;
        public Command UpdateBuyAmenties => _updateBuyAmenties ??= new Command(obj =>
        {
            if (obj is ObservableCollection<Amenities> amenties)
            {
                for (int i = 0; i < amenties.Count; i++)
                {
                    if (amenties[i].Take && amenties[i].Price != 0)
                    {
                        bool isAdd = false;
                        for (int j = 0; j < AmenitiesTickets.Count; j++)
                        {
                            if (AmenitiesTickets[j].AmenityID == amenties[i].ID)
                            {
                                isAdd = true;
                            }
                        }
                        if (!isAdd)
                        {
                            WorkWithDatabase.SqlCommand.CommandText = $"INSERT INTO AmenitiesTickets (AmenityID, TicketID, Price) VALUES ({amenties[i].ID}, {SelectedTicket.ID}, {amenties[i].Price})";
                            int x = WorkWithDatabase.SqlCommand.ExecuteNonQuery();
                            isAdd = false;
                        }
                    }
                    else
                    {
                        for (int j = 0; j < AmenitiesTickets.Count; j++)
                        {
                            if (AmenitiesTickets[j].AmenityID == amenties[i].ID)
                            {
                                WorkWithDatabase.SqlCommand.CommandText = $"DELETE AmenitiesTickets WHERE AmenityID = {amenties[i].ID} AND TicketID = {SelectedTicket.ID}";
                                int x = WorkWithDatabase.SqlCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }
                MessageBox.Show("Информация изменена");
                AmenitiesTickets = TakeAmentiesTickets();
                UpdateInformation();
            }
        });

        private Command _getInformationAboutDay;

        private ObservableCollection<Amenities> TakeAllAmenities()
        {
            ObservableCollection<Amenities> amenities = new ObservableCollection<Amenities>();
            WorkWithDatabase.SetSqlCommand($"SELECT * FROM [Amenities]");
            SqlDataReader reader = WorkWithDatabase.SqlCommand.ExecuteReader();
            while (reader.Read())
            {
                amenities.Add(new Amenities { ID = int.Parse(reader.GetValue(0).ToString()), Service = reader.GetValue(1).ToString(), Price = float.Parse(reader.GetValue(2).ToString()), Take = false });
            }
            reader.Close();
            return amenities;
        }

        public Command GetInformationAboutDay => _getInformationAboutDay ??= new Command(obj =>
        {
            if (obj is string flightID)
            {
                AllAmenities = TakeAllAmenities();
                ObservableCollection<AircraftsDateAmenities> amenities = new ObservableCollection<AircraftsDateAmenities>();
                for (int i = 0; i < AllAmenities.Count; i++)
                {
                    amenities.Add(new AircraftsDateAmenities { ID = AllAmenities[i].ID, NameAmemities = AllAmenities[i].Service });
                }
                WorkWithDatabase.SqlCommand.CommandText = $"SELECT ID FROM Tickets WHERE ScheduleID = {flightID}";
                SqlDataReader sqlDataReader = WorkWithDatabase.SqlCommand.ExecuteReader();
                List<int> countIDTickets = new List<int>();
                while (sqlDataReader.Read())
                {
                    countIDTickets.Add(int.Parse(sqlDataReader.GetValue(0).ToString()));
                }
                sqlDataReader.Close();
                for (int i = 0; i < countIDTickets.Count; i++)
                {
                    WorkWithDatabase.SqlCommand.CommandText = $"SELECT AmenityID FROM AmenitiesTickets WHERE TicketID = {countIDTickets[i]}";
                    sqlDataReader = WorkWithDatabase.SqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        foreach (var amen in amenities)
                        {
                            if (amen.ID == int.Parse(sqlDataReader.GetValue(0).ToString()))
                            {
                                amen.CountAmenities++;
                            }
                        }
                    }
                    sqlDataReader.Close();
                }
                AircraftsDateAmenities = amenities;
            }
        });

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        #endregion

    }
}
