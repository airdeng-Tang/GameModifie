using GameRunningDbg.GameInfo.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameModifier.GameInfo.Model.MHW
{
    public class HrLevel : MemoryBase<HrLevel>
    {
        public HrLevel(int[] offset) : base(offset)
        {
            Name = "Hr";
        }
    }
}
