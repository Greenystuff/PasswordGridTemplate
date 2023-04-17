using System.Windows;
using System.Windows.Controls;
using PasswordGridTemplate.Models;
using PasswordGridTemplate.Tables;
using PasswordGridTemplate.ViewModel;

namespace PasswordGridTemplate.View
{
    public partial class PassView : UserControl
    {
        PasswordsTable passwordsTable = new();
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
                passwordsTable.DeleteAll();
                mdpDg.ItemsSource = null;
                PassViewModel.gui.getAllPasswords();
                mdpDg.ItemsSource = PassViewModel.gui.Passwords;
            }

        }

        private void DeleteClicked(object sender, RoutedEventArgs e)
        {
            passwordsTable.DeleteSelected();
            mdpDg.ItemsSource = null;
            PassViewModel.gui.getAllPasswords();
            mdpDg.ItemsSource = PassViewModel.gui.Passwords;
        }

        void OnChecked(object sender, RoutedEventArgs e)
        {
            if (mdpDg.IsLoaded)
            {
                dynamic selected_row = mdpDg.SelectedItem;
                if (selected_row != null)
                {
                    Password LineSelected = (Password)mdpDg.SelectedItem;
                    passwordsTable.UpdateToDelete(true, LineSelected.Id);
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
                    Password LineSelected = (Password)mdpDg.SelectedItem;
                    passwordsTable.UpdateToDelete(false, LineSelected.Id);
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
            Password password = new(-1, false, tbDestination.Text, tbIdentifiant.Text, tbPass.Text);
            passwordsTable.InsertPassword(password);
            mdpDg.ItemsSource = null;
            PassViewModel.gui.getAllPasswords();
            mdpDg.ItemsSource = PassViewModel.gui.Passwords;
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
