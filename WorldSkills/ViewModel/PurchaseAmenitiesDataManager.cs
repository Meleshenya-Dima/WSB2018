using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using WorldSkills.Model;

namespace WorldSkills.ViewModel
{
    class PurchaseAmenitiesDataManager : INotifyPropertyChanged
    {

        public PurchaseAmenitiesDataManager()
        {
            AllAmenities = GetAmenities();
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

        public ObservableCollection<Amenities> GetAmenities()
        {
            ObservableCollection<Amenities> amenities = new ObservableCollection<Amenities>();
            WorkWithDatabase.SetSqlCommand($"SELECT * FROM [Amenities]");
            SqlDataReader reader = WorkWithDatabase.SqlCommand.ExecuteReader();
            while (reader.Read())
            {
                amenities.Add(new Amenities { ID = int.Parse(reader.GetValue(0).ToString()), Service = reader.GetValue(1).ToString(), Price = float.Parse(reader.GetValue(0).ToString()) });
            }
            reader.Close();
            return amenities;
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


        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        #endregion

    }
}
