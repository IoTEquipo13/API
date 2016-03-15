using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KaronAPI.ViewModel
{
    public class UserVM
    {
        public string Plate { get; set; }
        public string Photo { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public Dictionary<string, string> PrefSegment { get; set; }
        public string Condition { get; set; }
    }
}
