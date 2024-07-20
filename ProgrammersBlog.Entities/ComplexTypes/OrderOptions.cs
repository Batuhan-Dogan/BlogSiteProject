using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Entities.ComplexTypes
{
    public abstract class OrderOptions
    {
        public static string Default => "Default";
        public static string MostRead => "MostRead";
        public static string HotlyDebated => "HotlyDebated";
        public static string FirstAdded => "FirstAdded";
        public static string LastAdded => "LastAdded";
    }
}
