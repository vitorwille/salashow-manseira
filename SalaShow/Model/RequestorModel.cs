namespace SalaShow.Model
{
    public class RequestorModel
    {
        private string _matricula;
        private string _nome;
        private string _setor;
        private string _telefone;

        public string Matricula
        {
            get { return this._matricula; }
            set { this._matricula = value; }
        }

        public string Nome
        {
            get { return this._nome; }
            set { this._nome = value; }
        }

        public string Setor
        {
            get { return this._setor; }
            set { this._setor = value; }
        }

        public string Telefone
        {
            get { return this._telefone; }
            set { this._telefone = value; }
        }   
        
        public RequestorModel()
        {
            this._matricula = "";
            this._nome = "";
            this._setor = "";
            this._telefone = "";
        }
        
        public RequestorModel(string matriculaSolicitante, string nomeSolicitante, string setorSolicitante, string telefoneSolicitante)
        {   
            this._matricula = matriculaSolicitante;
            this._nome = nomeSolicitante;
            this._setor = setorSolicitante;
            this._telefone = telefoneSolicitante;
        }
    }
}