using System;
using System.Collections.Generic;
using SalaShow.Model;

namespace SalaShow.Controller
{
    internal class BookingController
    {
        private int _column, _row, _width, _height, _inputColumn;
        private View _window;
        private List<string> _fields;

        private BookingModel _bookingModel;
        private List<BookingModel> _bookings;
        private RoomController _roomController;
        private RequestorController _requestorController;
        public List<BookingModel> Bookings { get { return this._bookings; } }

        public BookingController(int col, int row, View window, RoomController roomController, RequestorController requestorController)
        {
            this._column = col;
            this._row = row;
            this._window = window;
            this._roomController = roomController;
            this._requestorController = requestorController;
            this._fields = new List<string>{
                "────────────────────────────────────────────────────────────────",
                " ID do solicitante: ",
                " ID da sala: ",
                " Data (dd/mm/aaaa): ",
                " Horário (HH:mm): ",
                " Horário término (HH:mm): "
            };

            int maxLabelLen = 0;
            for (int i = 1; i < this._fields.Count; i++)
                if (this._fields[i].Length > maxLabelLen) maxLabelLen = this._fields[i].Length;

            this._inputColumn = this._column + 1 + maxLabelLen;
            this._width = this._fields[0].Length + 1;
            this._height = this._fields.Count + 6;

            this._bookingModel = new BookingModel();
            this._bookings = new List<BookingModel>();
        }

        public BookingModel FindBooking(string requestorId, string roomId, string date, string startTime)
        {
            foreach (BookingModel bookingModel in _bookings)
            {
                if (bookingModel.RequestorId == requestorId && bookingModel.RoomId == roomId &&
                    bookingModel.AppointmentDate == date && bookingModel.AppointmentTimeStart == startTime)
                {
                    return bookingModel;
                }
            }

            return null;
        }

        public bool IsRoomAvailable(string roomId, string date, string startTime, string endTime)
        {
            foreach (BookingModel bookingModel in _bookings)
            {
                if (bookingModel.RoomId == roomId && bookingModel.AppointmentDate == date)
                {
                    if (!(endTime.CompareTo(bookingModel.AppointmentTimeStart) <= 0 || startTime.CompareTo(bookingModel.AppointmentTimeEnd) >= 0))
                    {
                        return false;
                    }
                }
            }
            return true;
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
                answer = this._window.AskInput(" (N)ovo | (C)onsultar | (E)xcluir | (V)oltar: ", line,
                    colStart, colEnd).ToUpper();
                this._window.ClearSelection(colStart, line, colEnd, line);

                if (answer == "V")
                {
                    break;
                }
                else if (answer == "N")
                {
                    this.ShowForm(-1);
                    this.EnterData("ID");
                    this.EnterData("DATA");

                    RequestorModel requestorModel = this._requestorController.FindRequestor(this._bookingModel.RequestorId);
                    if (requestorModel == null)
                    {
                        this._window.AskInput(" Solicitante não encontrado! Pressione Enter para continuar.", line,
                            colStart, colEnd);
                        this._window.ClearSelection(colStart, line, colEnd, line);
                        this.ShowForm();
                        continue;
                    }

                    this._bookingModel.RequestorName = requestorModel.Name;

                    RoomModel roomModel = this._roomController.FindRoom(this._bookingModel.RoomId);
                    if (roomModel == null)
                    {
                        this._window.AskInput(" Sala não encontrada! Pressione Enter para continuar.", line,
                            colStart, colEnd);
                        this._window.ClearSelection(colStart, line, colEnd, line);
                        this.ShowForm();
                        continue;
                    }

                    if (!this.IsRoomAvailable(this._bookingModel.RoomId, this._bookingModel.AppointmentDate,
                        this._bookingModel.AppointmentTimeStart, this._bookingModel.AppointmentTimeEnd))
                    {
                        this._window.AskInput(" Sala já reservada neste horário! Pressione Enter para continuar", line,
                            colStart, colEnd);
                        this._window.ClearSelection(colStart, line, colEnd, line);
                        this.ShowForm();
                        continue;
                    }

                    answer = this._window.AskInput(" Confirma reserva? (s/n): ", line, colStart, colEnd).ToUpper();
                    this._window.ClearSelection(colStart, line, colEnd, line);
                    if (answer == "S")
                    {
                        this._bookings.Add(
                            new BookingModel(this._bookingModel.RequestorId, this._bookingModel.RequestorName, this._bookingModel.RoomId,
                                this._bookingModel.AppointmentDate, this._bookingModel.AppointmentTimeStart,
                                this._bookingModel.AppointmentTimeEnd)
                        );

                        foreach (RoomModel roomModel2 in this._roomController.Rooms)
                        {
                            if (roomModel2.Code == this._bookingModel.RoomId)
                            {
                                roomModel2.Busy = true;
                                break;
                            }
                        }
                    }

                    this.ShowForm();
                }
                else if (answer == "C" || answer == "E")
                {
                    this.EnterData("ID");

                    List<BookingModel> matches = new List<BookingModel>();
                    foreach (BookingModel bookingModel in this._bookings)
                    {
                        if (bookingModel.RequestorId == this._bookingModel.RequestorId &&
                            bookingModel.RoomId == this._bookingModel.RoomId)
                        {
                            matches.Add(bookingModel);
                        }
                    }

                    if (matches.Count == 0)
                    {
                        string msg = " Reserva não encontrada. Pressione Enter para continuar.";
                        this._window.AskInput(msg, line, colStart, colEnd);
                        this._window.ClearSelection(colStart, line, colEnd, line);
                        this.ShowForm();
                        continue;
                    }

                    this.ShowForm(-1);

                    int displayRow = this._row + 3;
                    foreach (BookingModel bookingModel in matches)
                    {
                        Console.SetCursorPosition(colStart, displayRow);
                        Console.Write(bookingModel.RequestorId + " | " + bookingModel.RoomId + " | " + bookingModel.AppointmentDate +
                            " | " + bookingModel.AppointmentTimeStart + "-" + bookingModel.AppointmentTimeEnd + "  ");
                        displayRow++;
                    }

                    if (answer == "C")
                    {
                        this._window.AskInput(" Pressione Enter para voltar...", line, colStart, colEnd);
                        this._window.ClearSelection(colStart, line, colEnd, line);
                    }
                    else if (answer == "E")
                    {
                        answer = this._window.AskInput(" Digite a data (dd/mm/aaaa) da reserva a cancelar: ", line,
                            colStart, colEnd);
                        string cancelDate = answer;
                        this._window.ClearSelection(colStart, line, colEnd, line);

                        answer = this._window.AskInput(" Digite o horário de início da reserva a cancelar (HH:mm): ", line,
                            colStart, colEnd);
                        string cancelStart = answer;
                        this._window.ClearSelection(colStart, line, colEnd, line);

                        BookingModel toRemove = null;
                        foreach (BookingModel bookingModel in matches)
                        {
                            if (bookingModel.AppointmentDate == cancelDate && bookingModel.AppointmentTimeStart == cancelStart)
                            {
                                toRemove = bookingModel;
                                break;
                            }
                        }

                        if (toRemove != null)
                        {
                            string roomId = toRemove.RoomId;

                            answer = this._window.AskInput(" Confirma cancelamento? (s/n): ", line, colStart, colEnd)
                                .ToUpper();
                            this._window.ClearSelection(colStart, line, colEnd, line);

                            if (answer == "S")
                            {
                                this._bookings.Remove(toRemove);

                                bool hasOtherBookings = false;
                                foreach (BookingModel bookingModel in this._bookings)
                                {
                                    if (bookingModel.RoomId == roomId)
                                    {
                                        hasOtherBookings = true;
                                        break;
                                    }
                                }

                                if (!hasOtherBookings)
                                {
                                    foreach (RoomModel roomModel in this._roomController.Rooms)
                                    {
                                        if (roomModel.Code == roomId)
                                        {
                                            roomModel.Busy = false;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            this._window.AskInput(" Reserva não encontrada. Pressione Enter para continuar.",
                                line, colStart, colEnd);
                            this._window.ClearSelection(colStart, line, colEnd, line);
                        }
                    }

                    this.ShowForm();
                }
                else
                {
                    this._window.AskInput(" Opção inválida! Pressione Enter para continuar.", line, colStart, colEnd);
                    this._window.ClearSelection(colStart, line, colEnd, line);
                }
            } while (answer != "V");
            this._bookingModel = new BookingModel();
        }

        public void ShowForm(int showFields = -1)
        {
            this._window.DrawWindow(this._column, this._row, this._column + this._width, this._row + this._height);
            int row = this._row + 1;
            this._window.CenterWindow(this._column, this._column + this._width, row, "SalaShow - Gerenciar Reservas");
            row++;

            Console.SetCursorPosition(this._column + 1, row);
            Console.Write(this._fields[0]);
            row++;

            int max = showFields < 0 ? this._fields.Count : 1 + showFields;
            for (int i = 1; i < this._fields.Count && i < max; i++)
            {
                Console.SetCursorPosition(this._column + 1, row);
                Console.Write(this._fields[i]);
                row++;
            }
        }

        public void ClearInputs()
        {
            int colEnd = this._column + this._width - 1;
            this._window.ClearSelection(this._inputColumn, this._row + 4, colEnd, this._row + 2 + this._fields.Count - 1);
        }

        public void EnterData(string mode)
        {
            int inputRow = this._row + 5;

            if (mode == "ID")
            {
                Console.SetCursorPosition(this._inputColumn, this._row + 3);
                this._bookingModel.RequestorId = Console.ReadLine();

                Console.SetCursorPosition(this._inputColumn, this._row + 4);
                this._bookingModel.RoomId = Console.ReadLine();
            }

            if (mode == "DATA")
            {
                Console.SetCursorPosition(this._inputColumn, inputRow);
                this._bookingModel.AppointmentDate = Console.ReadLine();
                inputRow++;

                Console.SetCursorPosition(this._inputColumn, inputRow);
                this._bookingModel.AppointmentTimeStart = Console.ReadLine();
                inputRow++;

                Console.SetCursorPosition(this._inputColumn, inputRow);
                this._bookingModel.AppointmentTimeEnd = Console.ReadLine();
                inputRow++;
            }
        }

        public void RegisterBooking()
        {
            string answer;
            int colStart = this._column + 1;
            int colEnd = this._column + this._width - 1;
            int line = this._row + this._height - 1;

            this.ShowForm(-1);
            this.EnterData("ID");
            this.EnterData("DATA");

            RequestorModel requestorModel = this._requestorController.FindRequestor(this._bookingModel.RequestorId);
            if (requestorModel == null)
            {
                this._window.AskInput(" Solicitante não encontrado! Pressione Enter para continuar.", line,
                    colStart, colEnd);
                this._window.ClearSelection(colStart, line, colEnd, line);
                return;
            }

            this._bookingModel.RequestorName = requestorModel.Name;

            RoomModel roomModel = this._roomController.FindRoom(this._bookingModel.RoomId);
            if (roomModel == null)
            {
                this._window.AskInput(" Sala não encontrada! Pressione Enter para continuar.", line,
                    colStart, colEnd);
                this._window.ClearSelection(colStart, line, colEnd, line);
                return;
            }

            if (!this.IsRoomAvailable(this._bookingModel.RoomId, this._bookingModel.AppointmentDate,
                this._bookingModel.AppointmentTimeStart, this._bookingModel.AppointmentTimeEnd))
            {
                this._window.AskInput(" Sala já reservada neste horário! Pressione Enter para continuar", line,
                    colStart, colEnd);
                this._window.ClearSelection(colStart, line, colEnd, line);
                return;
            }

            answer = this._window.AskInput(" Confirma reserva? (s/n): ", line, colStart, colEnd).ToUpper();
            this._window.ClearSelection(colStart, line, colEnd, line);
            if (answer == "S")
            {
                this._bookings.Add(
                    new BookingModel(this._bookingModel.RequestorId, this._bookingModel.RequestorName, this._bookingModel.RoomId,
                        this._bookingModel.AppointmentDate, this._bookingModel.AppointmentTimeStart,
                        this._bookingModel.AppointmentTimeEnd)
                );

                foreach (RoomModel roomModel2 in this._roomController.Rooms)
                {
                    if (roomModel2.Code == this._bookingModel.RoomId)
                    {
                        roomModel2.Busy = true;
                        break;
                    }
                }

                this._window.AskInput(" Reserva registrada com sucesso! Pressione Enter para continuar.", line,
                    colStart, colEnd);
                this._window.ClearSelection(colStart, line, colEnd, line);
            }

            this._bookingModel = new BookingModel();
        }

        public void CancelBooking()
        {
            int colStart = this._column + 1;
            int colEnd = this._column + this._width - 1;
            int line = this._row + this._height - 1;

            this.ShowForm(2);
            this.EnterData("ID");

            List<BookingModel> matches = new List<BookingModel>();
            foreach (BookingModel bookingModel in this._bookings)
            {
                if (bookingModel.RequestorId == this._bookingModel.RequestorId &&
                    bookingModel.RoomId == this._bookingModel.RoomId)
                {
                    matches.Add(bookingModel);
                }
            }

            if (matches.Count == 0)
            {
                this._window.AskInput(" Reserva não encontrada. Pressione Enter para continuar.",
                    line, colStart, colEnd);
                this._window.ClearSelection(colStart, line, colEnd, line);
                this._bookingModel = new BookingModel();
                return;
            }

            this._window.ClearSelection(colStart, this._row + 3, colEnd, this._row + 8);

            int displayRow = this._row + 3;
            Console.SetCursorPosition(colStart, displayRow);
            Console.Write("Reservas encontradas:");
            displayRow++;

            foreach (BookingModel bookingModel in matches)
            {
                Console.SetCursorPosition(colStart, displayRow);
                Console.Write(bookingModel.AppointmentDate + " | " + bookingModel.AppointmentTimeStart + "-" + bookingModel.AppointmentTimeEnd);
                displayRow++;
            }

            string answer = this._window.AskInput(" Digite a data (dd/mm/aaaa) da reserva a cancelar: ", line,
                colStart, colEnd);
            string cancelDate = answer;
            this._window.ClearSelection(colStart, line, colEnd, line);

            answer = this._window.AskInput(" Digite o horário de início (HH:mm): ", line, colStart, colEnd);
            string cancelStart = answer;
            this._window.ClearSelection(colStart, line, colEnd, line);

            BookingModel toRemove = null;
            foreach (BookingModel bookingModel in matches)
            {
                if (bookingModel.AppointmentDate == cancelDate && bookingModel.AppointmentTimeStart == cancelStart)
                {
                    toRemove = bookingModel;
                    break;
                }
            }

            if (toRemove != null)
            {
                string roomId = toRemove.RoomId;

                answer = this._window.AskInput(" Confirma cancelamento? (s/n): ", line, colStart, colEnd)
                    .ToUpper();
                this._window.ClearSelection(colStart, line, colEnd, line);

                if (answer == "S")
                {
                    this._bookings.Remove(toRemove);

                    bool hasOtherBookings = false;
                    foreach (BookingModel bookingModel in this._bookings)
                    {
                        if (bookingModel.RoomId == roomId)
                        {
                            hasOtherBookings = true;
                            break;
                        }
                    }

                    if (!hasOtherBookings)
                    {
                        foreach (RoomModel roomModel in this._roomController.Rooms)
                        {
                            if (roomModel.Code == roomId)
                            {
                                roomModel.Busy = false;
                                break;
                            }
                        }
                    }

                    this._window.AskInput(" Reserva cancelada com sucesso! Pressione Enter para continuar.", line,
                        colStart, colEnd);
                    this._window.ClearSelection(colStart, line, colEnd, line);
                }
            }
            else
            {
                this._window.AskInput(" Reserva não encontrada. Pressione Enter para continuar.",
                    line, colStart, colEnd);
                this._window.ClearSelection(colStart, line, colEnd, line);
            }

            this._bookingModel = new BookingModel();
        }

        public void ShowFreeRooms()
        {
            int colStart = this._column + 1;
            int colEnd = this._column + this._width - 1;
            int line = this._row + this._height - 1;

            this.ShowForm(0);
            this._window.CenterWindow(this._column, this._column + this._width, this._row + 2,
                "Informe os dados para consulta:");

            Console.SetCursorPosition(colStart, this._row + 3);
            Console.Write("Data (dd/mm/aaaa): ");
            string date = Console.ReadLine();

            Console.SetCursorPosition(colStart, this._row + 4);
            Console.Write("Horário início (HH:mm): ");
            string startTime = Console.ReadLine();

            Console.SetCursorPosition(colStart, this._row + 5);
            Console.Write("Horário término (HH:mm): ");
            string endTime = Console.ReadLine();

            List<RoomModel> freeRooms = new List<RoomModel>();
            foreach (RoomModel roomModel in this._roomController.Rooms)
            {
                if (this.IsRoomAvailable(roomModel.Code, date, startTime, endTime))
                {
                    freeRooms.Add(roomModel);
                }
            }

            this._window.ClearSelection(colStart, this._row + 3, colEnd, this._row + 5);
            int displayRow = this._row + 3;

            if (freeRooms.Count == 0)
            {
                Console.SetCursorPosition(colStart, displayRow);
                Console.Write("Nenhuma sala livre neste horário.                          ");
            }
            else
            {
                Console.SetCursorPosition(colStart, displayRow);
                Console.Write("Salas livres:                                             ");
                displayRow++;

                foreach (RoomModel roomModel in freeRooms)
                {
                    Console.SetCursorPosition(colStart, displayRow);
                    Console.Write(roomModel.Code + " - " + roomModel.Name + " (" + roomModel.Capacity + ")           ");
                    displayRow++;
                }
            }

            this._window.AskInput(" Pressione Enter para voltar.", line, colStart, colEnd);
            this._window.ClearSelection(colStart, line, colEnd, line);
        }

        public void ShowBookingsByRoomAndDate()
        {
            int colStart = this._column + 1;
            int colEnd = this._column + this._width - 1;
            int line = this._row + this._height - 1;

            this.ShowForm(0);

            Console.SetCursorPosition(colStart, this._row + 3);
            Console.Write("ID da sala: ");
            string roomId = Console.ReadLine();

            Console.SetCursorPosition(colStart, this._row + 4);
            Console.Write("Data (dd/mm/aaaa): ");
            string date = Console.ReadLine();

            List<BookingModel> matches = new List<BookingModel>();
            foreach (BookingModel bookingModel in this._bookings)
            {
                if (bookingModel.RoomId == roomId && bookingModel.AppointmentDate == date)
                {
                    matches.Add(bookingModel);
                }
            }

            this._window.ClearSelection(colStart, this._row + 3, colEnd, this._row + 4);
            int displayRow = this._row + 3;

            if (matches.Count == 0)
            {
                Console.SetCursorPosition(colStart, displayRow);
                Console.Write("Nenhuma reserva encontrada para esta sala/data.                ");
            }
            else
            {
                Console.SetCursorPosition(colStart, displayRow);
                Console.Write("Reservas:                                                    ");
                displayRow++;

                foreach (BookingModel bookingModel in matches)
                {
                    Console.SetCursorPosition(colStart, displayRow);
                    Console.Write(bookingModel.AppointmentTimeStart + "-" + bookingModel.AppointmentTimeEnd +
                        " | Solicitante: " + bookingModel.RequestorName + "                         ");
                    displayRow++;
                }
            }

            this._window.AskInput(" Pressione Enter para voltar.", line, colStart, colEnd);
            this._window.ClearSelection(colStart, line, colEnd, line);
        }
    }
}
