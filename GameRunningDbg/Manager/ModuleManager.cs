using GameRunningDbg.Tool;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRunningDbg.Manager
{
    public class ModuleManager : Singleton<ModuleManager>
    {
        public Dictionary<string, ProcessModule> modules;

        public void Init()
        {
            modules = new Dictionary<string, ProcessModule>();
        }
    }
}
