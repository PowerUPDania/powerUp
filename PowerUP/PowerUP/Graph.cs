using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerUP
{
    internal class Graph
    {
        private int iD;
        private int iterationID;
        private string name;
        private int maxXValue = -1;

        public List<graphPoint> pointCollection;

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

        public Graph(int iD, string name, int iterationID)
        {
            this.ID = iD;
            this.name = name;
            this.iterationID = iterationID;
            pointCollection = new List<graphPoint>();
        }
    }
}
