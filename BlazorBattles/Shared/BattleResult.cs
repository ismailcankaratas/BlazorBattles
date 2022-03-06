﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBattles.Shared
{
    public class BattleResult
    {
        public IList<string> Log { get; set; } = new List<string>();
        public int AttackerDamgerSum { get; set; }
        public int OpponentDamgerSum { get; set; }
        public bool IsVictory { get; set; }
        public int RoundsFought { get; set; }
    }
}
