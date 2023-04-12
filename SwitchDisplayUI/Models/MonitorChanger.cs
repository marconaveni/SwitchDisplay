using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SwitchDisplayUI.Models
{

    struct Monitor
    {
        public uint id;
        public string name;
    }

    static class MonitorChanger
    {

        public static List<Monitor> GetMonitorList()
        {
            List<Monitor> monitores = new List<Monitor>();
            Monitor monitor;
            uint Length = MonitorCount();
            for (uint i = 0; i < Length; i++)
            {
                monitor.id = i;
                monitor.name = GetMonitorName(i);
                monitores.Add(monitor);
            }
            return monitores;
        }

        public static string GetMonitorName(uint id)
        {
            StringBuilder str = new StringBuilder();
            QueryDisplay(id, str);
            return str.ToString();
        }


        [DllImport("SwitchDisplay.dll", CharSet = CharSet.Auto)]
        private static extern void QueryDisplay(uint id, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder str_out);

        [DllImport("SwitchDisplay.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetMonitorPrimary(uint id);

        [DllImport("SwitchDisplay.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint GetMonitorPrimary();

        [DllImport("SwitchDisplay.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint MonitorCount();




    }
}
