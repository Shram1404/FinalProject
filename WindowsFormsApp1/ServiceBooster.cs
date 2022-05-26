using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceProcess;

namespace WindowsFormsApp1
{
    internal class ServiceBooster
    {
        private bool _forStart = true;
        public void ServiceClose(string[] serviceName)
        {
            foreach (var service in serviceName)
                try
                {
                    ServiceController sc = new ServiceController(service);

                    if ((sc.Status.Equals(ServiceControllerStatus.Stopped)) || (sc.Status.Equals(ServiceControllerStatus.StopPending)))
                    {
                        _forStart = false;
                    }
                    else
                    {
                        sc.Stop();
                        _forStart = true;
                    }
                }
                catch (System.InvalidOperationException)
                {

                }
        }

        public void ServiceStart(string[] serviceName)
        {
            foreach (var service in serviceName)
            {
                try
                {
                    ServiceController st = new ServiceController(service);

                    if (_forStart == true)
                    {
                        st.Start();
                    }
                }
                catch (System.InvalidOperationException)
                {

                }
            }

        }
    }
}
