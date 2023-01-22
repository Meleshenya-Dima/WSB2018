using System;
using System.Globalization;
using System.Windows.Data;
using WorldSkills.Model;

namespace WorldSkills.ViewModel
{
    class SendFilterInformationMultyConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {

            string From = values[0] as string;
            string To = values[1] as string;
            string SortValue = values[2] as string;
            string Outbound = values[3] as string;
            string FlightNumber = values[4] as string;

            if (!string.IsNullOrEmpty(From) && !string.IsNullOrEmpty(To))
            {
                return new FilterValues {From = From, To = To, SortValue = SortValue};
            }
            else if (!string.IsNullOrEmpty(Outbound) && !string.IsNullOrEmpty(FlightNumber))
            {
                return new FilterValues { Outbound = Outbound, FlightNumber = FlightNumber, SortValue = SortValue };
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
