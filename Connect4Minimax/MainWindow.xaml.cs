using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Connect4Minimax
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        public int cx = 15;
        public int cy = 70;
        public int Level = 3;
        public Conecta4 conecta4 = new Conecta4();

        public event PropertyChangedEventHandler? PropertyChanged;



        public MainWindow()
        {
            InitializeComponent();
        }

        public void Registrar()
        {
            int q = 0;
            int w = 5;

            for (int y = 0; y < 6; y++)
            {
                for (int x = 0; x < 7; x++)
                {
                    if (conecta4.tablero[q, w] == 0)
                    {
                        switch (q)
                        {
                            case 0:
                                Connect4VM.Instance.ColoresFila1[w] = 0;
                                break;
                            case 1:
                                Connect4VM.Instance.ColoresFila2[w] = 0;
                                break;
                            case 2:
                                Connect4VM.Instance.ColoresFila3[w] = 0;
                                break;
                            case 3:
                                Connect4VM.Instance.ColoresFila4[w] = 0;
                                break;
                            case 4:
                                Connect4VM.Instance.ColoresFila5[w] = 0;
                                break;
                            case 5:
                                Connect4VM.Instance.ColoresFila6[w] = 0;
                                break;
                            case 6:
                                Connect4VM.Instance.ColoresFila7[w] = 0;
                                break;
                            default:
                                break;
                        }
                    }
                    if (conecta4.tablero[q, w] == 1)
                    {
                        switch (q)
                        {
                            case 0:
                                Connect4VM.Instance.ColoresFila1[w] = 1;
                                break;
                            case 1:
                                Connect4VM.Instance.ColoresFila2[w] = 1;
                                break;
                            case 2:
                                Connect4VM.Instance.ColoresFila3[w] = 1;
                                break;
                            case 3:
                                Connect4VM.Instance.ColoresFila4[w] = 1;
                                break;
                            case 4:
                                Connect4VM.Instance.ColoresFila5[w] = 1;
                                break;
                            case 5:
                                Connect4VM.Instance.ColoresFila6[w] = 1;
                                break;
                            case 6:
                                Connect4VM.Instance.ColoresFila7[w] = 1;
                                break;
                            default:
                                break;
                        }
                    }
                    if (conecta4.tablero[q, w] == 2)
                    {
                        switch (q)
                        {
                            case 0:
                                Connect4VM.Instance.ColoresFila1[w] = 2;
                                break;
                            case 1:
                                Connect4VM.Instance.ColoresFila2[w] = 2;
                                break;
                            case 2:
                                Connect4VM.Instance.ColoresFila3[w] = 2;
                                break;
                            case 3:
                                Connect4VM.Instance.ColoresFila4[w] = 2;
                                break;
                            case 4:
                                Connect4VM.Instance.ColoresFila5[w] = 2;
                                break;
                            case 5:
                                Connect4VM.Instance.ColoresFila6[w] = 2;
                                break;
                            case 6:
                                Connect4VM.Instance.ColoresFila7[w] = 2;
                                break;
                            default:
                                break;
                        }
                    }
                    q++;
                }
                q = 0;
                w--;
            }
        }


        public async void PlayerClick(int idFila)
        {
            if (conecta4.endt == 0 && conecta4.turnoID != conecta4.cpuID)
            {
                if ((idFila >= 0) && (idFila < 7))
                {
                    if (conecta4.Agregar(idFila, conecta4.playerID) == 0)
                    {
                        Registrar();
                        conecta4.VerificarTurno();
                        conecta4.turnoID = conecta4.cpuID;
                        if (conecta4.endt == 1)
                        {
                            return;
                        }
                        Connect4VM.Instance.Message = "Turno del CPU";
                        
                        await Task.Delay(800);
                        if (conecta4.endt == 0)
                        {
                            int ck;
                            conecta4.col = conecta4.Think();
                            ck = conecta4.Agregar(conecta4.col, conecta4.cpuID);
                            if (ck == 0)
                            {
                                conecta4.lin = 6 - conecta4.cantidades[conecta4.col];
                                conecta4.turnoID = conecta4.playerID;
                            }
                        }
                        Registrar();
                        conecta4.VerificarTurno();
                        if (conecta4.endt == 1)
                        {
                            return;
                        }
                        Connect4VM.Instance.Message = "Turno del Jugador";
                    }

                }

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PlayerClick(0);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PlayerClick(1);
        }

        private void btnFila3_Click(object sender, RoutedEventArgs e)
        {
            PlayerClick(2);
        }

        private void btnFila4_Click(object sender, RoutedEventArgs e)
        {
            PlayerClick(3);
        }

        private void btnFila5_Click(object sender, RoutedEventArgs e)
        {
            PlayerClick(4);
        }

        private void btnFila6_Click(object sender, RoutedEventArgs e)
        {
            PlayerClick(5);
        }

        private void btnFila7_Click(object sender, RoutedEventArgs e)
        {
            PlayerClick(6);
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            conecta4 = new Conecta4();
            Registrar();
            Connect4VM.Instance.Message = "Turno del Jugador";
        }
    }
}
