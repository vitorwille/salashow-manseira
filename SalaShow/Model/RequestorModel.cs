namespace SalaShow.Model
{
    public class RequestorModel
    {
        private string _id;
        private string _name;
        private string _department;
        private string _phone;

        public string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        public string Department
        {
            get { return this._department; }
            set { this._department = value; }
        }

        public string Phone
        {
            get { return this._phone; }
            set { this._phone = value; }
        }   
        
        public RequestorModel()
        {
            this._id = "";
            this._name = "";
            this._department = "";
            this._phone = "";
        }
        
        public RequestorModel(string requestorId, string requestorName, string requestorDepartment, string requestorPhone)
        {   
            this._id = requestorId;
            this._name = requestorName;
            this._department = requestorDepartment;
            this._phone = requestorPhone;
        }
    }
}