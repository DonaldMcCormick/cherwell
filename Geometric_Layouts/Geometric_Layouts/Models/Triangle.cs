using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geometric_Layouts.Models
{
    public class Triangle
    {
        public long Id { get; set; }

        public int X1 { get; set; }
        public int Y1 { get; set; }

        public int X2 { get; set; }
        public int Y2 { get; set; }

        public int X3 { get; set; }
        public int Y3 { get; set; }

        public string TriangleLocation { get; set; }
    }
}
