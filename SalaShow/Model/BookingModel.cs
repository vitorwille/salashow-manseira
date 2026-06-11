namespace SalaShow.Model
{
    public class BookingModel
    {
        private int _requestorId;
        private int _roomId;
        private string _appointmentDate;
        private string _appointmentTimeStart;
        private string _appointmentTimeEnd;
        
        public int RequestorId
        {
            get { return _requestorId; }
            set { _requestorId = value; }
        }

        public int RoomId
        {
            get { return _roomId; }
            set { _roomId = value; }
        }
        
        public string AppointmentDate
        {
            get { return _appointmentDate; }
            set { _appointmentDate = value; }
        }

        public string AppointmentTimeStart
        {
            get { return _appointmentTimeStart; }
            set { _appointmentTimeStart = value; }
        }

        public string AppointmentTimeEnd
        {
            get { return _appointmentTimeEnd; }
            set { _appointmentTimeEnd = value; }
        }
        
        public BookingModel()
        {
            this._requestorId = 0;
            this._roomId = 0;
            this._appointmentDate = "";
            this._appointmentTimeStart = "";
            this._appointmentTimeEnd = "";
        }
        
        public BookingModel(int requestorId, int roomId, string appointmentDate, string appointmentTimeStart, string appointmentTimeEnd)
        {   
            this._requestorId = requestorId;
            this._roomId = roomId;
            this._appointmentDate = appointmentDate;
            this._appointmentTimeStart = appointmentTimeStart;
            this._appointmentTimeEnd = appointmentTimeEnd;
        }
    }
}