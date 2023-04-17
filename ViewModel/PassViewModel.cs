using PasswordGridTemplate.Models;
using PasswordGridTemplate.Tables;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PasswordGridTemplate.ViewModel
{
    public class PassViewModel : ViewModelBase
    {
        public static PassViewModel? gui;
        public ObservableCollection<Password> Passwords { get; set; }
        public PassViewModel()
        {
            gui = this;
            getAllPasswords();
        }

        public async void getAllPasswords()
        {
            PasswordsTable passwordsTable = new();
            List<Password> passwords = await passwordsTable.GetAllPasswords();
            Passwords = new ObservableCollection<Password>(passwords);
        }

        public ICommand SubmitCommand { get; }
    }
}
