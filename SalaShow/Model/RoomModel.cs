using System.Collections.Generic;

namespace SalaShow.Model
{
    public class RoomModel
    {
        // atributos
        private string _code;
        private string _name;
        private string _location;
        private string _capacity;
        private bool _busy;
        private List<string> _extras;



        // prop
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Location
        {
            get { return _location; }
            set { _location = value; }
        }

        public string Capacity
        {
            get { return _capacity; }
            set { _capacity = value; }
        }

        public bool Busy
        {
            get { return _busy; }
            set { _busy = value; }
        }

        public List<string> Extras
        {
            get { return _extras; }
            set { _extras = value; }
        }
        
        public RoomModel()
        {
            this._code = "";
            this._name = "";
            this._location = "";
            this._capacity = "";
            this._busy = false;
            this._extras = new List<string>();
        }
        
        public RoomModel(string roomCode, string roomName, string roomLocation, string roomCapacity, string roomBusy, List<string> roomExtras)
        {   
            this._code = roomCode;
            this._name = roomName;
            this._location = roomLocation;
            this._capacity = roomCapacity;
            this._busy = roomBusy == "S" ? true : false;
            this._extras = roomExtras;
        }
    }
}