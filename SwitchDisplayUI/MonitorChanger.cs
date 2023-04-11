using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SwitchDisplayUI
{

    struct Monitor
    {
        public uint id;
        public string name;
    }

    internal class MonitorChanger
    {

        public List<Monitor> monitores;

        public List<Monitor> GetMonitorList()
        {
            List<Monitor> monitorList = new();
            Monitor monitor;
            uint Length = MonitorCount();
            for (uint i = 0; i < Length; i++)
            {
                monitor.id = i;
                monitor.name = MonitorName(i);
                monitorList.Add(monitor);
            }
            return monitorList;
        }


        [DllImport("C:\\Projetos\\VisualStudioProjects\\SwitchDisplay\\x64\\Release\\SwitchDisplay.dll", CharSet = CharSet.Auto)]
        private static extern void QueryDisplay(uint id, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder str_out);

        [DllImport("C:\\Projetos\\VisualStudioProjects\\SwitchDisplay\\x64\\Release\\SwitchDisplay.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetMonitorPrimary(uint id);

        [DllImport("C:\\Projetos\\VisualStudioProjects\\SwitchDisplay\\x64\\Release\\SwitchDisplay.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint GetMonitorPrimary(uint id);

        [DllImport("C:\\Projetos\\VisualStudioProjects\\SwitchDisplay\\x64\\Release\\SwitchDisplay.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint MonitorCount();

        public string MonitorName(uint id)
        {
            StringBuilder str = new StringBuilder();
            QueryDisplay(id, str);
            return str.ToString();
        }


    }
}
