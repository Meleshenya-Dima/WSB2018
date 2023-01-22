using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WorldSkills.Model
{
    class Tracking : INotifyPropertyChanged
    {

        private string _unsuccessfulLogoutReason;
        private bool _isException;
        public int ID { get; set; }

        public string Date { get; set; }

        public string LoginTime { get; set; }

        public string LogoutTime { get; set; }

        public string TimeSpentOnSystem { get; set; }

        public string UnsuccessfulLogoutReason {
            get => _unsuccessfulLogoutReason;
            set
            {
                if (value != "")
                {
                    _isException = true;
                }
                _unsuccessfulLogoutReason = value;
            }
        }

        public bool IsException
        {
            get => _isException;
            set
            {
                _isException = value;
                OnPropertyChanged("IsExeption");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

    }
}
