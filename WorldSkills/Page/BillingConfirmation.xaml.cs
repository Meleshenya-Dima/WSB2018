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
    /// Логика взаимодействия для BillingConfirmation.xaml
    /// </summary>
    public partial class BillingConfirmation : Window
    {
        private SearchForFlightsDataManager _dataContext;
        public BillingConfirmation(object contentview)
        {
            _dataContext = (SearchForFlightsDataManager)contentview;
            InitializeComponent();
            DataContext = _dataContext;
        }
    }
}
