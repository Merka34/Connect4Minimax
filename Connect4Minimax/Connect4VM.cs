using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4Minimax
{
    public class Connect4VM : INotifyPropertyChanged
    {
        public static Connect4VM? Instance;
        public event PropertyChangedEventHandler? PropertyChanged;

        private string _message = "Turno del Jugador";

        public string Message
        {
            get { return _message; }
            set { _message = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Message))); }
        }


        private ObservableCollection<int> _coloresFila1;

        public ObservableCollection<int> ColoresFila1
        {
            get { return _coloresFila1; }
            set { _coloresFila1 = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ColoresFila1))); }
        }

        private ObservableCollection<int> _coloresFila2;

        public ObservableCollection<int> ColoresFila2
        {
            get { return _coloresFila2; }
            set { _coloresFila2 = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ColoresFila2))); }
        }

        private ObservableCollection<int> _controlesFila3;

        public ObservableCollection<int> ColoresFila3
        {
            get { return _controlesFila3; }
            set { _controlesFila3 = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ColoresFila3))); }
        }

        private ObservableCollection<int> _controlesFila4;

        public ObservableCollection<int> ColoresFila4
        {
            get { return _controlesFila4; }
            set { _controlesFila4 = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ColoresFila4))); }
        }

        private ObservableCollection<int> _controlesFila5;

        public ObservableCollection<int> ColoresFila5
        {
            get { return _controlesFila5; }
            set { _controlesFila5 = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ColoresFila5))); }
        }

        private ObservableCollection<int> _controlesFila6;

        public ObservableCollection<int> ColoresFila6
        {
            get { return _controlesFila6; }
            set { _controlesFila6 = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ColoresFila6))); }
        }

        private ObservableCollection<int> _controlesFila7;

        public ObservableCollection<int> ColoresFila7
        {
            get { return _controlesFila7; }
            set { _controlesFila7 = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ColoresFila7))); }
        }

        public Connect4VM()
        {
            Instance = this;
            ColoresFila1 = new ObservableCollection<int> { 0, 0, 0, 0, 0, 0 };
            ColoresFila2 = new ObservableCollection<int> { 0, 0, 0, 0, 0, 0 };
            ColoresFila3 = new ObservableCollection<int> { 0, 0, 0, 0, 0, 0 };
            ColoresFila4 = new ObservableCollection<int> { 0, 0, 0, 0, 0, 0 };
            ColoresFila5 = new ObservableCollection<int> { 0, 0, 0, 0, 0, 0 };
            ColoresFila6 = new ObservableCollection<int> { 0, 0, 0, 0, 0, 0 };
            ColoresFila7 = new ObservableCollection<int> { 0, 0, 0, 0, 0, 0 };
        }
    }
}
