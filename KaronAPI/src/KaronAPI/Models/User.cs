using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KaronAPI.Models
{
    public class User
    {
        public List<string> Plate { get; set; }
        public string Photo { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public Dictionary<DayOfWeek,string> PrefSegment { get; set; }
        public string Condition { get; set; }
    }
}
