using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using System.IO;

namespace NetworkMonitor
{
    public partial class Form1 : MaterialForm
    {
        private Timer updateTimer;
        private HashSet<string> blockedPrograms;
        private MaterialSkinManager materialSkinManager;
        private const string BlockedProgramsFile = "blockedPrograms.txt";

        public Form1()
        {
            InitializeComponent();
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Blue600, Primary.Blue700,
                Primary.Blue200, Accent.LightBlue200,
                TextShade.WHITE
            );
            blockedPrograms = new HashSet<string>();
            LoadBlockedPrograms();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateProcessList();

            updateTimer = new Timer();
            updateTimer.Interval = 1000; // تحديث كل ثانية
            updateTimer.Tick += UpdateTimer_Tick;
            updateTimer.Start();
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            UpdateProcessData();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            UpdateProcessList();
        }

        private void btnBlock_Click(object sender, EventArgs e)
        {
            if (listViewProcesses.SelectedItems.Count > 0)
            {
                var processName = listViewProcesses.SelectedItems[0].Text;
                BlockInternet(processName);
                UpdateProcessList();
            }
        }

        private void btnUnblock_Click(object sender, EventArgs e)
        {
            if (listViewProcesses.SelectedItems.Count > 0)
            {
                var processName = listViewProcesses.SelectedItems[0].Text;
                UnblockInternet(processName);
                UpdateProcessList();
            }
        }

        private void btnUnblockAll_Click(object sender, EventArgs e)
        {
            UnblockAllInternet();
            UpdateProcessList();
        }

        private void btnToggleTheme_Click(object sender, EventArgs e)
        {
            if (materialSkinManager.Theme == MaterialSkinManager.Themes.LIGHT)
            {
                materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
                btnToggleTheme.Text = "Light Mode";
            }
            else
            {
                materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
                btnToggleTheme.Text = "Dark Mode";
            }
        }

        private void UpdateProcessList()
        {
            listViewProcesses.Items.Clear();
            imageList.Images.Clear();

            var processes = Process.GetProcesses();
            var processNames = processes.Select(p => p.ProcessName).Distinct();

            foreach (var processName in processNames)
            {
                var listViewItem = new ListViewItem(processName);

                try
                {
                    var process = Process.GetProcessesByName(processName).FirstOrDefault();
                    if (process != null)
                    {
                        var icon = Icon.ExtractAssociatedIcon(process.MainModule.FileName);
                        if (icon != null)
                        {
                            imageList.Images.Add(processName, icon.ToBitmap());
                            listViewItem.ImageKey = processName;
                        }
                    }
                }
                catch { }

                listViewItem.SubItems.Add("0 B"); // Placeholder for Bytes Received
                listViewItem.SubItems.Add("0 B"); // Placeholder for Bytes Sent
                listViewItem.SubItems.Add(blockedPrograms.Contains(processName) ? "Blocked" : "Unblocked"); // حالة البرنامج
                listViewProcesses.Items.Add(listViewItem);
            }
        }

        private void UpdateProcessData()
        {
            var tcpConnections = GetAllTcpConnections();
            var udpConnections = GetAllUdpConnections();

            long totalBytesReceived = 0;
            long totalBytesSent = 0;

            foreach (ListViewItem item in listViewProcesses.Items)
            {
                try
                {
                    var processName = item.Text;
                    var processIds = Process.GetProcessesByName(processName).Select(p => p.Id).ToList();

                    var tcpUsage = tcpConnections.Where(c => processIds.Contains(c.owningPid)).ToList();
                    var udpUsage = udpConnections.Where(c => processIds.Contains(c.owningPid)).ToList();

                    var bytesReceived = tcpUsage.Sum(c => c.localPort) + udpUsage.Sum(c => c.localPort);
                    var bytesSent = tcpUsage.Sum(c => c.remotePort);

                    item.SubItems[1].Text = FormatBytes(bytesReceived);
                    item.SubItems[2].Text = FormatBytes(bytesSent);

                    totalBytesReceived += bytesReceived;
                    totalBytesSent += bytesSent;
                }
                catch { }
            }

            lblTotalUpload.Text = $"Total Upload: {FormatBytes(totalBytesSent)}";
            lblTotalDownload.Text = $"Total Download: {FormatBytes(totalBytesReceived)}";
        }

        private string FormatBytes(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }
            return $"{len:0.##} {sizes[order]}";
        }

        private void BlockInternet(string processName)
        {
            var processes = Process.GetProcessesByName(processName);
            foreach (var process in processes)
            {
                string executablePath = GetExecutablePath(process.Id);
                if (!string.IsNullOrEmpty(executablePath))
                {
                    string command = $"netsh advfirewall firewall add rule name=\"Block {processName}\" dir=out program=\"{executablePath}\" action=block";
                    ExecuteCommand(command);
                }
            }
            blockedPrograms.Add(processName);
            SaveBlockedPrograms();
        }

        private void UnblockInternet(string processName)
        {
            string command = $"netsh advfirewall firewall delete rule name=\"Block {processName}\"";
            ExecuteCommand(command);
            blockedPrograms.Remove(processName);
            SaveBlockedPrograms();
        }

        private void UnblockAllInternet()
        {
            foreach (var processName in blockedPrograms.ToList())
            {
                UnblockInternet(processName);
            }
        }

        private void ExecuteCommand(string command)
        {
            ProcessStartInfo processInfo = new ProcessStartInfo("cmd.exe", "/C " + command);
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardOutput = true;
            processInfo.RedirectStandardError = true;

            using (Process process = Process.Start(processInfo))
            {
                process.OutputDataReceived += (sender, args) => Console.WriteLine(args.Data);
                process.BeginOutputReadLine();
                process.WaitForExit();
            }
        }

        private string GetExecutablePath(int processId)
        {
            string wmiQuery = $"SELECT ExecutablePath FROM Win32_Process WHERE ProcessId = {processId}";
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(wmiQuery))
            using (ManagementObjectCollection results = searcher.Get())
            {
                foreach (ManagementObject mo in results)
                {
                    return mo["ExecutablePath"]?.ToString();
                }
            }
            return null;
        }

        private void LoadBlockedPrograms()
        {
            if (File.Exists(BlockedProgramsFile))
            {
                var lines = File.ReadAllLines(BlockedProgramsFile);
                blockedPrograms = new HashSet<string>(lines);
            }
        }

        private void SaveBlockedPrograms()
        {
            File.WriteAllLines(BlockedProgramsFile, blockedPrograms);
        }

        [DllImport("iphlpapi.dll", SetLastError = true)]
        private static extern int GetExtendedTcpTable(IntPtr pTcpTable, ref int dwOutBufLen, bool sort, int ipVersion, int tblClass, int reserved);

        [DllImport("iphlpapi.dll", SetLastError = true)]
        private static extern int GetExtendedUdpTable(IntPtr pUdpTable, ref int dwOutBufLen, bool sort, int ipVersion, int tblClass, int reserved);

        [StructLayout(LayoutKind.Sequential)]
        public struct MIB_TCPROW_OWNER_PID
        {
            public uint state;
            public uint localAddr;
            public int localPort;
            public uint remoteAddr;
            public int remotePort;
            public int owningPid;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MIB_TCPTABLE_OWNER_PID
        {
            public uint dwNumEntries;
            public MIB_TCPROW_OWNER_PID table;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MIB_UDPROW_OWNER_PID
        {
            public uint localAddr;
            public int localPort;
            public int owningPid;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MIB_UDPTABLE_OWNER_PID
        {
            public uint dwNumEntries;
            public MIB_UDPROW_OWNER_PID table;
        }

        private const int AF_INET = 2;    // IP_v4
        private const int TCP_TABLE_OWNER_PID_ALL = 5;
        private const int UDP_TABLE_OWNER_PID = 1;

        private List<MIB_TCPROW_OWNER_PID> GetAllTcpConnections()
        {
            List<MIB_TCPROW_OWNER_PID> tcpConnections = new List<MIB_TCPROW_OWNER_PID>();

            int bufferSize = 0;
            GetExtendedTcpTable(IntPtr.Zero, ref bufferSize, true, AF_INET, TCP_TABLE_OWNER_PID_ALL, 0);
            IntPtr tcpTablePtr = Marshal.AllocHGlobal(bufferSize);

            try
            {
                int result = GetExtendedTcpTable(tcpTablePtr, ref bufferSize, true, AF_INET, TCP_TABLE_OWNER_PID_ALL, 0);
                if (result != 0)
                {
                    return tcpConnections;
                }

                MIB_TCPTABLE_OWNER_PID tcpTable = new MIB_TCPTABLE_OWNER_PID();
                tcpTable.dwNumEntries = (uint)Marshal.ReadInt32(tcpTablePtr);
                IntPtr rowPtr = (IntPtr)((long)tcpTablePtr + Marshal.SizeOf(tcpTable.dwNumEntries));
                for (int i = 0; i < tcpTable.dwNumEntries; i++)
                {
                    MIB_TCPROW_OWNER_PID tcpRow = (MIB_TCPROW_OWNER_PID)Marshal.PtrToStructure(rowPtr, typeof(MIB_TCPROW_OWNER_PID));
                    tcpConnections.Add(tcpRow);
                    rowPtr = (IntPtr)((long)rowPtr + Marshal.SizeOf(tcpRow));
                }
            }
            finally
            {
                Marshal.FreeHGlobal(tcpTablePtr);
            }

            return tcpConnections;
        }

        private List<MIB_UDPROW_OWNER_PID> GetAllUdpConnections()
        {
            List<MIB_UDPROW_OWNER_PID> udpConnections = new List<MIB_UDPROW_OWNER_PID>();

            int bufferSize = 0;
            GetExtendedUdpTable(IntPtr.Zero, ref bufferSize, true, AF_INET, UDP_TABLE_OWNER_PID, 0);
            IntPtr udpTablePtr = Marshal.AllocHGlobal(bufferSize);

            try
            {
                int result = GetExtendedUdpTable(udpTablePtr, ref bufferSize, true, AF_INET, UDP_TABLE_OWNER_PID, 0);
                if (result != 0)
                {
                    return udpConnections;
                }

                MIB_UDPTABLE_OWNER_PID udpTable = new MIB_UDPTABLE_OWNER_PID();
                udpTable.dwNumEntries = (uint)Marshal.ReadInt32(udpTablePtr);
                IntPtr rowPtr = (IntPtr)((long)udpTablePtr + Marshal.SizeOf(udpTable.dwNumEntries));
                for (int i = 0; i < udpTable.dwNumEntries; i++)
                {
                    MIB_UDPROW_OWNER_PID udpRow = (MIB_UDPROW_OWNER_PID)Marshal.PtrToStructure(rowPtr, typeof(MIB_UDPROW_OWNER_PID));
                    udpConnections.Add(udpRow);
                    rowPtr = (IntPtr)((long)rowPtr + Marshal.SizeOf(udpRow));
                }
            }
            finally
            {
                Marshal.FreeHGlobal(udpTablePtr);
            }

            return udpConnections;
        }

        private void lblDeveloperInfo_Click(object sender, EventArgs e)
        {

        }

        private void listViewProcesses_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
