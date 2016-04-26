using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerUP
{
    internal class Project
    {
        private string description;
        private string startdato;
        private string slutdato;
        private int ID;
        private string name;

        public List<Iteration> iterations;

        public int ID1
        {
            get
            {
                return ID;
            }

            set
            {
                ID = value;
            }
        }

        public Project(int iD, string name)
        {
            this.ID = iD;
            this.name = name;
        }

        public Project(int iD, string name, string description, string startdato, string slutdato)
        {
            this.description = description;
            this.startdato = startdato;
            this.slutdato = slutdato;
            this.ID = iD;
            this.name = name;
            iterations = new List<Iteration>();
        }
    }
}
