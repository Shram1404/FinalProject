using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Threading;
using Form1;

namespace WindowsFormsApp1
{
    public class PCInfo
    {
        static ManagementClass myManagementClass = new ManagementClass("Win32_Processor");
        ManagementObjectCollection myManagementCollection = myManagementClass.GetInstances();
        PropertyDataCollection myProperties = myManagementClass.Properties;
        Dictionary<string, object> myPropertyResults = new Dictionary<string, object>();

        ManagementObjectSearcher search = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_VideoController");

        public string _CPU { get; set; }
        public int _Cores { get; set; }
        public int _LogicalCores { get; set; }

        public string[] _GPU = new string[2];
        public long[] _GPUMemory = new long[2];

        public ulong _Memory;

        public void ShowPCInfo()
        {
            GetPCInfo();

            Console.WriteLine("Cores " + _Cores);
            Console.WriteLine("Logical Cores " + _LogicalCores);
            
            foreach (string gpu in _GPU)
                Console.WriteLine("GPU " + gpu);
            foreach (int gpu in _GPUMemory)
                Console.WriteLine("GPU memory " + gpu);

            Console.WriteLine("Memory " + _Memory);

        }
        public void GetPCInfo()
        {
            GetCPUInfo();
            GetGPUInfo();
            GetMemoryInfo();
        }
        private void GetCPUInfo()
        {
            foreach (var obj in myManagementCollection)
            {
                foreach (var myProperty in myProperties)
                {
                    myPropertyResults.Add(myProperty.Name,
                        obj.Properties[myProperty.Name].Value);
                }
            }

            foreach (var myProperty in myProperties)
            {
                _CPU = myPropertyResults["Name"].ToString();
                _Cores = int.Parse(myPropertyResults["NumberOfCores"].ToString());
                _LogicalCores = Environment.ProcessorCount;
            }
        }
        private void GetGPUInfo()
        {
            var i = 0;

            foreach (ManagementObject queryObj in search.Get())
            {
                _GPU[i] = queryObj["Caption"].ToString();
                var gpuRamTemp = queryObj["AdapterRAM"];
                _GPUMemory[i] = Convert.ToInt64(gpuRamTemp) / 1000000;
                i++;
            }
        }

        private void GetMemoryInfo()
        {
            _Memory = new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory;
            _Memory /= 1000000;
        }
    }
}
