using System;
using System.Drawing;
using System.Collections;
using System.Windows;

namespace Connect4Minimax
{
    public class Conecta4
    {
        public Random random = new Random();
        public int[,] tablero = new int[7, 6];
        public int[,] tableroCPU = new int[7, 6];
        public int[] cantidades = new int[7];
        public int player, computer, endt = 0;
        public int playerID = 1, cpuID = 2, rec = 5, turnoID = 1;
        public int m, n, r, temp, so, ch, col, t, y;
        public int lin;

        public int Think()
        {
            int i = rec;
            return Verificar(i);
        }

        public void VerificarTurno()
        {
            int idGanada = VerificarGanada();
            if (idGanada == playerID)
            {
                Connect4VM.Instance.Message = "¡Felicidades, Has ganado!";
                endt = 1;
                return;
            }
            else if (idGanada == cpuID)
            {
                Connect4VM.Instance.Message = "¡Lo siento, has perdido!";
                endt = 1;
                return;
            }
            else if (idGanada == 0)
            {
                for (int t = 0; t <= 6; t++)
                {
                    if (cantidades[t] < 6)
                    {
                        idGanada = 1;
                    }
                }
                if (idGanada == 0)
                {
                    endt = 1;
                    return;
                }
            }
        }

        public int Verificar(int i)
        {
            int co, score, t, g, j = 0, p;
            i--;
            if (i == -1)
            {
                score = Posicion();
                return score;
            }
            if (i % 2 == 0)
            {
                int max = 0, k;
                j = 0; co = 0;
                for (t = 0; t < 7; t++)
                {
                    g = Agregar(t, cpuID);
                    if (g == 0)
                    {
                        if (VerificarGanada() == cpuID)
                        {
                            Remover(t);
                            if (i == rec - 1)
                                return t;
                            else
                                return 9000;
                        }
                        k = Verificar(i);
                        if (co == 0)
                        {
                            max = k;
                            co = 1;
                            j = t;
                        }
                        if (k == max)
                        {
                            p = (random.Next(6)) + 1;
                            if (p > 4) j = t;
                        }
                        if (k > max)
                        {
                            max = k;
                            j = t;
                        }
                        Remover(t);
                    }
                }
                score = max;
            }
            else
            {
                int min = 0, k = 0;
                co = 0;
                for (t = 0; t < 7; t++)
                {
                    g = Agregar(t, playerID);
                    if (g == 0)
                    {
                        if (VerificarGanada() == playerID)
                        {
                            Remover(t);
                            return -10000;
                        }
                        k = Verificar(i);
                        if (co == 0)
                        {
                            min = k;
                            co = 1;
                            j = t;
                        }
                        if (k < min)
                        {
                            min = k;
                            j = t;
                        }
                        Remover(t);
                    }
                }
                score = min;
            }
            if (i == rec - 1)
                return j;
            return score;
        }

        public int Agregar(int posicion, int idTurno)
        {
            if (cantidades[posicion] < 6)
            {
                tablero[posicion, cantidades[posicion]] = idTurno;
                cantidades[posicion]++;
                return 0;
            }
            return 1;
        }

        public int Remover(int c)
        {
            cantidades[c]--;
            tablero[c, cantidades[c]] = 0;
            return 0;
        }

        public int Posicion()
        {
            int u, o, x, y, j, score;
            int gh = 0, hg = 0;
            score = 0;

            //Vacia el tablero donde analiza el algoritmo
            for (x = 0; x < 7; x++)
            {
                for (y = 0; y < 6; y++)
                {
                    tableroCPU[x, y] = 0;
                }
            }

            //Suma los puntos de cada oportunidad que tenga
            for (y = 0; y < 6; y++)
            {
                for (x = 0; x < 7; x++)
                {
                    if (tablero[x, y] == 0)
                        score = score + VerificarPosicion(x, y);
                    if (y > 0)
                    {
                        if ((tableroCPU[x, y] == cpuID) && (tablero[x, y - 1] != 0))
                        {
                            gh++;
                        }
                        if ((tableroCPU[x, y] == playerID) && (tablero[x, y - 1] != 0)) 
                        { 
                            hg++; 
                            score = score - 4000; 
                        }
                    }
                    else
                    {
                        if (tableroCPU[x, y] == cpuID)
                        {
                            gh++;
                        }
                        if (tableroCPU[x, y] == playerID)
                        {
                            hg++;
                            score = score - 4000;
                        }
                    }
                }
            }
            if (gh > 1)
            {
                score = score + (gh - 1) * 500;
            }
            if (gh == 1)
            {
                score = score - 100;
            }
            if (hg > 1)
            {
                score = score - (hg - 1) * 500;
            }

            for (x = 0; x < 7; x++)
            {
                gh = 0;
                for (y = 1; y < 6; y++)
                {
                    if ((tableroCPU[x, y] == cpuID) && (tableroCPU[x, y - 1] == cpuID))
                    {
                        u = 0; j = 0;
                        for (o = y - 1; o > -1; o--)
                        {
                            if (tableroCPU[x, o] == playerID) u = 1;
                            if (tablero[x, o] == 0) j++;
                        }
                        if (u == 0)
                            score = score + 1300 - j * 7;
                        if (u == 1)
                            score = score + 300;
                    }

                    if ((tableroCPU[x, y] == playerID) && (tableroCPU[x, y - 1] == playerID))
                    {
                        u = 0; j = 0;
                        for (o = y - 1; o > -1; o--)
                        {
                            if (tableroCPU[x, o] == cpuID) u = 1;
                            if (tablero[x, o] == 0) j++;
                        }
                        if (u == 0) score = score - 1500 + j * 7;
                        if (u == 1) score = score - 300;
                    }
                    if (tableroCPU[x, y] == playerID)
                    {
                        u = 0;
                        for (o = y - 1; o > -1; o--)
                        {
                            if (tableroCPU[x, o] == cpuID) u = 1;
                        }
                        if (u == 1) score = score + 30;
                    }
                    if (tableroCPU[x, y] == cpuID)
                    {
                        u = 0;
                        for (o = y - 1; o > -1; o--)
                        {
                            if (tableroCPU[x, o] == playerID) u = 1;
                        }
                        if (u == 1) score = score - 30;
                    }
                }
            }
            return score;
        }

        //Comprobación que las posicion sea una casilla vacia
        public int VerificarPosicion(int x, int y)
        {
            int score = 0;
            int max, min;
            int d0 = 0, d1 = 0, d2 = 0, d3 = 0;
            if (((x + 1) < 7) && ((y - 1) > -1))
            {
                if (tablero[x + 1, y - 1] == cpuID)
                {
                    d1++;
                    if (((x + 2) < 7) && ((y - 2) > -1))
                    {
                        if (tablero[x + 2, y - 2] == cpuID)
                        {
                            d1++;
                            if (((x + 3) < 7) && ((y - 3) > -1))
                            {
                                if (tablero[x + 3, y - 3] == cpuID) d1++;
                            }
                        }
                    }
                }
            }
            if (((x - 1) > -1) && ((y + 1) < 6))
            {
                if (tablero[x - 1, y + 1] == cpuID)
                {
                    d1++;
                    if (((x - 2) > -1) && ((y + 2) < 6))
                    {
                        if (tablero[x - 2, y + 2] == cpuID)
                        {
                            d1++;
                            if (((x - 3) > -1) && ((y + 3) < 6))
                            {
                                if (tablero[x - 3, y + 3] == cpuID) d1++;
                            }
                        }
                    }
                }
            }
            if (((x - 1) > -1) && ((y - 1) > -1))
            {
                if (tablero[x - 1, y - 1] == cpuID)
                {
                    d2++;
                    if (((x - 2) > -1) && ((y - 2) > -1))
                    {
                        if (tablero[x - 2, y - 2] == cpuID)
                        {
                            d2++;
                            if (((x - 3) > -1) && ((y - 3) > -1))
                            {
                                if (tablero[x - 3, y - 3] == cpuID) d2++;
                            }
                        }
                    }
                }
            }
            if (((x + 1) < 7) && ((y + 1) < 6))
            {
                if (tablero[x + 1, y + 1] == cpuID)
                {
                    d2++;
                    if (((x + 2) < 7) && ((y + 2) < 6))
                    {
                        if (tablero[x + 2, y + 2] == cpuID)
                        {
                            d2++;
                            if (((x + 3) < 7) && ((y + 3) < 6))
                            {
                                if (tablero[x + 3, y + 3] == cpuID) d2++;
                            }
                        }
                    }
                }
            }
            if ((y - 1) > -1) if (tablero[x, y - 1] == cpuID)
                {
                    d0++;
                    if ((y - 2) > -1) if (tablero[x, y - 2] == cpuID)
                        {
                            d0++;
                            if ((y - 3) > -1) if (tablero[x, y - 3] == cpuID) d0++;
                        }
                }
            if (x - 1 > -1)
            {
                if (tablero[x - 1, y] == cpuID)
                {
                    d3++;
                    if (x - 2 > -1)
                    {
                        if (tablero[x - 2, y] == cpuID)
                        {
                            d3++;
                            if (x - 3 > -1) if (tablero[x - 3, y] == cpuID) d3++;
                        }
                    }
                }
            }
            if (x + 1 < 7)
            {
                if (tablero[x + 1, y] == cpuID)
                {
                    d3++;
                    if (x + 2 < 7)
                    {
                        if (tablero[x + 2, y] == cpuID)
                        {
                            d3++;
                            if (x + 3 < 7) if (tablero[x + 3, y] == cpuID) d3++;
                        }
                    }
                }
            }
            max = d0;
            if (d1 > max)
            {
                max = d1;
            }
            if (d2 > max)
            {
                max = d2;
            }
            if (d3 > max)
            { 
                max = d3;
            }
            if (max == 2)
            {
                score = score + 5;
            }
            if (max > 2)
            {
                score = score + 71; tableroCPU[x, y] = cpuID;
                if ((d1 < 3) && (d2 < 3) && (d3 < 3)) score = score - 10;
            }
            if (((x + 1) < 7) && ((y - 1) > -1))
            {
                if (tablero[x + 1, y - 1] == playerID)
                {
                    d1++;
                    if (((x + 2) < 7) && ((y - 2) > -1))
                    {
                        if (tablero[x + 2, y - 2] == playerID)
                        {
                            d1++;
                            if (((x + 3) < 7) && ((y - 3) > -1))
                            {
                                if (tablero[x + 3, y - 3] == playerID) d1++;
                            }
                        }
                    }
                }
            }
            if (((x - 1) > -1) && ((y + 1) < 6))
            {
                if (tablero[x - 1, y + 1] == playerID)
                {
                    d1++;
                    if (((x - 2) > -1) && ((y + 2) < 6))
                    {
                        if (tablero[x - 2, y + 2] == playerID)
                        {
                            d1++;
                            if (((x - 3) > -1) && ((y + 3) < 6))
                            {
                                if (tablero[x - 3, y + 3] == playerID) d1++;
                            }
                        }
                    }
                }
            }
            if (((x - 1) > -1) && ((y - 1) > -1))
            {
                if (tablero[x - 1, y - 1] == playerID)
                {
                    d2++;
                    if (((x - 2) > -1) && ((y - 2) > -1))
                    {
                        if (tablero[x - 2, y - 2] == playerID)
                        {
                            d2++;
                            if (((x - 3) > -1) && ((y - 3) > -1))
                            {
                                if (tablero[x - 3, y - 3] == playerID) d2++;
                            }
                        }
                    }
                }
            }
            if (((x + 1) < 7) && ((y + 1) < 6))
            {
                if (tablero[x + 1, y + 1] == playerID)
                {
                    d2++;
                    if (((x + 2) < 7) && ((y + 2) < 6))
                    {
                        if (tablero[x + 2, y + 2] == playerID)
                        {
                            d2++;
                            if (((x + 3) < 7) && ((y + 3) < 6))
                            {
                                if (tablero[x + 3, y + 3] == playerID) d2++;
                            }
                        }
                    }
                }
            }
            if ((y - 1) > -1) if (tablero[x, y - 1] == playerID)
                {
                    d0++;
                    if ((y - 2) > -1) if (tablero[x, y - 2] == playerID)
                        {
                            d0++;
                            if ((y - 3) > -1) if (tablero[x, y - 3] == playerID) d0++;
                        }
                }
            if (x - 1 > -1)
            {
                if (tablero[x - 1, y] == playerID)
                {
                    d3++;
                    if (x - 2 > -1)
                    {
                        if (tablero[x - 2, y] == playerID)
                        {
                            d3++;
                            if (x - 3 > -1) if (tablero[x - 3, y] == playerID) d3++;
                        }
                    }
                }
            }
            if (x + 1 < 7)
            {
                if (tablero[x + 1, y] == playerID)
                {
                    d3++;
                    if (x + 2 < 7)
                    {
                        if (tablero[x + 2, y] == playerID)
                        {
                            d3++;
                            if (x + 3 < 7) if (tablero[x + 3, y] == playerID) d3++;
                        }
                    }
                }
            }
            min = d0;
            if (d1 > min)
                min = d1;
            if (d2 > min)
                min = d2;
            if (d3 > min)
                min = d3;
            if (min == 2)
                score = score - 4;
            if (min > 2)
            {
                score = score - 70; tableroCPU[x, y] = playerID;
                if ((d1 < 3) && (d2 < 3) && (d3 < 3)) score = score + 10;
            }
            return score;
        }

        public int VerificarGanada()
        {
            int r, x, y;
            r = 0;
            for (y = 2; y > -1; y--)
            {
                for (x = 0; x < 7; x++)
                {
                    checku(x, y, ref r);
                }
            }
            for (y = 0; y < 6; y++)
            {
                for (x = 0; x < 4; x++)
                {
                    check2r(x, y, ref r);
                }
            }
            for (y = 2; y > -1; y--)
            {
                for (x = 0; x < 4; x++)
                {
                    checkr(x, y, ref r);
                }
            }
            for (y = 2; y > -1; y--)
            {
                for (x = 3; x < 7; x++)
                {
                    checkl(x, y, ref r);
                }
            }
            return r;
        }

        public void checku(int x, int y, ref int r)
        {
            if ((tablero[x, y] == 2) && (tablero[x, y + 1] == 2) && (tablero[x, y + 2] == 2) && (tablero[x, y + 3] == 2))
                r = 2;
            if ((tablero[x, y] == 1) && (tablero[x, y + 1] == 1) && (tablero[x, y + 2] == 1) && (tablero[x, y + 3] == 1))
                r = 1;
        }

        public void check2r(int x, int y, ref int r)
        {
            if ((tablero[x, y] == 2) && (tablero[x + 1, y] == 2) && (tablero[x + 2, y] == 2) && (tablero[x + 3, y] == 2))
                r = 2;
            if ((tablero[x, y] == 1) && (tablero[x + 1, y] == 1) && (tablero[x + 2, y] == 1) && (tablero[x + 3, y] == 1))
                r = 1;
        }

        public void checkr(int x, int y, ref int r)
        {
            if ((tablero[x, y] == 2) && (tablero[x + 1, y + 1] == 2) && (tablero[x + 2, y + 2] == 2) && (tablero[x + 3, y + 3] == 2))
                r = 2;
            if ((tablero[x, y] == 1) && (tablero[x + 1, y + 1] == 1) && (tablero[x + 2, y + 2] == 1) && (tablero[x + 3, y + 3] == 1))
                r = 1;
        }

        public void checkl(int x, int y, ref int r)
        {
            if ((tablero[x, y] == 2) && (tablero[x - 1, y + 1] == 2) && (tablero[x - 2, y + 2] == 2) && (tablero[x - 3, y + 3] == 2))
                r = 2;
            if ((tablero[x, y] == 1) && (tablero[x - 1, y + 1] == 1) && (tablero[x - 2, y + 2] == 1) && (tablero[x - 3, y + 3] == 1))
                r = 1;
        }
    }
}
