using System.Windows;
using WorldSkills.ViewModel;

namespace WorldSkills.Page
{
    public partial class BookingConfirmation : Window
    {
        private SearchForFlightsDataManager _dataView;
        public BookingConfirmation(object dataContext)
        {
            _dataView = (SearchForFlightsDataManager)dataContext;
            InitializeComponent();
            DataContext = _dataView;
        }

        private void ConfirmBookingButtonClick(object sender, RoutedEventArgs e)
        {
            var page = new BillingConfirmation(_dataView);
            page.ShowDialog();
        }

        private void BackToSearchButtonClick(object sender, RoutedEventArgs e) => this.Close();

    }
}
