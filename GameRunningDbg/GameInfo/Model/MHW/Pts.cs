using GameRunningDbg.GameInfo.Model;
using GameRunningDbg.GameInfo.Model.Base;
using HunterPie.Core.System.Windows.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GameRunningDbg.GameInfo.Model.MHW
{
    /// <summary>
    /// 怪猎调查点数
    /// </summary>
    public class Pts : MemoryBase<Pts>
    {
        public Pts(int[] offsets) : base(offsets)
        {
            Name = "调查点数";
        }
    }
}
