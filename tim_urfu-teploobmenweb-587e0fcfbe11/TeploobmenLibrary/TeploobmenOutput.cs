using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeploobmenLibrary
{
    public class TeploobmenOutput
    {
        public List<TeploobmenOutputRow> Rows { get; set; }
    }

    public class TeploobmenOutputRow
    {
        public double Y { get; set; }

        public double RelativeHeight { get; set; }

        public double Intermediate1 { get; set; }

        public double Intermediate2 { get; set; }

        public double V { get; set; }

        public double RelativeTempGas { get; set; }

        public double temp { get; set; }

        public double Temp { get; set; }

        public double Difference { get; set; }

    }
}
