using FontAwesome.Sharp;
using System.Windows.Input;

namespace PasswordGridTemplate.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        //Fields
        
        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;

        

        
        //--> Properties
        public ViewModelBase CurrentChildView 
        { 
            get
            {
                return _currentChildView;
            }
            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }
        public string Caption
        { 
            get
            {
                return _caption;
            }
            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }
        public IconChar Icon 
        { 
            get
            {
                return _icon;
            }
            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }

        //--> Commands
        public ICommand ShowPassViewCommand { get; }

        public MainViewModel()
        {
            ShowPassViewCommand = new ViewModelCommand(ExecuteShowPassViewCommand);

            //Default View

            ExecuteShowPassViewCommand(null);
        }

        private void ExecuteShowPassViewCommand(object obj)
        {
            CurrentChildView = new PassViewModel();
            Caption = "Mots de passe";
            Icon = IconChar.Lock;
        }
    }
}
