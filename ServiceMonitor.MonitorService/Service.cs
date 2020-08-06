using ServiceMonitor.BL;
using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Threading;
using System.Timers;

namespace ServiceMonitor.MonitorService
{
    public partial class Service : ServiceBase
    {
        private static System.Timers.Timer aTimer;
        static List<Entity.Model.Service> services = new List<Entity.Model.Service>();
        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            aTimer = new System.Timers.Timer(1000);
            aTimer.Elapsed += new ElapsedEventHandler(StartProcess);
            aTimer.Enabled = true;
            services = new ServiceMgt().GetAllWithDetails(); // assign service list initially to avoid query db in every seconds

        }

        private static void StartProcess(object source, ElapsedEventArgs e)
        {

            foreach (var service in services)
            {
                if (service.LastUpdateTime.AddSeconds(service.Frequency) <= DateTime.UtcNow ||
                    service.GraceTime != null && service.LastUpdateTime.AddSeconds(service.GraceTime.Value) <= DateTime.UtcNow)
                {
                    // trigger check process if frequency or grace time as defined
                    service.LastUpdateTime = DateTime.UtcNow;
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
