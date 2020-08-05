using ServiceMonitor.BL;
using System;
using System.ServiceProcess;
using System.Threading;
using System.Timers;

namespace ServiceMonitor.MonitorService
{
    public partial class Service : ServiceBase
    {
        private static System.Timers.Timer aTimer;

        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            aTimer = new System.Timers.Timer(1000);
            aTimer.Elapsed += new ElapsedEventHandler(StartProcess);
            aTimer.Enabled = true;
        }

        private static void StartProcess(object source, ElapsedEventArgs e)
        {
            //aTimer.Interval = 300000;

            var services = new ServiceMgt().GetAllWithDetails();
            foreach (var service in services)
            {
                if (service.LastUpdateTime.AddSeconds(service.Frequency) <= DateTime.UtcNow ||
                    service.GraceTime != null && service.LastUpdateTime.AddSeconds(service.GraceTime.Value) <= DateTime.UtcNow)
                {
                    Thread thread = new Thread(new ServiceStatusUpdateMgt().Process);
                    thread.Start(service);
                }
            }

        }


        protected override void OnStop()
        {
        }

        internal void onDebug()
        {
            OnStart(null);
        }


    }
}
