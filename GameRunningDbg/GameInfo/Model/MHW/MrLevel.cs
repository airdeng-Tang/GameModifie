using GameRunningDbg.GameInfo.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameModifier.GameInfo.Model.MHW
{
    public class MrLevel : MemoryBase<MrLevel>
    {
        public MrLevel(int[] offset) : base(offset)
        {
            Name = "Mr";
        }
    }
}
