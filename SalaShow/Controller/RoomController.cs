using System;
using System.Collections.Generic;
using SalaShow.Model;

namespace SalaShow.Controller
{
    internal class RoomController
    {
        private int _column, _row, _width, _height, _position, _inputColumn;
        private View _window;
        private List<string> _fields;

        private RoomModel _roomModel;
        private List<RoomModel> _rooms;
        public List<RoomModel> Rooms { get { return this._rooms; } }
        
        public RoomController(int col, int row, View window)
        {
            this._column = col;
            this._row = row;
            this._window = window;
            this._fields = new List<string>{
                "────────────────────────────────────────────────────────────",
                " ID da sala: ",
                " Nº da sala: ",
                " Bloco: ",
                " Capacidade: ",
                " Ocupada? (s/n): ",
                " Recursos: "
            };

            int maxLabelLen = 0;
            for (int i = 1; i < this._fields.Count; i++)
                if (this._fields[i].Length > maxLabelLen) maxLabelLen = this._fields[i].Length;

            this._inputColumn = this._column + 1 + maxLabelLen;
            this._width = this._fields[0].Length + 2;
            this._height = this._fields.Count + 6;

            this._roomModel = new RoomModel();
            this._rooms = new List<RoomModel>();
            this._rooms.Add(new RoomModel("0", "Sala de Exemplo", "Bloco A", "20 pessoas", "S", new List<string>(){"Six", "Seven"}));
        }
        
        public void ShowRoom()
        {
            int row = this._row + 3;

            Console.SetCursorPosition(this._inputColumn, row);
            Console.Write(this._rooms[this._position].Code);
            row++;

            Console.SetCursorPosition(this._inputColumn, row);
            Console.Write(this._rooms[this._position].Name);
            row++;

            Console.SetCursorPosition(this._inputColumn, row);
            Console.Write(this._rooms[this._position].Location);
            row++;

            Console.SetCursorPosition(this._inputColumn, row);
            Console.Write(this._rooms[this._position].Capacity);
            row++;

            Console.SetCursorPosition(this._inputColumn, row);
            Console.Write(this._rooms[this._position].Busy ? "S" : "N");
            row++;

            Console.SetCursorPosition(this._inputColumn, row);
            string extras = string.Join(", ", this._rooms[this._position].Extras);
            int maxChars = this._width - (this._inputColumn - this._column) - 1;
            while (extras.Length > maxChars)
            {
                Console.Write(extras.Substring(0, maxChars));
                extras = extras.Substring(maxChars);
                row++;
                Console.SetCursorPosition(this._inputColumn, row);
            }
            Console.Write(extras);
        }

        public RoomModel FindRoom(string roomCode)
        {
            foreach (RoomModel room in _rooms)
            {
                if (this._rooms[this._rooms.IndexOf(room)].Code == roomCode)
                {
                    return this._rooms[this._rooms.IndexOf(room)];
                }
            }
            
            return null;
        }
        
        public void CRUD()
        {
            string answer;
            int colStart = this._column + 1;
            int colEnd = this._column + this._width - 1;
            int line = this._row + this._height - 1;

            do
            {
                this.ShowForm();
                answer = this._window.AskInput(" (N)ovo | (A)lterar | (C)onsultar | (E)xcluir | (V)oltar: ", line,
                    colStart, colEnd).ToUpper();
                this._window.ClearSelection(colStart, line, colEnd, line);

                if (answer == "V")
                {
                    break;
                }
                else if (answer == "N")
                {
                    this.ShowForm(true);
                    this.EnterData("ID");
                    this.EnterData("DATA");
                    answer = this._window.AskInput(" Confirma cadastro? (s/n): ", line, colStart, colEnd).ToUpper();
                    this._window.ClearSelection(colStart, line, colEnd, line);
                    if (answer == "S")
                    {
                        this._rooms.Add(
                            new RoomModel(this._roomModel.Code, this._roomModel.Name,
                                this._roomModel.Location, this._roomModel.Capacity,
                                this._roomModel.Busy ? "S" : "N", this._roomModel.Extras)
                        );
                    }

                    this.ShowForm();
                }
                else if (answer == "A" || answer == "C" || answer == "E")
                {
                    this.EnterData("ID");
                    RoomModel roomFoundModel = this.FindRoom(this._roomModel.Code);
                    bool roomFound = roomFoundModel != null;

                    if (answer == "A")
                    {
                        if (roomFound)
                        {
                            this._position = this._rooms.IndexOf(roomFoundModel);
                            this._roomModel = new RoomModel();
                            this.ShowForm(true);
                            this.ShowRoom();
                            this._window.AskInput(" Pressione Enter para editar a sala selecionada.", line, colStart, colEnd);
                            this._window.ClearSelection(colStart, line, colEnd, line);
                            this.ClearInputs();
                            this.EnterData("DATA");
                            answer = this._window.AskInput(" Confirma edição? (s/n): ", line, colStart, colEnd)
                                .ToUpper();
                            this._window.ClearSelection(colStart, line, colEnd, line);
                            if (answer == "S")
                            {
                                this._rooms[this._position].Name = this._roomModel.Name;
                                this._rooms[this._position].Location = this._roomModel.Location;
                                this._rooms[this._position].Capacity = this._roomModel.Capacity;
                                this._rooms[this._position].Busy = this._roomModel.Busy;
                                this._rooms[this._position].Extras = this._roomModel.Extras;
                            }
                        }
                        else
                        {
                            answer = this._window.AskInput(" Sala não encontrada. Deseja cadastrar? (s/n): ", line,
                                colStart, colEnd).ToUpper();
                            this._window.ClearSelection(colStart, line, colEnd, line);
                            if (answer == "S")
                            {
                                this.ShowForm(true);
                                this.EnterData("DATA");
                                answer = this._window.AskInput(" Confirma cadastro? (s/n): ", line, colStart, colEnd)
                                    .ToUpper();
                                this._window.ClearSelection(colStart, line, colEnd, line);
                                if (answer == "S")
                                {
                                    this._rooms.Add(
                                        new RoomModel(this._roomModel.Code, this._roomModel.Name,
                                            this._roomModel.Location, this._roomModel.Capacity,
                                            this._roomModel.Busy ? "S" : "N", this._roomModel.Extras)
                                    );
                                }
                            }
                        }

                        this.ShowForm();
                    }
                    else if (answer == "C")
                    {
                        if (roomFound)
                        {
                            this._position = this._rooms.IndexOf(roomFoundModel);
                            this.ShowForm(true);
                            this.ShowRoom();
                            this._window.AskInput(" Pressione Enter para voltar...", line, colStart, colEnd);
                            this._window.ClearSelection(colStart, line, colEnd, line);
                        }
                        else
                        {
                            this._window.AskInput(" Sala não encontrada. Pressione Enter para voltar.", line, colStart,
                                colEnd);
                            this.ShowForm();
                        }

                        this.ShowForm();
                    }
                    else if (answer == "E")
                    {
                        if (roomFound)
                        {
                            this._position = this._rooms.IndexOf(roomFoundModel);
                            this._roomModel = new RoomModel();
                            this.ShowForm(true);
                            this.ShowRoom();
                            answer = this._window.AskInput(" Confirma exclusão? (s/n): ", line, colStart, colEnd)
                                .ToUpper();
                            this._window.ClearSelection(colStart, line, colEnd, line);
                            if (answer == "S")
                            {
                                this._rooms.RemoveAt(this._position);
                            }
                        }
                        else
                        {
                            this._window.AskInput(" Sala não encontrada.", line, colStart, colEnd);
                            this._window.ClearSelection(colStart, line, colEnd, line);
                        }

                        this.ShowForm();
                    }
                }
                else
                {
                    this._window.AskInput(" Opção inválida! Pressione Enter para continuar.", line, colStart, colEnd);
                    this._window.ClearSelection(colStart, line, colEnd, line);
                }
            } while (answer != "V");
            this._roomModel = new RoomModel();
        }
        
        public void ShowForm(bool showAllFields = false)
        {
            this._window.DrawWindow(this._column, this._row, this._column + this._width, this._row + this._height);
            int row = this._row + 1;
            this._window.CenterWindow(this._column, this._column + this._width, row, "SalaShow - Gerenciar Salas");
            row++;

            Console.SetCursorPosition(this._column + 1, row);
            Console.Write(this._fields[0]);
            row++;

            Console.SetCursorPosition(this._column + 1, row);
            Console.Write(this._fields[1]);
            row++;

            if (showAllFields)
            {
                for (int i = 2; i < this._fields.Count; i++)
                {
                    Console.SetCursorPosition(this._column + 1, row);
                    Console.Write(this._fields[i]);
                    row++;
                }
            }
        }

        public void ClearInputs()
        {
            int colEnd = this._column + this._width - 1;
            this._window.ClearSelection(this._inputColumn, this._row + 4, colEnd, this._row + 2 + this._fields.Count - 1);
        }

        public void EnterData(string mode)
        {
            int inputRow = this._row + 4;

            if (mode == "ID")
            {
                Console.SetCursorPosition(this._inputColumn, this._row + 3);
                this._roomModel.Code = Console.ReadLine();
            }

            if (mode == "DATA")
            {
                Console.SetCursorPosition(this._inputColumn, inputRow);
                this._roomModel.Name = Console.ReadLine();
                inputRow++;

                Console.SetCursorPosition(this._inputColumn, inputRow);
                this._roomModel.Location = Console.ReadLine();
                inputRow++;

                Console.SetCursorPosition(this._inputColumn, inputRow);
                this._roomModel.Capacity = Console.ReadLine();
                inputRow++;

                Console.SetCursorPosition(this._inputColumn, inputRow);
                this._roomModel.Busy = Console.ReadLine().ToUpper() == "S";
                inputRow++;

                Console.SetCursorPosition(this._inputColumn, inputRow);
                this._roomModel.Extras = new List<string>(Console.ReadLine().Split(','));
            }
        }
    }
}