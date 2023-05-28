using GameRunningDbg.Core;
using GameRunningDbg.Tool;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRunningDbg.Model
{
    public class ProcessModel : Singleton<ProcessModel>
    {
        public string name;
        public Process module;
        bool isRunning = false;

        public void Init()
        {
            isRunning = true;
        }

        //public process() { 
            
        //}
    }
}
