using System;
using System.Collections.Generic;
using System.Diagnostics;
using SalaShow.Controller;

namespace SalaShow
{
  internal class Program
  {
    public static void Main(string[] args)
    {
      View window = new View(ConsoleColor.DarkBlue, ConsoleColor.Gray);
      RoomController roomController = new RoomController(10,5, window);
      
      string answer;
      List<string> optionsMenu = new List<string>();
      optionsMenu.Add("1 - Registrar Reserva                              ");
      optionsMenu.Add("2 - Cancelar Reserva                               ");
      optionsMenu.Add("3 - Visualizar salas livres (por horário)          ");
      optionsMenu.Add("4 - Visualizar reservas em sala e data específicas ");
      optionsMenu.Add("───────────────────────────────────────────────────");
      optionsMenu.Add("8 - Gerenciar Usuários                             ");
      optionsMenu.Add("9 - Gerenciar Salas                                ");
      optionsMenu.Add("───────────────────────────────────────────────────");
      optionsMenu.Add("                                                   ");
      optionsMenu.Add("0 - Sair                                           ");
      optionsMenu.Add("                                                   ");

      while (1 == 1)
      {
        window.PrepareWindow("SalaShow - Menu Principal", 3, 1, 76, 22);
        answer = window.ShowOptionsModal(12+1, 5, optionsMenu);
        switch (answer)
        {
          case "0":
            Console.Clear();
            Console.WriteLine("\n/!\\ Encerrando SalaShow...\n\n");
            Process.GetCurrentProcess().Kill();
            break;
          case "9":
            Console.Clear();
            roomController.CRUD();
            break;
        }
      }
    }
  }
}