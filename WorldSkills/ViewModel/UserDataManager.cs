using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Threading;
using WorldSkills.Model;

namespace WorldSkills.ViewModel
{
    class UserDataManager : INotifyPropertyChanged
    {
        private string ExceptionText = "";
        private DispatcherTimer _dispatcherTimer;
        private User _user;

        public UserDataManager(User user)
        {
            _startTracking = DateTime.Now;
            _user = user;
            Tracking = TakeAllTracking();
            IntroductionText = $"Hi {user.FirstName}, Welcome to BELAVIA Airlines Automation System";
            _dateTime = DateTime.Today;
            _dispatcherTimer = new();
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            _dispatcherTimer.Tick += DispatcherTimerTick;
            _dispatcherTimer.Start();
        }

        private string _introductionText;

        public string IntroductionText
        {
            get => _introductionText;
            set
            {
                _introductionText = value;
                OnPropertyChanged("IntroductionText");
            }
        }

        public ObservableCollection<Tracking> Tracking { get; set; }

        public ObservableCollection<Tracking> TakeAllTracking()
        {
            try
            {
                ObservableCollection<Tracking> trackings = new ObservableCollection<Tracking>();
                WorkWithDatabase.SetSqlCommand("SELECT * FROM [Tracking]");
                SqlDataReader sqlDataReader = WorkWithDatabase.SqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    trackings.Add(new Tracking { ID = int.Parse(sqlDataReader.GetValue(0).ToString()), Date = sqlDataReader.GetValue(1).ToString(), LoginTime = sqlDataReader.GetValue(2).ToString(), LogoutTime = sqlDataReader.GetValue(3).ToString(), TimeSpentOnSystem = sqlDataReader.GetValue(4).ToString(), UnsuccessfulLogoutReason = sqlDataReader.GetValue(5).ToString() });
                }
                sqlDataReader.Close();
                return trackings;
            }
            catch (Exception ex)
            {
                ExceptionText += $"{ex.Message} \t";
                NumberOfCrashes++;
                return new ObservableCollection<Tracking>();
            }
        }

        private Command _updateCommand;

        public Command UpdateLogoutCommand => _updateCommand ??= new Command(obj =>
        {
            try
            {
                _dispatcherTimer.Stop();
                WorkWithDatabase.SetSqlCommand($"INSERT INTO [Tracking] VALUES ('{DateTime.Now:yyyy/MM/dd}', '{_startTracking.ToLongTimeString()}', '{DateTime.Now.ToLongTimeString()}', '{_timeSpent}', '{ExceptionText}', '{_user.Email}')");
                WorkWithDatabase.SqlCommand.ExecuteNonQuery();
                Application.Current.Shutdown();
            }
            catch (Exception ex)
            {
                ExceptionText += $"{ex.Message} \t";
                NumberOfCrashes++;
            }
        });

        private DateTime _startTracking;
        private DateTime _dateTime;

        private string _timeSpent;

        public string TimeSpent
        {
            get => _timeSpent;
            set
            {
                _timeSpent = value;
                OnPropertyChanged("TimeSpent");
            }
        }

        private int _numberCrashes = 0;

        public int NumberOfCrashes
        {
            get => _numberCrashes;
            set
            {
                _numberCrashes++;
                OnPropertyChanged("NumberOfCrashes");
            }
        }

        private void DispatcherTimerTick(object sender, EventArgs e)
        {
            _dateTime = _dateTime.AddSeconds(1);
            TimeSpent = _dateTime.ToLongTimeString();
        }

        #region INotifyPropertyChanged Members implementation 

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        #endregion

    }
}
