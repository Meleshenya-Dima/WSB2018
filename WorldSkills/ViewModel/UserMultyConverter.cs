using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Data;
using WorldSkills.Model;

namespace WorldSkills.ViewModel
{
    public class UserMultyConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string EmailTextBox = values[0] as string;
            string FirstNameTextBox = values[1] as string;
            string LastNameTextBox = values[2] as string;
            string ComboBoxOffice = values[3] as string;
            string BirthdatePicker = values[4] as string;
            string PasswordTextBox = values[5] as string;
            int idOffice = 0;
            if (!string.IsNullOrEmpty(EmailTextBox) && !string.IsNullOrEmpty(FirstNameTextBox) && !string.IsNullOrEmpty(LastNameTextBox) && !string.IsNullOrEmpty(ComboBoxOffice) && !string.IsNullOrEmpty(BirthdatePicker) && !string.IsNullOrEmpty(PasswordTextBox))
            {
                WorkWithDatabase.SetSqlCommand($"SELECT ID FROM Offices WHERE Title = '{ComboBoxOffice}'");
                SqlDataReader reader = WorkWithDatabase.SqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    idOffice = int.Parse(reader.GetValue(0).ToString());
                }
                reader.Close();
                if (DataProtection.EmailChecker(EmailTextBox))
                {
                    WorkWithDatabase.SetSqlCommand($"SELECT Email FROM [Users] WHERE Email = '{EmailTextBox}'");
                    SqlDataReader sqlDataReader = WorkWithDatabase.SqlCommand.ExecuteReader();
                    if (!sqlDataReader.Read())
                    {
                        sqlDataReader.Close();
                        return new User { Email = EmailTextBox, FirstName = FirstNameTextBox, LastName = LastNameTextBox, Birthdate = DateTime.Parse(BirthdatePicker), Password = PasswordTextBox, Active = true, OfficeIDText = ComboBoxOffice, RoleID = 2, OfficeID = idOffice };
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
