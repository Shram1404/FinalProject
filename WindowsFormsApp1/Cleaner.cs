using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceProcess;
using System.IO;

namespace WindowsFormsApp1
{
    internal class Cleaner
    {
        public void CleanTemp()
        {
            int delDay = 1;

            string[] files = Directory.GetFiles(@"C:\Windows\Temp");
            foreach (string file in files)
            {
                FileInfo fi = new FileInfo(file);
                if (fi.CreationTime < DateTime.Now.AddDays(-delDay))
                {
                    try
                    {
                        fi.Delete();
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }
    }
}
