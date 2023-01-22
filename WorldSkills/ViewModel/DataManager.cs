using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Windows;
using WorldSkills.Model;
using WorldSkills.Page;

namespace WorldSkills.ViewModel
{
    class DataManager : INotifyPropertyChanged
    {
        public DataManager()
        {
            AllOffice = TakeAllOffice();
            AllUsers = TakeAllUser();
        }

        #region Values
        private User _selectedUser;
        private string _selectedOffice;
        private Command _addCommand;
        private Command _enableDisableLogin;
        private Command _updateCommand;
        private ObservableCollection<User> _user;
        #endregion

        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged("SelectedUser");
            }
        }
        public string SelectedOffice
        {
            get { return _selectedOffice; }
            set
            {
                _selectedOffice = value;
                if (_selectedOffice != "All Offices")
                {
                    ObservableCollection<User> users = new();
                    WorkWithDatabase.SetSqlCommand($"SELECT * FROM [Users] WHERE OfficeID = (SELECT ID FROM Offices WHERE Title = '{_selectedOffice}')");
                    SqlDataReader sqlDataReader = WorkWithDatabase.SqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        string obj = "";
                        for (int i = 0; i < AllOffice.Count; i++)
                        {
                            if (AllOffice[i].ID == int.Parse(sqlDataReader.GetValue(6).ToString()))
                            {
                                obj += AllOffice[i].Title;
                            }
                        }
                        users.Add(new User { ID = int.Parse(sqlDataReader.GetValue(0).ToString()), RoleID = int.Parse(sqlDataReader.GetValue(1).ToString()), Email = sqlDataReader.GetValue(2).ToString(), Password = sqlDataReader.GetValue(3).ToString(), FirstName = sqlDataReader.GetValue(4).ToString(), LastName = sqlDataReader.GetValue(5).ToString(), OfficeID = int.Parse(sqlDataReader.GetValue(6).ToString()), Birthdate = DateTime.Parse(sqlDataReader.GetValue(7).ToString()), Active = bool.Parse(sqlDataReader.GetValue(8).ToString()), OfficeIDText = obj });
                    }
                    AllUsers = users;
                    sqlDataReader.Close();
                }
                else if (_selectedOffice == "All Offices")
                {
                    AllUsers = TakeAllUser();
                }
            }
        }

        public ObservableCollection<User> AllUsers
        {
            get => _user ??= new ObservableCollection<User>();
            set
            {
                _user = value;
                OnPropertyChanged("AllUsers");
            }
        }

        public ObservableCollection<Office> AllOffice { get; set; }


        public ObservableCollection<User> TakeAllUser()
        {
            ObservableCollection<User> users = new();
            WorkWithDatabase.SetSqlCommand("SELECT * FROM [Users]");
            SqlDataReader sqlDataReader = WorkWithDatabase.SqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                string obj = "";
                for (int i = 0; i < AllOffice.Count; i++)
                {
                    if (AllOffice[i].ID == int.Parse(sqlDataReader.GetValue(6).ToString()))
                    {
                        obj += AllOffice[i].Title;
                    }
                }
                users.Add(new User { ID = int.Parse(sqlDataReader.GetValue(0).ToString()), RoleID = int.Parse(sqlDataReader.GetValue(1).ToString()), Email = sqlDataReader.GetValue(2).ToString(), Password = sqlDataReader.GetValue(3).ToString(), FirstName = sqlDataReader.GetValue(4).ToString(), LastName = sqlDataReader.GetValue(5).ToString(), OfficeID = int.Parse(sqlDataReader.GetValue(6).ToString()), Birthdate = DateTime.Parse(sqlDataReader.GetValue(7).ToString()), Active = bool.Parse(sqlDataReader.GetValue(8).ToString()), OfficeIDText = obj });
            }
            sqlDataReader.Close();
            return users;
        }

        public ObservableCollection<Office> TakeAllOffice()
        {
            ObservableCollection<Office> offices = new();
            offices.Add(new Office { Title = "All Offices"});
            WorkWithDatabase.SetSqlCommand("SELECT * FROM [Offices]");
            SqlDataReader sqlDataReader = WorkWithDatabase.SqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                offices.Add(new Office { ID = int.Parse(sqlDataReader.GetValue(0).ToString()), CountryID = int.Parse(sqlDataReader.GetValue(1).ToString()), Title = sqlDataReader.GetValue(2).ToString(), Phone = sqlDataReader.GetValue(3).ToString(), Contact = sqlDataReader.GetValue(4).ToString() });
            }
            sqlDataReader.Close();
            return offices;
        }


        public Command AddCommand => _addCommand ??= new Command(obj =>
        {
            if (obj is User user)
            {
                int boolPosition = 0;
                if (user.Active)
                {
                    boolPosition = 1;
                }
                WorkWithDatabase.SetSqlCommand($"INSERT INTO Users VALUES ({user.RoleID}, '{user.Email}', '{DataProtection.ConvertToMD5(user.Password)}', '{user.FirstName}', '{user.LastName}', {user.OfficeID}, '{user.Birthdate:yyyy/MM/dd}', {boolPosition})");
                int isAdd = WorkWithDatabase.SqlCommand.ExecuteNonQuery();
                if (isAdd == 1)
                {
                    AllUsers.Insert(0,user);
                    MessageBox.Show("Пользователь добавлен");
                    OnPropertyChanged("AllUsers");
                }
                else
                {
                    MessageBox.Show("Пользователь не добавлен");
                }
            }
        });

        public Command EnableDisableLogin => _enableDisableLogin ??= new Command(obj =>
        {
            if (obj is User user)
            {
                if (user.Active)
                {
                    WorkWithDatabase.SetSqlCommand($"UPDATE Users SET Active = {0} WHERE Email = '{user.Email}'");
                    WorkWithDatabase.SqlCommand.ExecuteNonQuery();
                }
                else
                {
                    WorkWithDatabase.SetSqlCommand($"UPDATE Users SET Active = {1} WHERE Email = '{user.Email}'");
                    WorkWithDatabase.SqlCommand.ExecuteNonQuery();
                }
                user.Active = !user.Active;
            }
        });

        public Command UpdateCommand => _updateCommand ??= new Command(obj =>
        {
            if (obj is User user)
            {
                if (user.RoleIDText == "Administrator")
                {
                    WorkWithDatabase.SetSqlCommand($"UPDATE Users SET RoleID = {1} WHERE Email = '{user.Email}'");
                    WorkWithDatabase.SqlCommand.ExecuteNonQuery();
                    foreach (var item in AllUsers)
                    {
                        if (item.Email == user.Email)
                        {
                            item.RoleID = 1;
                            break;
                        }
                    }
                }
                else
                {
                    WorkWithDatabase.SetSqlCommand($"UPDATE Users SET RoleID = {2} WHERE Email = '{user.Email}'");
                    WorkWithDatabase.SqlCommand.ExecuteNonQuery();
                    foreach (var item in AllUsers)
                    {
                        if (item.Email == user.Email)
                        {
                            item.RoleID = 2;
                            break;
                        }
                    }
                }
            }
        });

        #region INotifyPropertyChanged Members implementation 

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        #endregion
    }
}
