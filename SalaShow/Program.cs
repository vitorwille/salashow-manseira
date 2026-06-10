using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SalaShow
{
  internal class Program
  {
    public static void Main(string[] args)
    {
      string escolha;
      View janela = new View(ConsoleColor.DarkBlue, ConsoleColor.Gray);
      List<string> opcMenu = new List<string>();
      opcMenu.Add("x - Sair     ");

      while (1 == 1)
      {
        janela.PrepararJanela("Menu Principal", 5, 1, 50, 20);
        escolha = janela.MostrarJanelaOpcoes(5, 22, opcMenu);
        switch (escolha)
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