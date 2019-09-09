using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace PlaneFlight
{

    public class PropertyEventArgs
    {
        public string mess;
        public PropertyEventArgs(string mess) { this.mess = mess; }
    }

}