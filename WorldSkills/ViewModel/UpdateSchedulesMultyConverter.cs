using System;
using System.Globalization;
using System.Windows.Data;
using WorldSkills.Model;

namespace WorldSkills.ViewModel
{
    class UpdateSchedulesMultyConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Schedules schedules = new Schedules();
            string Date = values[0] as string;
            string Time = values[1] as string;
            string EconomyPrice = values[2] as string;

            if (!string.IsNullOrEmpty(Date))
            {
                DateTime date;
                bool isParse = DateTime.TryParse(Date, out date);
                if (isParse)
                {
                    schedules.Data = date;
                }
            }
            if (!string.IsNullOrEmpty(Time))
            {
                DateTime time;
                bool isParse = DateTime.TryParse(Time, out time);
                if (isParse)
                {
                    schedules.Time = time;
                }
            }
            if (!string.IsNullOrEmpty(EconomyPrice))
            {
                schedules.EconomyPrice = decimal.Parse(EconomyPrice);
            }
            return schedules;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
