using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WorldSkills.Model
{
    public class User : INotifyPropertyChanged
    {
        private bool _active;
        private int _roleID;
        private int _age;
        private DateTime _birthdate;
        private string _roleIDText;
        private int _officeID;
        private string _officeIDText;

        public int RoleID
        {
            get => _roleID;
            set
            {
                _roleID = value;
                if (_roleID == 1)
                {
                    RoleIDText = "Administrator";
                }
                else if (_roleID == 2)
                {
                    RoleIDText = "User";
                }
                OnPropertyChanged("RoleID");
            }
        }

        public int ID { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string OfficeIDText
        {
            get => _officeIDText;
            set
            {
                _officeIDText = value;
                OnPropertyChanged("OfficeIDText");
            }
        }

        public int OfficeID
        {
            get => _officeID;
            set
            {
                _officeID = value;
                OnPropertyChanged("OfficeID");
            }
        }

        public string RoleIDText
        {
            get => _roleIDText;
            set
            {
                _roleIDText = value;
                OnPropertyChanged("RoleIDText");
            }
        }

        public DateTime Birthdate
        {
            get => _birthdate;
            set
            {
                _birthdate = value;
                Age = DateTime.Now.Year - Birthdate.Year;
                OnPropertyChanged("Birthdate");
            }
        }

        public bool Active
        {
            get => _active;
            set
            {
                _active = value;
                OnPropertyChanged("Active");
            }
        }

        public int Age
        {
            get => _age;
            set
            {
                _age = value;
                OnPropertyChanged("Age");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
