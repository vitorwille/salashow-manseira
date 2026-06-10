using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaShow
{
    internal class View
    {
        private ConsoleColor _corBg;
        private ConsoleColor _corTexto;
        
        public View(ConsoleColor colorBg, ConsoleColor colorTxt)
        {
            this._corBg = colorBg;
            this._corTexto = colorTxt;
        }
        
        public View() { }
        
        public void PrepararJanela(string tituloJanela, int colIni, int linIni, int colFin, int linFin)
        {
            Console.BackgroundColor = this._corBg;
            Console.ForegroundColor = this._corTexto;
            Console.Clear();
            this.DesenharJanela(colIni, linIni, colFin, linFin);
            this.CentralizarJanela(colIni, colFin, linIni+1, tituloJanela);
        }
        
        public void CentralizarJanela(int colIni, int colFin, int linha, string tituloJanela)
        {
            int coluna = colIni + ((colFin - colIni - tituloJanela.Length) / 2);
            Console.SetCursorPosition(coluna, linha);
            Console.Write(tituloJanela);
        }
        
        public string PedirInput(string textoDialogo, int linha, int colIni, int colFin)
        {
            string resposta;
            this.LimpaSelecao(colIni, linha, colFin, linha);
            Console.SetCursorPosition(colIni, linha);
            Console.Write(textoDialogo);
            resposta = Console.ReadLine();
            return resposta.ToUpper();
        }
        
        public void LimpaSelecao(int colIni, int linIni, int colFin, int linFin)
        {
            for(int x=colIni; x<=colFin; x++) // x vertical, y horizontal
            {
                for (int y=linIni; y<=linFin; y++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(" ");
                }
            }
        }
        
        public void DesenharJanela(int colIni, int linIni, int colFin, int linFin)
        {
            int linha, coluna;

            this.LimpaSelecao(colIni, linIni, colFin, linFin);

            // quina baixo esq
            Console.SetCursorPosition(colIni, linFin);
            Console.Write('┗');

            // quina baixo dir
            Console.SetCursorPosition(colFin, linFin);
            Console.Write('┛');
            
            // quina cima esq
            Console.SetCursorPosition(colIni, linIni);
            Console.Write('┏');

            // quina cima dir
            Console.SetCursorPosition(colFin, linIni);
            Console.Write('┓');
            
            // vertical
            for(linha=linIni+1; linha<linFin; linha++)
            {
                Console.SetCursorPosition(colIni, linha);
                Console.Write("┃");
                Console.SetCursorPosition(colFin, linha);
                Console.Write("┃");
            }
            
            // horizontal - top
            for(coluna=colIni+1; coluna<colFin; coluna++) 
            {
                Console.SetCursorPosition(coluna, linIni);
                Console.Write('━');
            }

            // horizontal - bottom
            for(coluna=colIni+1; coluna<colFin; coluna++) 
            {
                Console.SetCursorPosition(coluna, linFin);
                Console.Write('━');
            }
        }


        public string MostrarJanelaOpcoes(int colIni, int linIni, List<string> listaOpcoes)
        {
            string escolha;
            int i;
            int colFin = colIni + listaOpcoes[0].Length + 1;
            int linFinalJanela = linIni + listaOpcoes.Count() + 2;

            this.DesenharJanela(colIni, linIni, colFin, linFinalJanela);
            for(i=0; i<listaOpcoes.Count; i++)
            {
                Console.SetCursorPosition(colIni+1, linIni+1+i);
                Console.Write(listaOpcoes[i]);
            }
            Console.SetCursorPosition(colIni + 1, linIni + 1 + i);
            Console.Write("Opção: ");
            escolha = Console.ReadLine();

            return escolha;
        }




    }
}
