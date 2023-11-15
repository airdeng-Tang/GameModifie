using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRunningDbg.GameInfo.Base
{
    /// <summary>
    /// 内存信息基类
    /// </summary>
    public class MemoryBase
    {
        /// <summary>
        /// 内存偏移量列表
        /// </summary>
        public List<int> offsets;

        /// <summary>
        /// 内存地址
        /// </summary>
        public IntPtr p;

        private ProcessModule coinModule;
        /// <summary>
        /// 相关模块
        /// </summary>
        public ProcessModule CoinModule
        {
            get { return coinModule; }
            set
            {
                coinModule = value;
                CoinModule_p = coinModule.BaseAddress;
            }
        }

        /// <summary>
        /// 相关模块基址
        /// </summary>
        public IntPtr CoinModule_p;
        /// <summary>
        /// 本资源地址
        /// </summary>
        public IntPtr PlayerGolds_p;

        public MemoryBase(int[] offset) {
            offsets = new List<int>(offset);
        }
    }
}
