using System.Windows;
using WorldSkills.Model;
using WorldSkills.ViewModel;

namespace WorldSkills.Page
{
    public partial class AdministratorMainMenu : Window
    {
        //j.doe@amonic.com
        //123

        private DataManager _viewModelDataManager;
        public AdministratorMainMenu()
        {
            _viewModelDataManager = new DataManager();
            DataContext = _viewModelDataManager;
            InitializeComponent();
            OfficesComboBox.SelectedIndex = 0;
        }

        private void ManageFlightSchedules(object sender, RoutedEventArgs e)
        {
            var newForm = new ManageFlight();
            newForm.ShowDialog();
        }

        private void ChangeRoleButtonClick(object sender, RoutedEventArgs e)
        {
            var newForm = new EditRolePage(_viewModelDataManager);
            newForm.ShowDialog();
        }

        private void ExitButtonClick(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

        private void AddUserButtonClick(object sender, RoutedEventArgs e)
        {
            var newForm = new AddUserPage(_viewModelDataManager);
            newForm.ShowDialog();
        }
    }
}
