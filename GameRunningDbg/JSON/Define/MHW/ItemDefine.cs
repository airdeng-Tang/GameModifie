using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRunningDbg.JSON.Define.MHW
{
    public class ItemDefine
    {
        /// <summary>
        /// 物品id
        /// </summary>
        public int Id {  get; set; }
        /// <summary>
        /// 物品名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 道具类型
        /// </summary>
        public ItemTarget Target { get; set; }
        /// <summary>
        /// 稀有度
        /// </summary>
        public string RarenessLevel {  get; set; }
        /// <summary>
        /// 物品介绍
        /// </summary>
        public string Description { get; set; }

    }
}
