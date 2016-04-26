using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerUP
{
    class graphPoint
    {
        private int xValue;
        private int yValue;
        private int graphID;

        public graphPoint(int xValue, int yValue, int graphID)
        {
            this.XValue = xValue;
            this.YValue = yValue;
            this.graphID = graphID;
        }

        public int XValue
        {
            get
            {
                return xValue;
            }

            set
            {
                xValue = value;
            }
        }

        public int YValue
        {
            get
            {
                return yValue;
            }

            set
            {
                yValue = value;
            }
        }
    }
}
