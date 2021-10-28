using System;
using System.Collections.Generic;
using System.Text;

namespace Matchmaking
{
    class Giocatore
    {
        private string nome;
        private int rango;
        private List<Giocatore> giocatori = new List<Giocatore>();

        public void SetRango(int rango, int posizione)
        {
            giocatori[posizione].rango = rango;
        }

        public Giocatore(string nome, int rango)
        {
            this.nome = nome;
            this.rango = rango;
        }

        public Giocatore(string nome)
        {
            this.nome = nome;
        }

        public Giocatore()
        {

        }

        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        public int Rango
        {
            get { return rango; }
            set { rango = value; }
        }

        public List<Giocatore> Giocatori
        {
            get { return giocatori; }
            set { giocatori = value; }
        }

        internal Program Program
        {
            get => default;
            set
            {
            }
        }

        internal Coda Coda
        {
            get => default;
            set
            {
            }
        }
    }
}
