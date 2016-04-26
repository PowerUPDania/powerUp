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
        private int xValue;
        private int yValue;

        List<graphPoint> pointCollection;

        public Graph(int iD, string name, int iterationID)
        {
            this.iD = iD;
            this.name = name;
            this.iterationID = iterationID;
            pointCollection = new List<graphPoint>();
        }
    }
}
