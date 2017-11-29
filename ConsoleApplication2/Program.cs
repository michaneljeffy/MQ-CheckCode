using System;
using System.Net;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;
using System.Text;

namespace ConsoleApplication2
{
    class Program
    {
        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true,

        ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool ClosePrinter(IntPtr hPrinter);

       
        [DllImport("winspool.Drv", EntryPoint = "GetPrinterA", SetLastError = true, CharSet = CharSet.Ansi,

        ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]

        private static extern bool GetPrinter(IntPtr hPrinter, Int32 dwLevel,

         IntPtr pPrinter, Int32 dwBuf, out Int32 dwNeeded);


        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi,

          ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]

        private static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter,

             out IntPtr hPrinter, ref structPrinterDefaults pd);

        static void Main(string[] args)
        {
            //HttpClient _httpClient = HttpHelper.HttpClientInstance();

            // _httpClient.BaseAddress = new Uri("http://www.baidu.com");
            //while (true)
            //{
            //    HttpWebRequest request = WebRequestHelper.WebRequestInstance();
            //    request.KeepAlive = true;
            //    request.Timeout = int.MaxValue;

            //    Stream data = request.GetResponse().GetResponseStream();
            //    StreamReader sr = new StreamReader(data, Encoding.UTF8);
            //    string content = sr.ReadToEnd();
            //   Console.WriteLine(content);

            //content  = null;
            //  PrintServer myPrintServer = new PrintServer();

            // PrintQueueCollection myPrintQueues = myPrintServer.GetPrintQueues();
            // GetPrinterStatus("EPSON L310 Series (副本 2)");
            GetIP();

            GetIP2();

            GetIP3();

            GetLocalIP();

            DownLoadFile("http://static.in66.co/aW1hZ2VzLzIwMTcwODEzLzU5OGZiNjFmMWMzZmYuanBn","123");
            Console.ReadLine();
        }

        public static string GetPrinterStatus(string PrinterName)
        {
            int intValue = GetPrinterStatusInt(PrinterName);
            string strRet = string.Empty;
            switch (intValue)
            {
                case 0:
                    strRet = "准备就绪（Ready）";
                    break;
                case 0x00000200:
                    strRet = "忙(Busy）";
                    break;
                case 0x00400000:
                    strRet = "被打开（Printer Door Open）";
                    break;
                case 0x00000002:
                    strRet = "错误(Printer Error）";
                    break;
                case 0x0008000:
                    strRet = "初始化(Initializing）";
                    break;
                case 0x00000100:
                    strRet = "正在输入,输出（I/O Active）";
                    break;
                case 0x00000020:
                    strRet = "手工送纸（Manual Feed）";
                    break;
                case 0x00040000:
                    strRet = "无墨粉（No Toner）";
                    break;
                case 0x00001000:
                    strRet = "不可用（Not Available）";
                    break;
                case 0x00000080:
                    strRet = "脱机（Off Line）";
                    break;
                case 0x00200000:
                    strRet = "内存溢出（Out of Memory）";
                    break;
                case 0x00000800:
                    strRet = "输出口已满（Output Bin Full）";
                    break;
                case 0x00080000:
                    strRet = "当前页无法打印（Page Punt）";
                    break;
                case 0x00000008:
                    strRet = "塞纸（Paper Jam）";
                    break;
                case 0x00000010:
                    strRet = "打印纸用完（Paper Out）";
                    break;
                case 0x00000040:
                    strRet = "纸张问题（Page Problem）";
                    break;
                case 0x00000001:
                    strRet = "暂停（Paused）";
                    break;
                case 0x00000004:
                    strRet = "正在删除（Pending Deletion）";
                    break;
                case 0x00000400:
                    strRet = "正在打印（Printing）";
                    break;
                case 0x00004000:
                    strRet = "正在处理（Processing）";
                    break;
                case 0x00020000:
                    strRet = "墨粉不足（Toner Low）";
                    break;
                case 0x00100000:
                    strRet = "需要用户干预（User Intervention）";
                    break;
                case 0x20000000:
                    strRet = "等待（Waiting）";
                    break;
                case 0x00010000:
                    strRet = "热机中（Warming Up）";
                    break;
                default:
                    strRet = "未知状态（Unknown Status）";
                    break;
            }
            return strRet;
        }

        internal static int GetPrinterStatusInt(string PrinterName)
        {
            int intRet = 0;
            IntPtr hPrinter;
            structPrinterDefaults defaults = new structPrinterDefaults();

            if (OpenPrinter(PrinterName, out hPrinter, ref defaults))
            {
                int cbNeeded = 0;
                bool bolRet = GetPrinter(hPrinter, 2, IntPtr.Zero, 0, out cbNeeded);
                if (cbNeeded > 0)
                {
                    IntPtr pAddr = Marshal.AllocHGlobal((int)cbNeeded);
                    bolRet = GetPrinter(hPrinter, 2, pAddr, cbNeeded, out cbNeeded);
                    if (bolRet)
                    {
                        PRINTER_INFO_2 Info2 = new PRINTER_INFO_2();

                        Info2 = (PRINTER_INFO_2)Marshal.PtrToStructure(pAddr, typeof(PRINTER_INFO_2));

                        intRet = System.Convert.ToInt32(Info2.Status);
                    }
                    Marshal.FreeHGlobal(pAddr);
                }
                ClosePrinter(hPrinter);
            }

            return intRet;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal struct structPrinterDefaults
        {
            [MarshalAs(UnmanagedType.LPTStr)]
            public String pDatatype;
            public IntPtr pDevMode;
            [MarshalAs(UnmanagedType.I4)]
            public int DesiredAccess;
        };

       // 状态枚举
        [Flags]
        internal enum PrinterStatus
        {
            PRINTER_STATUS_BUSY = 0x00000200,
            PRINTER_STATUS_DOOR_OPEN = 0x00400000,
            PRINTER_STATUS_ERROR = 0x00000002,
            PRINTER_STATUS_INITIALIZING = 0x00008000,
            PRINTER_STATUS_IO_ACTIVE = 0x00000100,
            PRINTER_STATUS_MANUAL_FEED = 0x00000020,
            PRINTER_STATUS_NO_TONER = 0x00040000,
            PRINTER_STATUS_NOT_AVAILABLE = 0x00001000,
            PRINTER_STATUS_OFFLINE = 0x00000080,
            PRINTER_STATUS_OUT_OF_MEMORY = 0x00200000,
            PRINTER_STATUS_OUTPUT_BIN_FULL = 0x00000800,
            PRINTER_STATUS_PAGE_PUNT = 0x00080000,
            PRINTER_STATUS_PAPER_JAM = 0x00000008,
            PRINTER_STATUS_PAPER_OUT = 0x00000010,
            PRINTER_STATUS_PAPER_PROBLEM = 0x00000040,
            PRINTER_STATUS_PAUSED = 0x00000001,
            PRINTER_STATUS_PENDING_DELETION = 0x00000004,
            PRINTER_STATUS_PRINTING = 0x00000400,
            PRINTER_STATUS_PROCESSING = 0x00004000,
            PRINTER_STATUS_TONER_LOW = 0x00020000,
            PRINTER_STATUS_USER_INTERVENTION = 0x00100000,
            PRINTER_STATUS_WAITING = 0x20000000,
            PRINTER_STATUS_WARMING_UP = 0x00010000
        }

        [StructLayout(LayoutKind.Sequential)]

        struct PRINTER_INFO_2
        {

            [MarshalAs(UnmanagedType.LPStr)]
            public string pServerName;

            [MarshalAs(UnmanagedType.LPStr)]
            public string pPrinterName;

            [MarshalAs(UnmanagedType.LPStr)]
            public string pShareName;

            [MarshalAs(UnmanagedType.LPStr)]
            public string pPortName;

            [MarshalAs(UnmanagedType.LPStr)]
            public string pDriverName;

            [MarshalAs(UnmanagedType.LPStr)]
            public string pComment;

            [MarshalAs(UnmanagedType.LPStr)]
            public string pLocation;

            public IntPtr pDevMode;

            [MarshalAs(UnmanagedType.LPStr)]
            public string pSepFile;

            [MarshalAs(UnmanagedType.LPStr)]
            public string pPrintProcessor;

            [MarshalAs(UnmanagedType.LPStr)]
            public string pDatatype;

            [MarshalAs(UnmanagedType.LPStr)]
            public string pParameters;

            public IntPtr pSecurityDescriptor;

            public Int32 Attributes;

            public Int32 Priority;

            public Int32 DefaultPriority;

            public Int32 StartTime;

            public Int32 UntilTime;

            public Int32 Status;

            public Int32 cJobs;

            public Int32 AveragePPM;

        }

        public void WriteFileBlock(string filePath)
        {
            //filePath = Server.MapPath(filePath);
        }

        public static  void DownLoadFile(string fileUrl, string programID)
        {
            Stream stream = null;
            //var response = HttpContext.Current.Response;
           

            try
            {
               
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (stream!=null)
                {
                    stream.Close();
                }

                //response.Close();
            }
        }

        private static void GetIP()
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "ipconfig.exe";//设置程序名 
            cmd.StartInfo.Arguments = "/all"; //参数 
                                              //重定向标准输出 
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.CreateNoWindow = true;//不显示窗口（控制台程序是黑屏） 
                                                //cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;//暂时不明白什么意思 
                                                /* 
                                                收集一下 有备无患 
                                                关于:ProcessWindowStyle.Hidden隐藏后如何再显示？ 
                                                hwndWin32Host = Win32Native.FindWindow(null, win32Exinfo.windowsName); 
                                                Win32Native.ShowWindow(hwndWin32Host, 1); //先FindWindow找到窗口后再ShowWindow 
                                                */
            cmd.Start();
            string info = cmd.StandardOutput.ReadToEnd();
            cmd.WaitForExit();
            cmd.Close();
            Console.WriteLine(info);
        }

        private static void GetIP2()
        {
            string name = Dns.GetHostName();
            IPAddress[] ipadrlist = Dns.GetHostAddresses(name);
            foreach (IPAddress ipa in ipadrlist)
            {
                if (ipa.AddressFamily == AddressFamily.InterNetwork)
                    Console.WriteLine(ipa.ToString());
            }
        }

        private static void GetIP3()
        {
            System.Net.Sockets.TcpClient c = new System.Net.Sockets.TcpClient();
            c.Connect("www.baidu.com", 80);
            string ip = ((System.Net.IPEndPoint)c.Client.LocalEndPoint).Address.ToString();
            NetworkStream stream= c.GetStream();
            c.Close();
            Console.WriteLine(ip);
        }

        /// <summary> 
        /// 获取当前使用的IP 
        /// </summary> 
        /// <returns></returns> 
        public static string GetLocalIP()
        {
            string result = RunApp("route", "print", true);
            Match m = Regex.Match(result, @"0.0.0.0\s+0.0.0.0\s+(\d+.\d+.\d+.\d+)\s+(\d+.\d+.\d+.\d+)");
            if (m.Success)
            {
                Console.WriteLine(m.Groups[2].Value);
                return m.Groups[2].Value;
            }
            else
            {
                try
                {
                    System.Net.Sockets.TcpClient c = new System.Net.Sockets.TcpClient();
                    c.Connect("www.baidu.com", 80);
                    string ip = ((System.Net.IPEndPoint)c.Client.LocalEndPoint).Address.ToString();
                    c.Close();
                    return ip;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        /// <summary> 
        /// 获取本机主DNS 
        /// </summary> 
        /// <returns></returns> 
        public static string GetPrimaryDNS()
        {
            string result = RunApp("nslookup", "", true);
            Match m = Regex.Match(result, @"\d+\.\d+\.\d+\.\d+");
            if (m.Success)
            {
                return m.Value;
            }
            else
            {
                return null;
            }
        }
        /// <summary> 
        /// 运行一个控制台程序并返回其输出参数。 
        /// </summary> 
        /// <param name="filename">程序名</param> 
        /// <param name="arguments">输入参数</param> 
        /// <returns></returns> 
        public static string RunApp(string filename, string arguments, bool recordLog)
        {
            try
            {
                if (recordLog)
                {
                    Trace.WriteLine(filename + " " + arguments);
                }
                Process proc = new Process();
                proc.StartInfo.FileName = filename;
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.Arguments = arguments;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.UseShellExecute = false;
                proc.Start();
                using (System.IO.StreamReader sr = new System.IO.StreamReader(proc.StandardOutput.BaseStream, Encoding.Default))
                {
                    //string txt = sr.ReadToEnd(); 
                    //sr.Close(); 
                    //if (recordLog) 
                    //{ 
                    // Trace.WriteLine(txt); 
                    //} 
                    //if (!proc.HasExited) 
                    //{ 
                    // proc.Kill(); 
                    //} 
                    //上面标记的是原文，下面是我自己调试错误后自行修改的 
                    Thread.Sleep(100);  //貌似调用系统的nslookup还未返回数据或者数据未编码完成，程序就已经跳过直接执行 
                                        //txt = sr.ReadToEnd()了，导致返回的数据为空，故睡眠令硬件反应 
                    if (!proc.HasExited)  //在无参数调用nslookup后，可以继续输入命令继续操作，如果进程未停止就直接执行 
                    {    //txt = sr.ReadToEnd()程序就在等待输入，而且又无法输入，直接掐住无法继续运行 
                        proc.Kill();
                    }
                    string txt = sr.ReadToEnd();
                    sr.Close();
                    if (recordLog)
                        Trace.WriteLine(txt);
                    return txt;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                return ex.Message;
            }
        }
    }
}
