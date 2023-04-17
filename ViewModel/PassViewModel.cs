using System.Windows.Input;

namespace PasswordGridTemplate.ViewModel
{
    public class PassViewModel : ViewModelBase
    {
        public static PassViewModel? gui;
        //public ObservableCollection<Password> Passwords { get; set; }
        public PassViewModel()
        {
            gui = this;
            //getAllPasswords();
        }

        public ICommand SubmitCommand { get; }
    }
}
