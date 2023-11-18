using GameRunningDbg.JSON.Define.MHW;
using GameRunningDbg.Tool;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRunningDbg.Manager.MHW
{
    public class DataManager : Singleton<DataManager>
    {
        private string DataPath;
        public Dictionary<int, ItemDefine> itemDefine = null;
        public Dictionary<string, Dictionary<string, object>> offsets = null;

        public DataManager() 
        {
            //this.DataPath = "D:\\巨硬\\C#\\GameRunningDbg\\GameRunningDbg\\JSON\\";
            this.DataPath = "json\\";
            itemDefine = new Dictionary<int, ItemDefine>();
            offsets = new Dictionary<string, Dictionary<string, object>>();
        }

        public void Init()
        {
            Load();
        }

        public void Load()
        {
            string json = File.ReadAllText(this.DataPath+"怪物猎人世界道具.txt");
            itemDefine = JsonConvert.DeserializeObject<Dictionary<int, ItemDefine>>(json);
            Console.WriteLine($"道具json读取数量为 : {itemDefine.Count}");
            json = File.ReadAllText(this.DataPath + "offsets.txt");
            offsets = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(json);
        }
    }
}
