using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Form1
{
    class ProcessBooster
    {
        public IOrderedEnumerable<Process> _processes = from proc in Process.GetProcesses(".")
                                                        orderby proc.Id
                                                        select proc;
        string processForBoostSt = "taskmgr";
        public void ShowProcesses()
        {
            foreach (var p in _processes)
            {
                Console.WriteLine(p.Id + " " + p.ProcessName);
            }
        }
        public void FoundProcessForBoost()
        {
            float processForBoost = 0;

            foreach (Process proc in Process.GetProcesses())
            {
                PerformanceCounter myAppCpu =
                 new PerformanceCounter(
                     "Process", "% Processor Time",proc.ProcessName, true);
                if (processForBoost < myAppCpu.NextValue())
                {
                    processForBoostSt = proc.ProcessName;
                }
            }
        }
    

        public void SetPriorityHigh()
        {
            Process[] processForOptimization = Process.GetProcessesByName(processForBoostSt);
            for (int i = 0; i < processForOptimization.Length; i++)
            {
                processForOptimization[i].PriorityClass = ProcessPriorityClass.High;
            }
        }
        public void SetPriorityDefault()
        {
            Process[] processForOptimization = Process.GetProcessesByName(processForBoostSt);
            for (int i = 0; i < processForOptimization.Length; i++)
            {
                processForOptimization[i].PriorityClass = ProcessPriorityClass.Normal;
            }
        }
    }
}
