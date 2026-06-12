using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaShow
{
    internal class View
    {
        private ConsoleColor _colorBg;
        private ConsoleColor _colorText;
        
        public View(ConsoleColor colorBg, ConsoleColor colorTxt)
        {
            this._colorBg = colorBg;
            this._colorText = colorTxt;
        }
        
        public View() { }
        
        public void PrepareWindow(string windowTitle, int colStart, int linStart, int colEnd, int linEnd)
        {
            Console.BackgroundColor = this._colorBg;
            Console.ForegroundColor = this._colorText;
            Console.Clear();
            this.DrawWindow(colStart, linStart, colEnd, linEnd);
            this.CenterWindow(colStart, colEnd, linStart+1, windowTitle);
        }
        
        public void CenterWindow(int colStart, int colEnd, int line, string windowTitle)
        {
            int column = colStart + 1 + ((colEnd - colStart - 1 - windowTitle.Length) / 2);
            Console.SetCursorPosition(column, line);
            Console.Write(windowTitle);
        }
        
        public string AskInput(string dialogText, int line, int colStart, int colEnd)
        {
            string answer;
            this.ClearSelection(colStart, line, colEnd, line);
            Console.SetCursorPosition(colStart, line);
            Console.Write(dialogText);
            answer = Console.ReadLine();
            return answer.ToUpper();
        }
        
        public void ClearSelection(int colStart, int linStart, int colEnd, int linEnd)
        {
            for(int x=colStart; x<=colEnd; x++) // x vertical, y horizontal
            {
                for (int y=linStart; y<=linEnd; y++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(" ");
                }
            }
        }
        
        public void DrawWindow(int colStart, int linStart, int colEnd, int linEnd)
        {
            int line, column;

            this.ClearSelection(colStart, linStart, colEnd, linEnd);

            // quina baixo esq
            Console.SetCursorPosition(colStart, linEnd);
            Console.Write('┗');

            // quina baixo dir
            Console.SetCursorPosition(colEnd, linEnd);
            Console.Write('┛');
            
            // quina cima esq
            Console.SetCursorPosition(colStart, linStart);
            Console.Write('┏');

            // quina cima dir
            Console.SetCursorPosition(colEnd, linStart);
            Console.Write('┓');
            
            // vertical
            for(line=linStart+1; line<linEnd; line++)
            {
                Console.SetCursorPosition(colStart, line);
                Console.Write("┃");
                Console.SetCursorPosition(colEnd, line);
                Console.Write("┃");
            }
            
            // horizontal - cima
            for(column=colStart+1; column<colEnd; column++) 
            {
                Console.SetCursorPosition(column, linStart);
                Console.Write('━');
            }

            // horizontal - baixo
            for(column=colStart+1; column<colEnd; column++) 
            {
                Console.SetCursorPosition(column, linEnd);
                Console.Write('━');
            }
        }
        
        public string ShowOptionsModal(int colStart, int linStart, List<string> optionsList)
        {
            string answer;
            int i;
            int colEnd = colStart + optionsList[0].Length + 1;
            int linEndModal = linStart + optionsList.Count() + 2;

            this.DrawWindow(colStart, linStart, colEnd, linEndModal);
            for(i=0; i<optionsList.Count; i++)
            {
                Console.SetCursorPosition(colStart+1, linStart+1+i);
                Console.Write(optionsList[i]);
            }
            Console.SetCursorPosition(colStart + 1, linStart + 1 + i);
            Console.Write(" Seleção: ");
            answer = Console.ReadLine().ToUpper();

            return answer;
        }
    }
}
