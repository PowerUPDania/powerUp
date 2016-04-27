using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerUP
{
    internal class Iteration
    {
        private int duration;
        private int iD;
        private string name;
        private int projektfil;
        private string type;
        private string startdato;
        private string slutdato;
        private int maxXValue;

        public List<Graph> graphs;

        public int ID
        {
            get
            {
                return iD;
            }

            set
            {
                iD = value;
            }
        }

        public int MaxXValue
        {
            get
            {
                return maxXValue;
            }

            set
            {
                maxXValue = value;
            }
        }

        public Iteration(int iD, string name, int projektfil, string type, int duration, string startdato, string slutdato)
        {
            this.ID = iD;
            this.name = name;
            this.projektfil = projektfil;
            this.type = type;
            this.duration = duration;
            this.startdato = startdato;
            this.slutdato = slutdato;
            graphs = new List<Graph>();
            MaxXValue = -1;
        }
    }
}
