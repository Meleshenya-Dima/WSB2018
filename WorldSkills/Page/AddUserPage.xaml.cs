using System.Windows;
using WorldSkills.ViewModel;

namespace WorldSkills.Page
{
    public partial class AddUserPage : Window
    {
        public AddUserPage(object viewModel )
        {
            this.DataContext = viewModel;
            InitializeComponent();
            ComboBoxOffice.SelectedIndex = 0;
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
