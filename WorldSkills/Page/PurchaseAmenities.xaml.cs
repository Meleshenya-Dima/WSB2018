using System.Windows;
using WorldSkills.ViewModel;

namespace WorldSkills.Page
{
    public partial class PurchaseAmenities : Window
    {
        public PurchaseAmenities()
        {
            InitializeComponent();
            DataContext = new PurchaseAmenitiesDataManager();
        }
    }
}
