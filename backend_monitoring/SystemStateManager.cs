using System;
using System.IO;

namespace BackendMonitoring
{
    public class SystemStateManager
    {
        private string filePath = "system_state.txt";

        public void SaveState(string state)
        {
            File.WriteAllText(filePath, state);
            Console.WriteLine("[Monitoring] System state saved.");
        }

        public string LoadState()
        {
            if (File.Exists(filePath))
            {
                string state = File.ReadAllText(filePath);
                Console.WriteLine("[Monitoring] System state loaded.");
                return state;
            }
            else
            {
                Console.WriteLine("[Monitoring] No saved state found.");
                return string.Empty;
            }
        }
    }
}
