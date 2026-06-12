using System;
using System.Collections.Generic;
using SalaShow.Model;

namespace SalaShow.Controller
{
    internal class RequestorController
    {
        private int _column, _row, _width, _height, _position, _inputColumn;
        private View _window;
        private List<string> _fields;

        private RequestorModel _requestorModel;
        private List<RequestorModel> _requestors;
        public List<RequestorModel> Requestors { get { return this._requestors; } }

        public RequestorController(int col, int row, View window)
        {
            this._column = col;
            this._row = row;
            this._window = window;
            this._fields = new List<string>{
                "───────────────────────────────────────────────────────────",
                " ID do solicitante: ",
                " Nome: ",
                " Departamento: ",
                " Telefone: "
            };

            int maxLabelLen = 0;
            for (int i = 1; i < this._fields.Count; i++)
                if (this._fields[i].Length > maxLabelLen) maxLabelLen = this._fields[i].Length;

            this._inputColumn = this._column + 1 + maxLabelLen;
            this._width = this._fields[0].Length + 1;
            this._height = this._fields.Count + 6;

            this._requestorModel = new RequestorModel();
            this._requestors = new List<RequestorModel>();
            this._requestors.Add(new RequestorModel("0", "Solicitante Exemplo", "TI", "(47) 99999-9999"));
        }

        public void ShowRequestor()
        {
            int row = this._row + 3;

            Console.SetCursorPosition(this._inputColumn, row);
            Console.Write(this._requestors[this._position].Id);
            row++;

            Console.SetCursorPosition(this._inputColumn, row);
            Console.Write(this._requestors[this._position].Name);
            row++;

            Console.SetCursorPosition(this._inputColumn, row);
            Console.Write(this._requestors[this._position].Department);
            row++;

            Console.SetCursorPosition(this._inputColumn, row);
            Console.Write(this._requestors[this._position].Phone);
            row++;
        }

        public RequestorModel FindRequestor(string requestorId)
        {
            foreach (RequestorModel requestor in _requestors)
            {
                if (this._requestors[this._requestors.IndexOf(requestor)].Id == requestorId)
                {
                    return this._requestors[this._requestors.IndexOf(requestor)];
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
                        this._requestors.Add(
                            new RequestorModel(this._requestorModel.Id, this._requestorModel.Name,
                                this._requestorModel.Department, this._requestorModel.Phone)
                        );
                    }

                    this.ShowForm();
                }
                else if (answer == "A" || answer == "C" || answer == "E")
                {
                    this.EnterData("ID");
                    RequestorModel requestorFoundModel = this.FindRequestor(this._requestorModel.Id);
                    bool requestorFound = requestorFoundModel != null;

                    if (answer == "A")
                    {
                        if (requestorFound)
                        {
                            this._position = this._requestors.IndexOf(requestorFoundModel);
                            this._requestorModel = new RequestorModel();
                            this.ShowForm(true);
                            this.ShowRequestor();
                            this._window.AskInput(" Pressione Enter para editar o solicitante selecionado.", line, colStart, colEnd);
                            this._window.ClearSelection(colStart, line, colEnd, line);
                            this.ClearInputs();
                            this.EnterData("DATA");
                            answer = this._window.AskInput(" Confirma edição? (s/n): ", line, colStart, colEnd)
                                .ToUpper();
                            this._window.ClearSelection(colStart, line, colEnd, line);
                            if (answer == "S")
                            {
                                this._requestors[this._position].Name = this._requestorModel.Name;
                                this._requestors[this._position].Department = this._requestorModel.Department;
                                this._requestors[this._position].Phone = this._requestorModel.Phone;
                            }
                        }
                        else
                        {
                            answer = this._window.AskInput(" Solicitante não encontrado. Deseja cadastrar? (s/n): ", line,
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
                                    this._requestors.Add(
                                        new RequestorModel(this._requestorModel.Id, this._requestorModel.Name,
                                            this._requestorModel.Department, this._requestorModel.Phone)
                                    );
                                }
                            }
                        }

                        this.ShowForm();
                    }
                    else if (answer == "C")
                    {
                        if (requestorFound)
                        {
                            this._position = this._requestors.IndexOf(requestorFoundModel);
                            this.ShowForm(true);
                            this.ShowRequestor();
                            this._window.AskInput(" Pressione Enter para voltar...", line, colStart, colEnd);
                            this._window.ClearSelection(colStart, line, colEnd, line);
                        }
                        else
                        {
                            this._window.AskInput(" Solicitante não encontrado. Pressione Enter para voltar.", line, colStart,
                                colEnd);
                            this.ShowForm();
                        }

                        this.ShowForm();
                    }
                    else if (answer == "E")
                    {
                        if (requestorFound)
                        {
                            this._position = this._requestors.IndexOf(requestorFoundModel);
                            this._requestorModel = new RequestorModel();
                            this.ShowForm(true);
                            this.ShowRequestor();
                            answer = this._window.AskInput(" Confirma exclusão? (s/n): ", line, colStart, colEnd)
                                .ToUpper();
                            this._window.ClearSelection(colStart, line, colEnd, line);
                            if (answer == "S")
                            {
                                this._requestors.RemoveAt(this._position);
                            }
                        }
                        else
                        {
                            this._window.AskInput(" Solicitante não encontrado.", line, colStart, colEnd);
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
            this._requestorModel = new RequestorModel();
        }

        public void ShowForm(bool showAllFields = false)
        {
            this._window.DrawWindow(this._column, this._row, this._column + this._width, this._row + this._height);
            int row = this._row + 1;
            this._window.CenterWindow(this._column, this._column + this._width, row, "SalaShow - Gerenciar Solicitantes");
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
                this._requestorModel.Id = Console.ReadLine();
            }

            if (mode == "DATA")
            {
                Console.SetCursorPosition(this._inputColumn, inputRow);
                this._requestorModel.Name = Console.ReadLine();
                inputRow++;

                Console.SetCursorPosition(this._inputColumn, inputRow);
                this._requestorModel.Department = Console.ReadLine();
                inputRow++;

                Console.SetCursorPosition(this._inputColumn, inputRow);
                this._requestorModel.Phone = Console.ReadLine();
                inputRow++;
            }
        }
    }
}
