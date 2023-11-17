using GameRunningDbg.GameInfo.Model.Base;
using GameRunningDbg.Tool;
using HunterPie.Core.System.Windows.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GameRunningDbg.GameInfo.Model
{
    public class Gold : MemoryBase<Gold>
    {
        public Gold(int[] offset) : base(offset)
        {
            Name = "金币";
        }
    }
}
