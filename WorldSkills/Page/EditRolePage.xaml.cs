using System.Windows;

namespace WorldSkills.Page
{
    /// <summary>
    /// Логика взаимодействия для EditRolePage.xaml
    /// </summary>
    public partial class EditRolePage : Window
    {
        public EditRolePage(object viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e) => this.Close();
    }
}
