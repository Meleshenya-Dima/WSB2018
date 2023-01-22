using System;
using System.Diagnostics;
using System.Windows;
using WorldSkills.ViewModel;
using WinForm = System.Windows;

namespace WorldSkills.Page
{
    /// <summary>
    /// Логика взаимодействия для ApplyScheduleChanges.xaml
    /// </summary>
    public partial class ApplyScheduleChanges : Window
    {

        public ApplyScheduleChanges(object viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void SearchFileButtonClick(object sender, RoutedEventArgs e)
        {
            if (DefaultDialogService.OpenFileDialog())
            {
                ShowTextFile.Content = DefaultDialogService.FilePath;
            }
        }
    }
}
