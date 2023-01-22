using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WorldSkills.ViewModel;

namespace WorldSkills.Page
{
    /// <summary>
    /// Логика взаимодействия для SearchForFlights.xaml
    /// </summary>
    public partial class SearchForFlights : Window
    {
        SearchForFlightsDataManager _dataView;
        public SearchForFlights()
        {
            _dataView = new SearchForFlightsDataManager();
            InitializeComponent();
            DataContext = _dataView;
        }

        private void BookingConfirmationButtonClick(object sender, RoutedEventArgs e)
        {
            var page = new BookingConfirmation(_dataView, int.Parse(CountPassengers.Text));
            page.ShowDialog();
            this.Hide();
        }
    }
}
