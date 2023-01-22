using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Threading;
using WorldSkills.Model;
using WorldSkills.Page;
using WorldSkills.ViewModel;

namespace WorldSkills
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Value
        private DispatcherTimer _dispatcherTimer;
        private byte _badLoginCount;
        #endregion

        public byte BadLoginCount
        {
            get
            {
                return _badLoginCount;
            }
            set
            {
                _badLoginCount++;
                if (_badLoginCount == 3)
                {
                    StartTimer();
                    _badLoginCount = 0;
                }
            }
        }

        private void ExitButtonClick(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

        private void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            if (DataProtection.EmailChecker(LoginTextBox.Text))
            {
                WorkWithDatabase.SetSqlCommand($"SELECT * FROM [Users] WHERE Email = '{LoginTextBox.Text}' AND Password = '{DataProtection.ConvertToMD5(PasswordTextBox.Text)}'");
                SqlDataReader sqlDataReader = WorkWithDatabase.SqlCommand.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    if (bool.Parse(sqlDataReader.GetValue(8).ToString()))
                    {
                        if (int.Parse(sqlDataReader.GetValue(1).ToString()) == 1)
                        {
                            sqlDataReader.Close();
                            var newForm = new AdministratorMainMenu();
                            newForm.Show();
                            this.Close();
                        }
                        else
                        {
                            User user = new User { ID = int.Parse(sqlDataReader.GetValue(0).ToString()),  FirstName = sqlDataReader.GetValue(4).ToString(), Email = sqlDataReader.GetValue(2).ToString()};
                            sqlDataReader.Close();
                            var newForm = new DefaultUserMainMenu(user);
                            newForm.Show();
                            this.Close();
                        }
                    }
                    else
                    {
                        sqlDataReader.Close();
                        MessageBox.Show($"Вход в аккаунт {LoginTextBox.Text} невозможен, по причине его блокировки", "Аккаунт заблокирован", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    BadLoginCount++;
                }
                sqlDataReader.Close();
            }
            else
            {
                BadLoginCount++;
            }
        }

        private void StartTimer()
        {
            PasswordTextBox.IsEnabled = false;
            LoginTextBox.IsEnabled = false;
            LoginTextBox.Text = "";
            PasswordTextBox.Text = "";
            _dispatcherTimer = new();
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            _dispatcherTimer.Tick += DispatcherTimerTick;
            _dispatcherTimer.Start();
        }

        private int increment = 10;

        private void StopTimer()
        {
            _dispatcherTimer.Stop();
            LoginTextBox.IsEnabled = true;
            PasswordTextBox.IsEnabled = true;
            LoginTextBox.Text = "";
            increment = 10;
        }

        private void DispatcherTimerTick(object sender, EventArgs e)
        {
            if (increment == 0)
                StopTimer();
            else
                LoginTextBox.Text = $"{increment} секунд";
            increment--;
        }

        private void GridLoadedEvent(object sender, RoutedEventArgs e)
        {
            WorkWithDatabase.OpenConnection();
            //WorkWithDatabase.SetSqlCommand($"INSERT INTO [Users] (RoleID, Email, Password, FirstName, LastName, OfficeID, Birthdate, Active) VALUES (2,'m.osteen@amonic.com','{DataProtection.ConvertToMD5("9800")}','Milagros','Osteen',3,'1991-02-03',0)");
            //Вот так записывать
        }
    }
}
