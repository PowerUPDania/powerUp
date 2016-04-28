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
        private int internIndex;

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

        public int InternIndex
        {
            get { return internIndex; }
            set { internIndex = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Type1
        {
            get { return type; }
            set { type = value; }
        }

        public string Startdato
        {
            get { return startdato; }
            set { startdato = value; }
        }

        public string Slutdato
        {
            get { return slutdato; }
            set { slutdato = value; }
        }

        public Iteration(int iD, string name, int projektfil, string type, int duration, string startdato, string slutdato, int internIndex)
        {
            this.ID = iD;
            this.name = name;
            this.projektfil = projektfil;
            this.type = type;
            this.duration = duration;
            this.startdato = startdato;
            this.slutdato = slutdato;
            this.internIndex = internIndex;
            graphs = new List<Graph>();
            MaxXValue = -1;
        }
    }
}
