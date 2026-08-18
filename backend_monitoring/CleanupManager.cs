using System;

namespace BackendMonitoring
{
    public class CleanupManager
    {
        public CleanupManager()
        {
            Console.WriteLine("[Monitoring] Cleanup manager initialized.");
        }

        ~CleanupManager()
        {
            Console.WriteLine("[Monitoring] Cleanup manager destroyed. Resources released.");
        }

        public void PerformCleanup()
        {
            Console.WriteLine("[Monitoring] Performing cleanup tasks...");
        }
    }
}
