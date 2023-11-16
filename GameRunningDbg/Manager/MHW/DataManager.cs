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
        public DataManager() 
        {
            this.DataPath = "D:\\巨硬\\C#\\GameRunningDbg\\GameRunningDbg\\JSON\\";
            itemDefine = new Dictionary<int, ItemDefine>();
        }

        public void Init()
        {
            Load();
        }

        public void Load()
        {
            string json = File.ReadAllText(this.DataPath+"冰原素材ID.txt");
            itemDefine = JsonConvert.DeserializeObject<Dictionary<int, ItemDefine>>(json);
            Console.WriteLine($"道具json读取数量为 : {itemDefine.Count}");
        }
    }
}
