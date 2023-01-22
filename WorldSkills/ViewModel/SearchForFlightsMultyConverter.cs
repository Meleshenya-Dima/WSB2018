using System;
using System.Globalization;
using System.Windows.Data;

namespace WorldSkills.Model
{
    class SearchForFlightsMultyConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 7)
            {

                string From = values[0] as string;
                string To = values[1] as string;
                string CabinType = values[2] as string;
                string Outbound = values[3] as string;
                string Return = values[4] as string;
                string ThreeDaysOutbound = values[5].ToString();
                string ThreeDaysReturn = values[6].ToString();

                if (!string.IsNullOrEmpty(From) && !string.IsNullOrEmpty(To) && !string.IsNullOrEmpty(CabinType) && !string.IsNullOrEmpty(Outbound) && !string.IsNullOrEmpty(ThreeDaysOutbound) && !string.IsNullOrEmpty(ThreeDaysReturn))
                {
                    return new SearchFlightValue { From = From, To = To, CabinType = CabinType, Outbound = Outbound, Return = Return, ThreeDaysOutbound = bool.Parse(ThreeDaysOutbound), ThreeDaysReturn = bool.Parse(ThreeDaysReturn) };
                }
                else
                {
                    return null;
                }
            }
            else if(values.Length == 6)
            {
                string Firstname = values[0] as string;
                string Lastname = values[1] as string;
                string Birthdate = values[2] as string;
                string PasswordNumber = values[3] as string;
                string PassportCountry = values[4] as string;
                string Phone = values[5] as string;
                if (!string.IsNullOrEmpty(Firstname) && !string.IsNullOrEmpty(Lastname) && !string.IsNullOrEmpty(Birthdate) && !string.IsNullOrEmpty(PasswordNumber) && !string.IsNullOrEmpty(PassportCountry) && !string.IsNullOrEmpty(Phone))
                {
                    return new Tickets { Phone = Phone, PassportNumber =  PasswordNumber, PassportCountryID = PassportCountry, Firstname = Firstname, Lastname = Lastname };
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
