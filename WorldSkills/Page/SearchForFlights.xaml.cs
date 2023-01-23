using System.Windows;
using System.Windows.Controls;
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
            if ((bool)ReturnRadioButton.IsChecked)
            {
                if (SearchForFlightsDataManager.SelectOutboundAiportscountTicket > SearchForFlightsDataManager.CountTicket && SearchForFlightsDataManager.SelectReturnAirportsCountTicket > SearchForFlightsDataManager.CountTicket)
                {
                    var page = new BookingConfirmation(_dataView);
                    page.ShowDialog();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Количество заказанных билетов больше, чем мест!");
                }
            }
            else
            {
                if (SearchForFlightsDataManager.SelectOutboundAiportscountTicket > SearchForFlightsDataManager.CountTicket)
                {
                    var page = new BookingConfirmation(_dataView);
                    page.ShowDialog();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Количество заказанных билетов больше, чем мест!");
                }

            }
        }

        private void CountPassengersTextChanged(object sender, TextChangedEventArgs e)
        {
            SearchForFlightsDataManager.CountTicket = int.Parse(CountPassengers.Text);
        }
    }
}
