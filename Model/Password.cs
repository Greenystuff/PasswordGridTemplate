using System;
using System.ComponentModel;

namespace PasswordGridTemplate.Models
{
    public class Password : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public Password(int id, bool toDelete, string destination, string identifiant, string pass)
        {
            Id = id;
            ToDelete = toDelete;
            Destination = destination;
            Identifiant = identifiant;
            Pass = pass;
            
        }
        private int _Id;
        private bool _ToDelete;
        private string _Destination;
        private string _Identifiant;
        private string _Pass;

        public int Id
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public bool ToDelete
        {
            get
            {
                return _ToDelete;
            }
            set
            {
                _ToDelete = value;
                OnPropertyChanged(nameof(ToDelete));
            }
        }

        public String Destination
        {
            get
            {
                return _Destination;
            }
            set
            {
                _Destination = value;
                OnPropertyChanged("Destination");
            }
        }

        public String Identifiant
        {
            get
            {
                return _Identifiant;
            }
            set
            {
                _Identifiant = value;
                OnPropertyChanged("Identifiant");
            }
        }

        public String Pass
        {
            get
            {
                return _Pass;
            }
            set
            {
                _Pass = value;
                OnPropertyChanged("Pass");
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

}
