using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBattles.Shared
{
    public class Unit
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Attack { get; set; }
        public int Defanse { get; set; }
        public int HitPoints { get; set; } = 100;
        public int BananaConst { get; set; }
    }
}
