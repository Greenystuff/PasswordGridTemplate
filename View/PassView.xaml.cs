using System.Windows;
using System.Windows.Controls;
using PasswordGridTemplate.ViewModel;

namespace PasswordGridTemplate.View
{
    public partial class PassView : UserControl
    {
        public PassView()
        {
            InitializeComponent();
            DataContext = new PassViewModel();
        }

        private void Grid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            //// Have to do this in the unusual case where the border of the cell gets selected
            //// and causes a crash 'EditItem is not allowed'
            e.Cancel = true;
            mdpDg.SelectedIndex = -1;
        }

        private void DeleteAllClicked(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Êtes vous sûr de vouloir supprimer tous les mots de passe de la base de données ?",
                    "Supprimer les mots de passe",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                //Delete ALL
            }

        }

        private void DeleteClicked(object sender, RoutedEventArgs e)
        {
            //Delete Selected
        }

        void OnChecked(object sender, RoutedEventArgs e)
        {
            if (mdpDg.IsLoaded)
            {
                dynamic selected_row = mdpDg.SelectedItem;
                if (selected_row != null)
                {
                    // Checked to be deleted
                }
            }
        }

        void OnUnchecked(object sender, RoutedEventArgs e)
        {
            if (mdpDg.IsLoaded)
            {
                dynamic selected_row = mdpDg.SelectedItem;
                if (selected_row != null)
                {
                    // Unchecked to not be deleted
                }
            }
        }

        void OnShowChecked(object sender, RoutedEventArgs e)
        {
            if (mdpDg.IsLoaded)
            {
                // Show Password
            }
        }

        void OnShowUnchecked(object sender, RoutedEventArgs e)
        {
            if (mdpDg.IsLoaded)
            {
                // Hide Password
            }
        }

        private void AddPassClicked(object sender, RoutedEventArgs e)
        {
            // Button Add Password clicked
        }

        private void tbDestinationChanged(object sender, TextChangedEventArgs e)
        {
            addPassBtn.IsEnabled = false;
            if (tbDestination.Text != "" && tbIdentifiant.Text != "" && tbPass.Text != "")
            {
                addPassBtn.IsEnabled = true;
            }
        }

        private void tbIdentifiantChanged(object sender, TextChangedEventArgs e)
        {
            addPassBtn.IsEnabled = false;
            if (tbDestination.Text != "" && tbIdentifiant.Text != "" && tbPass.Text != "")
            {
                addPassBtn.IsEnabled = true;
            }
        }

        private void tbPassChanged(object sender, TextChangedEventArgs e)
        {
            addPassBtn.IsEnabled = false;
            if (tbDestination.Text != "" && tbIdentifiant.Text != "" && tbPass.Text != "")
            {
                addPassBtn.IsEnabled = true;
            }
        }
    }
}
