using System.Collections.Generic;

namespace SalaShow.Model
{
    public class RoomModel
    {
        // atributos
        private int _codigo;
        private string _nome;
        private string _localizacao;
        private string _capacidade;
        private bool _ocupada;
        private List<string> _recursos;



        // prop
        public int Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }

        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        public string Localizacao
        {
            get { return _localizacao; }
            set { _localizacao = value; }
        }

        public string Capacidade
        {
            get { return _capacidade; }
            set { _capacidade = value; }
        }

        public bool Ocupada
        {
            get { return _ocupada; }
            set { _ocupada = value; }
        }

        public List<string> Recursos
        {
            get { return _recursos; }
            set { _recursos = value; }
        }
        
        public RoomModel()
        {
            this._codigo = 0;
            this._nome = "";
            this._localizacao = "";
            this._capacidade = "";
            this._ocupada = false;
            this._recursos = new List<string>();
        }
        
        public RoomModel(int codigoSala, string nomeSala, string localizacaoSala, string capacidadeSala, bool estaOcupada, List<string> recursosSala)
        {   
            this._codigo = codigoSala;
            this._nome = nomeSala;
            this._localizacao = localizacaoSala;
            this._capacidade = capacidadeSala;
            this._ocupada = estaOcupada;
            this._recursos = recursosSala;
        }
    }
}