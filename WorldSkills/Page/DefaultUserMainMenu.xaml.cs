using System.Windows;
using WorldSkills.Model;
using WorldSkills.ViewModel;

namespace WorldSkills.Page
{
    public partial class DefaultUserMainMenu : Window
    {
        //k.omar@amonic.com
        //4258
        public static User User;
        public DefaultUserMainMenu(User user)
        {
            User = user;
            InitializeComponent();
            DataContext = new UserDataManager(user);
        }

        private void SearchForFlightsButtonClick(object sender, RoutedEventArgs e)
        {
            var newForm = new SearchForFlights();
            newForm.ShowDialog();
        }

        private void AdditionalServicesButtonClick(object sender, RoutedEventArgs e)
        {
            var newForm = new PurchaseAmenities();
            newForm.ShowDialog();
        }
    }
}
