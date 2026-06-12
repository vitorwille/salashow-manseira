namespace SalaShow.Model
{
    public class BookingModel
    {
        private string _requestorId;
        private string _requestorName;
        private string _roomId;
        private string _appointmentDate;
        private string _appointmentTimeStart;
        private string _appointmentTimeEnd;
        
        public string RequestorId
        {
            get { return _requestorId; }
            set { _requestorId = value; }
        }

        public string RequestorName
        {
            get { return _requestorName; }
            set { _requestorName = value; }
        }

        public string RoomId
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
            this._requestorId = "";
            this._requestorName = "";
            this._roomId = "";
            this._appointmentDate = "";
            this._appointmentTimeStart = "";
            this._appointmentTimeEnd = "";
        }
        
        public BookingModel(string requestorId, string requestorName, string roomId, string appointmentDate, string appointmentTimeStart, string appointmentTimeEnd)
        {   
            this._requestorId = requestorId;
            this._requestorName = requestorName;
            this._roomId = roomId;
            this._appointmentDate = appointmentDate;
            this._appointmentTimeStart = appointmentTimeStart;
            this._appointmentTimeEnd = appointmentTimeEnd;
        }
    }
}