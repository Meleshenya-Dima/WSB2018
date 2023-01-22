using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Data;
using WorldSkills.Model;

namespace WorldSkills.ViewModel
{
    public class UpdateUserMultyConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string EmailTextBox = values[0] as string;
            string FirstNameTextBox = values[1] as string;
            string LastNameTextBox = values[2] as string;
            string ComboBoxOffice = values[3] as string;
            string UserRole = values[4].ToString();
            string AdministratorRole = values[5].ToString();

            if (!string.IsNullOrEmpty(EmailTextBox) && !string.IsNullOrEmpty(FirstNameTextBox) && !string.IsNullOrEmpty(LastNameTextBox) && !string.IsNullOrEmpty(ComboBoxOffice) && !string.IsNullOrEmpty(UserRole) && !string.IsNullOrEmpty(AdministratorRole))
            {
                WorkWithDatabase.SetSqlCommand($"SELECT * FROM [Users] WHERE Email = '{EmailTextBox}'");
                SqlDataReader sqlDataReader = WorkWithDatabase.SqlCommand.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    if (UserRole == "True" && AdministratorRole == "False")
                    {
                        sqlDataReader.Close();
                        return new User { Email = EmailTextBox, RoleID = 2};
                    }
                    else if(AdministratorRole == "True" && UserRole == "False")
                    {
                        sqlDataReader.Close();
                        return new User { Email = EmailTextBox, RoleID = 1 };
                    }
                    else
                    {
                        sqlDataReader.Close();
                        return null;
                    }
                }
                else
                {
                    sqlDataReader.Close();
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
