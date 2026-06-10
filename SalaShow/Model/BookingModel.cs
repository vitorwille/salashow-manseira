namespace SalaShow.Model
{
    public class BookingModel
    {
        private int _matrSolicitante;
        private int _codSala;
        private string _dataReserva;
        private string _horaInicioReserva;
        private string _horaFimReserva;
        
        public int MatrSolicitante
        {
            get { return _matrSolicitante; }
            set { _matrSolicitante = value; }
        }

        public int CodSala
        {
            get { return _codSala; }
            set { _codSala = value; }
        }
        
        public string DataReserva
        {
            get { return _dataReserva; }
            set { _dataReserva = value; }
        }

        public string HoraInicioReserva
        {
            get { return _horaInicioReserva; }
            set { _horaInicioReserva = value; }
        }

        public string HoraFimReserva
        {
            get { return _horaFimReserva; }
            set { _horaFimReserva = value; }
        }
        
        public BookingModel()
        {
            this._matrSolicitante = 0;
            this._codSala = 0;
            this._dataReserva = "";
            this._horaInicioReserva = "";
            this._horaFimReserva = "";
        }
        
        public BookingModel(int matriculaSolicitante, int codigoSala, string dataReserva, string horaInicio, string horaFim)
        {   
            this._matrSolicitante = matriculaSolicitante;
            this._codSala = codigoSala;
            this._dataReserva = dataReserva;
            this._horaInicioReserva = horaInicio;
            this._horaFimReserva = horaFim;
        }
    }
}