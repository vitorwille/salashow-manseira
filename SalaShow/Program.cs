using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SalaShow
{
  internal class Program
  {
    public static void Main(string[] args)
    {
      string answer;
      View window = new View(ConsoleColor.DarkBlue, ConsoleColor.Gray);
      List<string> optionsMenu = new List<string>();
      optionsMenu.Add("x - Sair     ");

      while (1 == 1)
      {
        window.PrepareWindow("Menu Principal", 5, 1, 50, 20);
        answer = window.ShowOptionsModal(5, 22, optionsMenu);
        switch (answer)
        {
          case "x":
            Console.Clear();
            Console.WriteLine("/!\\ Encerrando SalaShow...\n\n");
            Process.GetCurrentProcess().Kill();
            break;
        }
      }
    }
  }
}