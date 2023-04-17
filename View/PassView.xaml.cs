using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using PasswordGridTemplate.Models;
using PasswordGridTemplate.Tables;
using PasswordGridTemplate.ViewModel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

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
                Password LineSelected = (Password)mdpDg.SelectedItem;
                
                CheckBox chk = (CheckBox)sender;
                DataGridRow row = (DataGridRow)mdpDg.ItemContainerGenerator.ContainerFromItem(chk.DataContext);
                DataGridCell p = (DataGridCell)((TextBlock)mdpDg.Columns[4].GetCellContent(row)).Parent;
                UIElement child = (UIElement)VisualTreeHelper.GetChild(p, 0);
                UIElement child2 = (UIElement)VisualTreeHelper.GetChild(child, 0);
                UIElement child3 = (UIElement)VisualTreeHelper.GetChild(child2, 0);
                TextBlock tbPass = child3 as TextBlock;
                tbPass.Text = passwordsTable.GetPasswordById(LineSelected.Id);

            }
        }

        void OnShowUnchecked(object sender, RoutedEventArgs e)
        {
            if (mdpDg.IsLoaded)
            {
                // Hide Password
                CheckBox chk = (CheckBox)sender;
                DataGridRow row = (DataGridRow)mdpDg.ItemContainerGenerator.ContainerFromItem(chk.DataContext);
                DataGridCell p = (DataGridCell)((TextBlock)mdpDg.Columns[4].GetCellContent(row)).Parent;
                UIElement child = (UIElement)VisualTreeHelper.GetChild(p, 0);
                UIElement child2 = (UIElement)VisualTreeHelper.GetChild(child, 0);
                UIElement child3 = (UIElement)VisualTreeHelper.GetChild(child2, 0);
                TextBlock tbPass = child3 as TextBlock;
                tbPass.Text = new string('•', tbPass.Text.Length);
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
