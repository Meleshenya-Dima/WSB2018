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
    /// Логика взаимодействия для ManageFlight.xaml
    /// </summary>
    public partial class ManageFlight : Window
    {
        private ManageFlightSchedulesDataManager _viewModel;
        public ManageFlight()
        {
            InitializeComponent();
            _viewModel  = new ManageFlightSchedulesDataManager();
            DataContext = _viewModel;
        }

        private void EditFlightButtonClick(object sender, RoutedEventArgs e)
        {
            var newForm = new ScheduleEdit(_viewModel);
            newForm.ShowDialog();
        }

        private void ImportChangesButtonClick(object sender, RoutedEventArgs e)
        {
            var newForm = new ApplyScheduleChanges(_viewModel);
            newForm.ShowDialog();
        }
    }
}
