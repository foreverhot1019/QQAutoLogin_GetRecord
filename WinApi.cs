using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsAPI
{
    /// <summary>
    /// WinApi调用
    /// </summary>
    public class WinApi
    {
        /// <summary>
        /// Hook过程 委托
        /// </summary>
        /// <param name="nCode">Hook代码</param>
        /// <param name="wParam"></param>
        /// <param name="iParam"></param>
        /// <returns></returns>
        public delegate int HookProc(Int32 nCode, IntPtr wParam, IntPtr iParam);

        /// <summary>
        /// 安装钩子
        /// </summary>
        /// <param name="idHook"></param>
        /// <param name="lpfm"></param>
        /// <param name="hInstance"></param>
        /// <param name="threadId"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 SetWindowsHookEx(Int32 idHook, HookProc lpfm, IntPtr hInstance, Int32 threadId);

        /// <summary>
        /// 卸载钩子
        /// </summary>
        /// <param name="idHook"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 UnhookWindowsHookEx(Int32 idHook);

        /// <summary>
        /// 下一个钩子
        /// </summary>
        /// <param name="idHook"></param>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="iParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 CallNextHookEx(Int32 idHook, Int32 nCode, IntPtr wParam, IntPtr iParam);

        /// <summary>
        /// 鼠标移动点击等动作
        /// </summary>
        /// <param name="dwFlags">底部参数有详细解释</param>
        /// <param name="dx">指定鼠标沿x轴的绝对位置或者从上次鼠标事件产生以来移动的数量，依赖于MOUSEEVENTF_ABSOLUTE的设置。给出的绝对数据作为鼠标的实际X坐标；给出的相对数据作为移动的mickeys数。一个mickey表示鼠标移动的数量，表明鼠标已经移动。</param>
        /// <param name="dy">指定鼠标沿y轴的绝对位置或者从上次鼠标事件产生以来移动的数量，依赖于MOUSEEVENTF_ABSOLUTE的设置。给出的绝对数据作为鼠标的实际y坐标，给出的相对数据作为移动的mickeys数。</param>
        /// <param name="cButtons">如果dwFlags为MOUSEEVENTF_WHEEL，则dwData指定鼠标轮移动的数量。正值表明鼠标轮向前转动，即远离用户的方向；负值表明鼠标轮向后转动，即朝向用户。一个轮击定义为WHEEL_DELTA，即120。如果dwFlagsS不是MOUSEEVENTF_WHEEL，则dWData应为零。</param>
        /// <param name="dwExtraInfo">指定与鼠标事件相关的附加32位值。应用程序调用函数GetMessageExtraInfo来获得此附加信息。</param>
        /// <returns></returns>
        [DllImport("user32")]
        public static extern int mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        /// <summary>
        /// 模拟键盘操作
        /// </summary>
        /// <param name="Keybd_vk">按键的虚拟键值</param>
        /// <param name="dwData">扫描码，一般不用设置，用0代替就行</param>
        /// <param name="dwFlags">选项标志，如果为keydown则置0即可，底部参数有详细解释(mouse_event注释)</param>
        /// <param name="dwExtraInfo">一般设为0</param>
        /// <returns></returns>
        [DllImport("user32")]
        public static extern void keybd_event(int Keybd_vk, int bScan, int dwFlags, int dwExtraInfo);
        [DllImport("user32")]
        public static extern void keybd_event(Keys key, int bScan, int dwFlags, int dwExtraInfo);

        /// <summary>
        /// 根据鼠标位置获取窗体
        /// </summary>
        /// <param name="lpPoint"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(Point lpPoint);

        /// <summary>
        /// 获取鼠标位置
        /// </summary>
        /// <param name="lpPoint"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int GetCursorPos(out Point lpPoint);

        /// <summary>
        /// 设置鼠标的位置、把鼠标移动到指定的位置
        /// </summary>
        /// <param name="lpPoint"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int SetCursorPos(Point lpPoint);

        /// <summary>
        /// 获取桌面的句柄
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();

        /// <summary>
        /// 获取鼠标位置下的窗体
        /// </summary>
        /// <returns></returns>
        public static IntPtr GetLocalWindow()
        {
            Point point;
            GetCursorPos(out point);
            return WindowFromPoint(point);
        }

        /// <summary>
        /// 返回父窗口中包含了指定点的第一个子窗口的句柄 hWndParent:
        /// 备注：系统有一个与某一父窗口有联系的所有子窗口的内部列表。列表中的句柄顺序依据这些子窗口的Z序。如果有多于一个的子窗口包含该点，那么系统返回在列表中包含该点并且满足由uFlags定义的规则的第一个窗口的句柄。
        /// </summary>
        /// <param name="pHwnd">父窗口句柄</param>
        /// <param name="pt">指定一个POINT结构，该结构定义了被检查的点的坐标</param>
        /// <param name="uFlgs">指明忽略的子窗口的类型。该参数可以是下列参数的组合</param>
        /// <returns>返回值为包含该点并且满足由uFlags定义的规则的第一个子窗口的句柄。如果该点在父窗口内，但在任一满足条件的子窗口外，则返回值为父窗口句柄。如果该点在父窗口之外或函数失败，则返回值为NULL。</returns>
        [DllImport("user32.dll")]
        public static extern IntPtr ChildWindowFromPointEx(IntPtr pHwnd, LPPOINT pt, uint uFlgs);

        /// <summary>
        /// 获取窗口大小及位置
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="IpRect"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, ref RECT IpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;//最左坐标
            public int Top;//最上坐标
            public int Right;//最右坐标
            public int Bottom;//最下坐标
            public RECT(int Left, int Top, int Right, int Bottom)
            {
                this.Left = Left;
                this.Top = Top;
                this.Right = Right;
                this.Bottom = Bottom;
            }

            public RECT(System.Drawing.Rectangle rectangle)
            {
                Left = rectangle.Left;
                Top = rectangle.Top;
                Right = rectangle.Right;
                Bottom = rectangle.Bottom;
            }

            public RECT(System.Drawing.Point location, System.Drawing.Size size)
            {
                Left = location.X;
                Top = location.Y;
                Right = location.X + size.Width;
                Bottom = location.Y + size.Height;
            }
        }

        /// <summary>
        /// 把屏幕坐标转化成相对当前窗体的坐标  
        /// </summary>
        /// <param name="hWnd">指向窗口的句柄，此窗口的用户空间将被用来转换</param>
        /// <param name="lpPoint">返回相对当前窗体的坐标</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool ScreenToClient(IntPtr hWnd, out LPPOINT lpPoint);

        /// <summary>
        /// 把当前窗体的坐标转换为屏幕坐标
        /// </summary>
        /// <param name="hWnd">用户区域用于转换的窗口句柄</param>
        /// <param name="lpPoint">返回屏幕坐标</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool ClientToScreen(IntPtr hWnd, out LPPOINT lpPoint);

        //要转换的坐标信息的结构体
        [StructLayout(LayoutKind.Sequential)]
        public struct LPPOINT
        {
            public int x;
            public int y;
            public LPPOINT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        /// <summary>
        /// 窗体是否可见
        /// </summary>
        /// <param name="lpClassName"></param>
        /// <param name="lpWindowName"></param>
        /// <returns></returns>
        [DllImport("User32.dll", EntryPoint = "IsWindowVisible")]
        public static extern int IsWindowVisible(int hWnd);

        /// <summary>
        /// 获取窗体名称长度
        /// </summary>
        /// <param name="lpClassName"></param>
        /// <param name="lpWindowName"></param>
        /// <returns></returns>
        [DllImport("User32.dll", EntryPoint = "GetWindowTextLength")]
        public static extern int GetWindowTextLength(int hWnd);

        /// <summary>
        /// 获取窗体名称长度
        ///SW_FORCEMINIMIZE：在WindowNT5.0中最小化窗口，即使拥有窗口的线程被挂起也会最小化。在从其他线程最小化窗口时才使用这个参数。
        ///SW_MIOE：隐藏窗口并激活其他窗口。
        ///SW_MAXIMIZE：最大化指定的窗口。
        ///SW_MINIMIZE：最小化指定的窗口并且激活在Z序中的下一个顶层窗口。
        ///SW_RESTORE：激活并显示窗口。如果窗口最小化或最大化，则系统将窗口恢复到原来的尺寸和位置。在恢复最小化窗口时，应用程序应该指定这个标志。
        ///SW_SHOW：在窗口原来的位置以原来的尺寸激活和显示窗口。
        ///SW_SHOWDEFAULT：依据在STARTUPINFO结构中指定的SW_FLAG标志设定显示状态，STARTUPINFO 结构是由启动应用程序的程序传递给CreateProcess函数的。
        ///SW_SHOWMAXIMIZED：激活窗口并将其最大化。
        ///SW_SHOWMINIMIZED：激活窗口并将其最小化。
        ///SW_SHOWMINNOACTIVATE：窗口最小化，激活窗口仍然维持激活状态。
        ///SW_SHOWNA：以窗口原来的状态显示窗口。激活窗口仍然维持激活状态。
        ///SW_SHOWNOACTIVATE：以窗口最近一次的大小和状态显示窗口。激活窗口仍然维持激活状态。
        ///SW_SHOWNOMAL：激活并显示一个窗口。如果窗口被最小化或最大化，系统将其恢复到原来的尺寸和大小。应用程序在第一次显示窗口的时候应该指定此标志。
        /// 返回值：如果窗口以前可见，则返回值为非零。如果窗口以前被隐藏，则返回值为零。
        /// 备注：应用程序第一次调用ShowWindow时，应该使用WinMain函数的nCmdshow参数作为它的nCmdShow参数。在随后调用ShowWindow函数时，必须使用列表中的一个给定值，而不是由WinMain函数的nCmdSHow参数指定的值。
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="nCmdShow">0：关闭窗口；1：正常大小显示窗口；2：最小化窗口；3：最大化窗口</param>
        /// <returns></returns>
        [DllImport("User32.dll", EntryPoint = "ShowWindow")]
        public static extern int ShowWindow(IntPtr hWnd, uint nCmdShow);

        /// <summary>
        /// 激活窗口
        /// </summary>
        /// <param name="lpClassName"></param>
        /// <param name="lpWindowName"></param>
        /// <returns></returns>
        [DllImport("User32.dll", EntryPoint = "SetActiveWindow")]
        public static extern IntPtr SetActiveWindow(IntPtr hWnd);

        /// <summary>
        /// 将窗口放在最前端
        /// </summary>
        /// <param name="lpClassName"></param>
        /// <param name="lpWindowName"></param>
        /// <returns></returns>
        [DllImport("User32.dll", EntryPoint = "SetForegroundWindow")]
        public static extern IntPtr SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// 根据类/标题查找窗口
        /// </summary>
        /// <param name="lpClassName"></param>
        /// <param name="lpWindowName"></param>
        /// <returns></returns>
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// 在窗口列表中寻找与指定条件相符的第一个子窗口
        /// </summary>
        /// <param name="hwndParent">父窗口句柄，如果hwndParent为 0 ，则函数以桌面窗口为父窗口，查找桌面窗口的所有子窗口。</param>
        /// <param name="hwndChildAfter">子窗口句柄，查找从在Z序中的下一个子窗口开始。子窗口必须为hwndParent窗口的直接子窗口而非后代窗口。如果HwndChildAfter为NULL，查找从hwndParent的第一个子窗口开始。如果hwndParent 和 hwndChildAfter同时为NULL，则函数查找所有的顶层窗口及消息窗口。</param>
        /// <param name="lpszClass">控件类名</param>
        /// <param name="lpszWindow">控件标题，如果该参数为 NULL，则为所有窗口全匹配。</param>
        /// <returns>控件句柄</returns>
        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        //回掉函数（EnumChildWindows 参数）
        public delegate bool CallBack(IntPtr hwnd, int lParam);
        /// <summary>
        /// 枚举一个父窗口的所有子窗口
        /// 注意：回调函数的返回值将会影响到这个API函数的行为。如果回调函数返回true，则枚举继续直到枚举完成；如果返回false，则将会中止枚举。
        /// 其中CallBack是这样的一个委托：public delegate bool CallBack(IntPtr hwnd, int lParam); 如果 CallBack 返回的是true，则会继续枚举，否则就会终止枚举。
        /// </summary>
        /// <param name="hWndParent">父窗口句柄</param>
        /// <param name="lpfn">回调函数的地址</param>
        /// <param name="lParam">自定义的参数</param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "EnumChildWindows")]
        public static extern int EnumChildWindows(IntPtr hWndParent, CallBack lpfn, int lParam);

        /// <summary>
        /// 发送windows消息
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="Msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("User32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// 发送windows消息
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="Msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, string lParam);

        /// <summary>
        /// 发送windows消息
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="wMsg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, string lParam);

        /// <summary>
        /// 发送windows消息
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="wMsg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        /// <summary>
        /// 发送windows消息
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="wMsg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, System.Text.StringBuilder lParam);

        /// <summary>
        /// 取得一个窗体的标题（caption）文字，或者一个控件的内容
        /// </summary>
        /// <param name="hwnd">窗口句柄</param>
        /// <param name="lpString">接收文本的缓冲区的指针</param>
        /// <param name="cch">指定缓冲区大小, 其中包含NULL字符; 如果文本超出，会被被截断</param>
        /// <returns>返回字符个数, 不包括中断的空字符; 如果标题为空或句柄无效, 则返回零</returns>
        [DllImport("user32.dll", EntryPoint = "GetWindowText")]
        public static extern int GetWindowText(IntPtr hwnd, System.Text.StringBuilder lpString, int cch);

        /// <summary>
        /// 为指定的窗口取得类名
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="lpClassName"></param>
        /// <param name="nMaxCount"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "GetClassName")]
        public static extern int GetClassName(IntPtr hwnd, System.Text.StringBuilder lpClassName, int nMaxCount);

        /// <summary>
        /// 该函数能在显示与隐藏窗口时产生两种特殊类型的动画效果：滚动动画和滑动动画。
        /// </summary>
        /// <param name="hwnd">指定产生动画的窗口的句柄。</param>
        /// <param name="dwTime">指明动画持续的时间（以微秒计），完成一个动画的标准时间为200微秒。</param>
        /// <param name="dwFags">指定动画类型。这个参数可以是一个或多个下列标志的组合。标志描述： </param>
        /// <returns></returns>
        /// 如果：
        /// 1、窗口使用了窗口边界；
        /// 2、窗口已经可见仍要显示窗口；
        /// 3、窗口已经隐藏仍要隐藏窗口。
        /// 函数将失败。
        [DllImport("user32.dll")]
        public static extern void AnimateWindow(IntPtr hwnd, int dwTime, int dwFags);

        /// <summary>
        /// 在窗口缩放时产生动画效果
        /// </summary>
        /// <param name="hwnd">窗口的Handle</param>
        /// <param name="idAni">动画效果标记</param>
        /// <param name="lprcFrom">起始窗口矩形</param>
        /// <param name="lprcTo">结束时窗口矩形</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool DrawAnimatedRects(System.IntPtr hwnd, int idAni, ref RECT lprcFrom, ref RECT lprcTo);

        /// <summary>
        /// 该函数改变一个子窗口，弹出式窗口式顶层窗口的尺寸，位置和Z序。 　　
        /// 子窗口，弹出式窗口，及顶层窗口根据它们在屏幕上出现的顺序排序、顶层窗口设置的级别最高，并且被设置为Z序的第一个窗口。
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="hWndAfter">在z序中的位于被置位的窗口前的窗口句柄。该参数必须为一个窗口句柄，或下列值之一</param>
        /// <param name="x">以客户坐标指定窗口新位置的左边界。</param>
        /// <param name="y">以客户坐标指定窗口新位置的顶边界。</param>
        /// <param name="cx">以像素指定窗口的新的宽度。</param>
        /// <param name="cy">以像素指定窗口的新的高度。</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndAfter, int x, int y, int cx, int cy, uint uflags);

        #region 注册热键

        /// <summary>
        /// 如果函数执行成功，返回值不为0。
        /// 如果函数执行失败，返回值为0。要得到扩展错误信息，调用GetLastError。
        /// </summary>
        /// <param name="hWnd">要定义热键的窗口的句柄</param>
        /// <param name="id">定义热键ID（不能与其它ID重复）</param>
        /// <param name="fsModifiers">标识热键是否在按Alt、Ctrl、Shift、Windows等键时才会生效</param>
        /// <param name="vk"></param>
        /// <returns></return定义热键的内容s>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, KeyModifiers fsModifiers, Keys vk);

        /// <summary>
        /// 如果函数执行成功，返回值不为0。            
        /// 如果函数执行失败，返回值为0。要得到扩展错误信息，调用GetLastError。
        /// </summary>
        /// <param name="hWnd">要定义热键的窗口的句柄</param>
        /// <param name="id">定义热键ID（不能与其它ID重复）</param>
        /// <param name="fsModifiers">标识热键是否在按Alt、Ctrl、Shift、Windows等键时才会生效</param>
        /// <param name="vk">定义热键的内容</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, Keys vk);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        /// <summary>
        /// 撤销热键
        /// </summary>
        /// <param name="hWnd">要取消热键的窗口的句柄</param>
        /// <param name="id">/要取消热键的ID</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        //定义了辅助键的名称（将数字转变为字符以便于记忆，也可去除此枚举而直接使用数值）
        [Flags()]
        public enum KeyModifiers
        {
            None = 0,
            Alt = 1,
            Ctrl = 2,
            Shift = 4,
            WindowsKey = 8
        }

        #endregion

        /// <summary>
        /// 获取当前线程编号
        /// </summary>
        /// <returns></returns>
        [DllImport("Kernel32.dll")]
        public static extern Int32 GetCurrentThreadId();

        /// <summary>
        /// 获取函数指针
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        [DllImport("Kernel32.dll")]
        public static extern IntPtr GetModuleHandle(string Name);

        #region 内存操作

        /// <summary>
        /// 申请内存空间
        /// </summary>
        /// <param name="hProcess"></param>
        /// <param name="lpAddress"></param>
        /// <param name="dwSize"></param>
        /// <param name="flAllocationType"></param>
        /// <param name="flProtect"></param>
        /// <returns></returns>
        [DllImport("Kernel32.dll")]
        public static extern Int32 VirtualAllocEx(IntPtr hProcess, Int32 lpAddress, Int32 dwSize, Int16 flAllocationType, Int16 flProtect);

        /// <summary>
        /// 读取内存空间
        /// </summary>
        /// <param name="hProcess"></param>
        /// <param name="lpBaseAddress"></param>
        /// <param name="lpBuffer"></param>
        /// <param name="nSize"></param>
        /// <param name="lpNumberOfBytesWritten"></param>
        /// <returns></returns>
        [DllImport("Kernel32.dll")]
        public static extern int ReadProcessMemory(IntPtr hProcess, Int32 lpBaseAddress, byte[] lpBuffer, long nSize, long lpNumberOfBytesWritten);

        /// <summary>
        /// 写内存空间
        /// </summary>
        /// <param name="hProcess"></param>
        /// <param name="lpBaseAddress"></param>
        /// <param name="lpBuffer"></param>
        /// <param name="nSize"></param>
        /// <param name="lpNumberOfBytesWritten"></param>
        /// <returns></returns>
        [DllImport("Kernel32.dll")]
        public static extern int WriteProcessMemory(IntPtr hProcess, Int32 lpBaseAddress, byte[] lpBuffer, long nSize, long lpNumberOfBytesWritten);

        #endregion

        #region Clipboard 剪贴板操作
        
//        /// <summary>
//        /// 打开剪切板
//        /// 每次只允许一个进程打开并访问。每打开一次就要关闭，否则其他进程无法访问剪切板。
//        /// </summary>
//        /// <param name="hWndNewOwner">指定关联到打开的剪切板的窗口句柄，传入NULL表示关联到当前任务。</param>
//        /// <returns></returns>
//        [DllImport("user32.dll")]
//        public static extern bool OpenClipboard(IntPtr hWndNewOwner);
         
//        /// <summary>
//        /// 关闭剪切板
//        /// </summary>
//        [DllImport("user32.dll")]
//        public static extern bool CloseClipboard();

//        /// <summary>
//        /// 清空剪切板内容
//        /// </summary>
//        [DllImport("user32.dll")]
//        public static extern bool EmptyClipboard ();
        
///// <summary>
//        /// 分配内存
//        /// </summary>
//        [DllImport("user32.dll")]
//        public static extern HGLOBAL GlobalAlloc(UINT uFlags, SIZE_T dwBytes);
//  在堆上动态分配以字节为单位的内存区域。成功则指向该内存，失败NULL。参数：1.分配内存属性， 2.分配的大小
//      锁定内存
//LPVOID GlobalLock(HGLOBAL hMem);
//  锁定由GlobalAlloc分配的内存，并将内存对象的锁定计数器+1，成功返回指向内存对象起始地址的指针。失败NULL

//系统为每个全局内存对象维护一个锁定计数器，初始为0，GlobalLock使计数器+1，GlobalUnLock计数器-1.一旦计数器值大于0，

//这块内存区域将不允许被移动或删除，只有当为0时，才解除对这块内存的锁定。如果分配时GMEM_FIXED属性，计数器一直为0

//GetClipboardData 获取剪切板内容
//SetClipboardData 设置剪切板内容
//IsClipboardFormatAvailable 判断剪切板的内容是否为某种格式
//CountClipboardFormats 获取剪切板当前内容有多少种类型
//EnumClipboardFormats 枚举剪切板中的格式
//GetClipboardFormatName 获取剪切板的格式的名称
//GetPriorityClipboardFormat 获取在一个列表下某个索引的剪切板格式
//RegisterClipboardFormat 注册(追加)剪切板格式
//高级应用下使用的函数（做剪切板监视程序的时候使用）:
//ChangeClipboardChain 改变剪切板监视链句柄
//GetClipboardOwner 获取剪切板的当前者句柄
//GetClipboardViewer 获取剪切板的原来第一个监视者句柄
//SetClipboardViewer 追加剪切板的监视者句柄
//GetOpenClipboardWindow 获取打开剪切板的当前窗口句柄
//GetClipboardSequenceNumber 获取当前窗体在剪切板链下的序列号

        #endregion

        /// <summary>
        /// SendMessage前将string转为byte[]
        /// </summary>
        /// <param name="InputStr"></param>
        /// <returns></returns>
        public static byte[] StringToByte(string InputStr)
        {
            byte[] ch = System.Text.Encoding.ASCII.GetBytes(InputStr);
            return ch;
        }

        public static bool IsChecked(IntPtr hWnd)
        {
            return WinApi.SendMessage(hWnd, BM_GETCHECK, 0, 0) == BST_CHECKED;
        }

        /// <summary>
        /// 创建显示器的DC 
        /// 如：CreateDC("DISPLAY", null, null, (IntPtr)null);
        /// </summary>
        /// <param name="lpszDriver">驱动名称</param>
        /// <param name="lpszDevice">设备名称</param>
        /// <param name="lpszOutput">无用，可以设定位"NULL"</param>
        /// <param name="lpInitData">任意的打印机数据</param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        public static extern IntPtr CreateDC(string lpszDriver, string lpszDevice, string lpszOutput, IntPtr lpInitData);

        /// <summary>
        /// 捕获当前屏幕
        /// </summary>
        /// <param name="hdcDest">目标设备的句柄</param>
        /// <param name="nXDest">目标对象的左上角的X坐标</param>
        /// <param name="nYDest">目标对象的左上角的X坐标</param>
        /// <param name="nWidth">目标对象的矩形的宽度</param>
        /// <param name="nHeight">目标对象的矩形的长度</param>
        /// <param name="hdcSrc">源设备的句柄</param>
        /// <param name="nXSrc">源对象的左上角的X坐标</param>
        /// <param name="nYSrc">源对象的左上角的X坐标</param>
        /// <param name="dwRop">光栅的操作值</param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        public static extern bool BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, TernaryRasterOperations dwRop);

        public enum TernaryRasterOperations
        {
            SRCCOPY = 0x00CC0020, /* dest = source*/
            SRCPAINT = 0x00EE0086, /* dest = source OR dest*/
            SRCAND = 0x008800C6, /* dest = source AND dest*/
            SRCINVERT = 0x00660046, /* dest = source XOR dest*/
            SRCERASE = 0x00440328, /* dest = source AND (NOT dest )*/
            NOTSRCCOPY = 0x00330008, /* dest = (NOT source)*/
            NOTSRCERASE = 0x001100A6, /* dest = (NOT src) AND (NOT dest) */
            MERGECOPY = 0x00C000CA, /* dest = (source AND pattern)*/
            MERGEPAINT = 0x00BB0226, /* dest = (NOT source) OR dest*/
            PATCOPY = 0x00F00021, /* dest = pattern*/
            PATPAINT = 0x00FB0A09, /* dest = DPSnoo*/
            PATINVERT = 0x005A0049, /* dest = pattern XOR dest*/
            DSTINVERT = 0x00550009, /* dest = (NOT dest)*/
            BLACKNESS = 0x00000042, /* dest = BLACK*/
            WHITENESS = 0x00FF0062, /* dest = WHITE*/
        }

        #region  参数

        #region AnimateWindow dwFags

        /// <summary>
        /// 自左向右显示窗口。该标志可以在滚动动画和滑动动画中使用。当使用AW_CENTER标志时，该标志将被忽略；
        /// </summary>
        public const Int32 AW_HOR_LEFT_RIGHT = 0x00000001;

        /// <summary>
        /// 自右向左显示窗口。该标志可以在滚动动画和滑动动画中使用。当使用AW_CENTER标志时，该标志将被忽略；
        /// </summary>
        public const Int32 AW_HOR_RIGHT_LEFT = 0x00000002;

        /// <summary>
        /// 自顶向下显示窗口。该标志可以在滚动动画和滑动动画中使用。当使用AW_CENTER标志时，该标志将被忽略；
        /// </summary>
        public const Int32 AW_VER_UP_DOWN = 0x00000004;

        /// <summary>
        /// 自下向上显示窗口。该标志可以在滚动动画和滑动动画中使用。当使用AW_CENTER标志时，该标志将被忽略；
        /// </summary>
        public const Int32 AW_VER_DOWN_UP = 0x00000008;

        /// <summary>
        /// 若使用了AW_HIDE标志，则使窗口向内重叠，即收缩窗口；若未使用AW_HIDE标志，则使窗口向外扩展，即展开窗口；
        /// </summary>
        public const Int32 AW_CENTER = 0x00000010;

        /// <summary>
        /// 隐藏窗口，缺省则显示窗口；
        /// </summary>
        public const Int32 AW_HIDE = 0x00010000;

        /// <summary>
        /// 激活窗口。在使用了AW_HIDE标志后不能使用这个标志；
        /// </summary>
        public const Int32 AW_ACTIVATE = 0x00020000;

        /// <summary>
        /// 使用滑动类型。缺省则为滚动动画类型。当使用AW_CENTER标志时，这个标志就被忽略
        /// </summary>
        public const Int32 AW_SLIDE = 0x00040000;

        /// <summary>
        /// 实现淡出效果。只有当hWnd为顶层窗口的时候才可以使用此标志；
        /// </summary>
        public const Int32 AW_BLEND = 0x00080000;

        #endregion

        #region ChildWindowFromPointEx uFlgs

        //忽略不可见的子窗口。
        public const int CWP_SKIPINVISIBLE = 0x3;
        //忽略不可用子窗体
        public const int CWP_SKIPDISABLED = 0x2;
        //忽略隐藏的或透明窗体
        public const int CWP_SKIPINVISIBL = 0x1;
        //不忽略任一子窗口
        public const int CWP_All = 0x0;

        #endregion

        #region SetWindowPos  hWndInsertAfter

        //{在前面}
        public const int HWND_TOP = 0;
        //{在后面}
        public const int HWND_BOTTOM = 1;
        //{在前面, 位于任何顶部窗口的前面}
        public const int HWND_TOPMOST = -1;
        //{在前面, 位于其他顶部窗口的后面}
        public const int HWND_NOTOPMOST = -2;

        #endregion

        #region SetWindowPos  uFlags

        //{忽略 cx、cy, 保持大小}
        public const int SWP_NOSIZE = 1;
        //{忽略 X、Y, 不改变位置}
        public const int SWP_NOMOVE = 2;
        //{忽略 hWndInsertAfter, 保持 Z 顺序}
        public const int SWP_NOZORDER = 4;
        //{不重绘}
        public const int SWP_NOREDRAW = 8;
        //{不激活}
        public const int SWP_NOACTIVATE = 10;
        //{强制发送 WM_NCCALCSIZE 消息, 一般只是在改变大小时才发送此消息}
        public const int SWP_FRAMECHANGED = 20;
        //{显示窗口}
        public const int SWP_SHOWWINDOW = 40;
        //{隐藏窗口}
        public const int SWP_HIDEWINDOW = 80;
        //{丢弃客户区}
        public const int SWP_NOCOPYBITS = 100;
        //{忽略 hWndInsertAfter, 不改变 Z 序列的所有者}
        public const int SWP_NOOWNERZORDER = 200;
        //{不发出 WM_WINDOWPOSCHANGING 消息}
        public const int SWP_NOSENDCHANGING = 400;
        //{画边框}
        public const int SWP_DRAWFRAME = SWP_FRAMECHANGED;
        public const int SWP_NOREPOSITION = SWP_NOOWNERZORDER;
        //{防止产生 WM_SYNCPAINT 消息}
        public const int SWP_DEFERERASE = 2000;
        //{若调用进程不拥有窗口, 系统会向拥有窗口的线程发出需求}
        public const int SWP_ASYNCWINDOWPOS = 4000;

        #endregion

        #region  DrawAnimatedRects idAni

        public const System.Int32 IDANI_OPEN = 1;
        public const System.Int32 IDANI_CAPTION = 3;

        #endregion

        //鼠标点击
        public const int WM_CLICK = 0x00F5;
        public const int EM_SETSEL = 0x00B1;
        public const int EM_REPLACESEL = 0x00C2;

        //获取checkbox是否选中
        public const int BM_GETCHECK = 0x00F0;
        //设置checkbox选中
        public const int BM_SETCHECK = 0x00F1;
        //获取checkbox状态
        public const int BM_GETSTATE = 0x00F2;
        //checkbox选中
        public const int BST_CHECKED = 0x1;

        //移动鼠标 
        public const int MOUSEEVENTF_MOVE = 0x0001;
        //模拟鼠标左键按下 
        public const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        //模拟鼠标左键抬起 
        public const int MOUSEEVENTF_LEFTUP = 0x0004;
        //模拟鼠标右键按下 
        public const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        //模拟鼠标右键抬起 
        public const int MOUSEEVENTF_RIGHTUP = 0x0010;
        //模拟鼠标中键按下 
        public const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        //模拟鼠标中键抬起 
        public const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        //标示是否采用绝对坐标 
        public const int MOUSEEVENTF_ABSOLUTE = 0x8000;

        /// <summary>
        /// values from Winuser.h in Microsoft SDK.
        /// Windows NT/2000/XP: Installs a hook procedure that monitors low-level mouse input events.
        /// </summary>
        public const int WH_MOUSE_LL = 14;

        /// <summary>
        /// Windows NT/2000/XP: Installs a hook procedure that monitors low-level keyboard  input events.
        /// </summary>
        public const int WH_KEYBOARD_LL = 13;

        /// <summary>
        /// Installs a hook procedure that monitors mouse messages. For more information, see the MouseProc hook procedure. 
        /// </summary>
        public const int WH_MOUSE = 7;

        /// <summary>
        /// Installs a hook procedure that monitors keystroke messages. For more information, see the KeyboardProc hook procedure. 
        /// </summary>
        public const int WH_KEYBOARD = 2;

        //Shift键
        public const byte VK_SHIFT = 0x10;
        //CapsLock键
        public const byte VK_CAPITAL = 0x14;
        //NumLK键
        public const byte VK_NUMLOCK = 0x90;

        //创建一个窗口
        public const int WM_CREATE = 0x01;
        //当一个窗口被破坏时发送
        public const int WM_DESTROY = 0x02;
        //移动一个窗口
        public const int WM_MOVE = 0x03;
        //改变一个窗口的大小
        public const int WM_SIZE = 0x05;
        //一个窗口被激活或失去激活状态
        public const int WM_ACTIVATE = 0x06;
        //一个窗口获得焦点
        public const int WM_SETFOCUS = 0x07;
        //一个窗口失去焦点
        public const int WM_KILLFOCUS = 0x08;
        //一个窗口改变成Enable状态
        public const int WM_ENABLE = 0x0A;
        //设置窗口是否能重画
        public const int WM_SETREDRAW = 0x0B;
        //应用程序发送此消息来设置一个窗口的文本
        public const int WM_SETTEXT = 0x0C;
        //应用程序发送此消息来复制对应窗口的文本到缓冲区
        public const int WM_GETTEXT = 0x0D;
        //得到与一个窗口有关的文本的长度（不包含空字符）
        public const int WM_GETTEXTLENGTH = 0x0E;
        //要求一个窗口重画自己
        public const int WM_PAINT = 0x0F;
        //当一个窗口或应用程序要关闭时发送一个信号
        public const int WM_CLOSE = 0x10;
        //当用户选择结束对话框或程序自己调用ExitWindows函数
        public const int WM_QUERYENDSESSION = 0x11;
        //用来结束程序运行
        public const int WM_QUIT = 0x12;
        //当用户窗口恢复以前的大小位置时，把此消息发送给某个图标
        public const int WM_QUERYOPEN = 0x13;
        //当窗口背景必须被擦除时（例在窗口改变大小时）
        public const int WM_ERASEBKGND = 0x14;
        //当系统颜色改变时，发送此消息给所有顶级窗口
        public const int WM_SYSCOLORCHANGE = 0x15;
        //当系统进程发出WM_QUERYENDSESSION消息后，此消息发送给应用程序，通知它对话是否结束
        public const int WM_ENDSESSION = 0x16;
        //当隐藏或显示窗口是发送此消息给这个窗口
        public const int WM_SHOWWINDOW = 0x18;
        //发此消息给应用程序哪个窗口是激活的，哪个是非激活的
        public const int WM_ACTIVATEAPP = 0x1C;
        //当系统的字体资源库变化时发送此消息给所有顶级窗口
        public const int WM_FONTCHANGE = 0x1D;
        //当系统的时间变化时发送此消息给所有顶级窗口
        public const int WM_TIMECHANGE = 0x1E;
        //发送此消息来取消某种正在进行的摸态（操作）
        public const int WM_CANCELMODE = 0x1F;
        //如果鼠标引起光标在某个窗口中移动且鼠标输入没有被捕获时，就发消息给某个窗口
        public const int WM_SETCURSOR = 0x20;
        //当光标在某个非激活的窗口中而用户正按着鼠标的某个键发送此消息给//当前窗口
        public const int WM_MOUSEACTIVATE = 0x21;
        //发送此消息给MDI子窗口//当用户点击此窗口的标题栏，或//当窗口被激活，移动，改变大小
        public const int WM_CHILDACTIVATE = 0x22;
        //此消息由基于计算机的训练程序发送，通过WH_JOURNALPALYBACK的hook程序分离出用户输入消息
        public const int WM_QUEUESYNC = 0x23;
        //此消息发送给窗口当它将要改变大小或位置
        public const int WM_GETMINMAXINFO = 0x24;
        //发送给最小化窗口当它图标将要被重画
        public const int WM_PAINTICON = 0x26;
        //此消息发送给某个最小化窗口，仅//当它在画图标前它的背景必须被重画
        public const int WM_ICONERASEBKGND = 0x27;
        //发送此消息给一个对话框程序去更改焦点位置
        public const int WM_NEXTDLGCTL = 0x28;
        //每当打印管理列队增加或减少一条作业时发出此消息 
        public const int WM_SPOOLERSTATUS = 0x2A;
        //当button，combobox，listbox，menu的可视外观改变时发送
        public const int WM_DRAWITEM = 0x2B;
        //当button, combo box, list box, list view control, or menu item 被创建时
        public const int WM_MEASUREITEM = 0x2C;
        //此消息有一个LBS_WANTKEYBOARDINPUT风格的发出给它的所有者来响应WM_KEYDOWN消息 
        public const int WM_VKEYTOITEM = 0x2E;
        //此消息由一个LBS_WANTKEYBOARDINPUT风格的列表框发送给他的所有者来响应WM_CHAR消息 
        public const int WM_CHARTOITEM = 0x2F;
        //当绘制文本时程序发送此消息得到控件要用的颜色
        public const int WM_SETFONT = 0x30;
        //应用程序发送此消息得到当前控件绘制文本的字体
        public const int WM_GETFONT = 0x31;
        //应用程序发送此消息让一个窗口与一个热键相关连 
        public const int WM_SETHOTKEY = 0x32;
        //应用程序发送此消息来判断热键与某个窗口是否有关联
        public const int WM_GETHOTKEY = 0x33;
        //此消息发送给最小化窗口，当此窗口将要被拖放而它的类中没有定义图标，应用程序能返回一个图标或光标的句柄，当用户拖放图标时系统显示这个图标或光标
        public const int WM_QUERYDRAGICON = 0x37;
        //发送此消息来判定combobox或listbox新增加的项的相对位置
        public const int WM_COMPAREITEM = 0x39;
        //显示内存已经很少了
        public const int WM_COMPACTING = 0x41;
        //发送此消息给那个窗口的大小和位置将要被改变时，来调用setwindowpos函数或其它窗口管理函数
        public const int WM_WINDOWPOSCHANGING = 0x46;
        //发送此消息给那个窗口的大小和位置已经被改变时，来调用setwindowpos函数或其它窗口管理函数
        public const int WM_WINDOWPOSCHANGED = 0x47;
        //当系统将要进入暂停状态时发送此消息
        public const int WM_POWER = 0x48;
        //当一个应用程序传递数据给另一个应用程序时发送此消息
        public const int WM_COPYDATA = 0x4A;
        //当某个用户取消程序日志激活状态，提交此消息给程序
        public const int WM_CANCELJOURNA = 0x4B;
        //当某个控件的某个事件已经发生或这个控件需要得到一些信息时，发送此消息给它的父窗口 
        public const int WM_NOTIFY = 0x4E;
        //当用户选择某种输入语言，或输入语言的热键改变
        public const int WM_INPUTLANGCHANGEREQUEST = 0x50;
        //当平台现场已经被改变后发送此消息给受影响的最顶级窗口
        public const int WM_INPUTLANGCHANGE = 0x51;
        //当程序已经初始化windows帮助例程时发送此消息给应用程序
        public const int WM_TCARD = 0x52;
        //此消息显示用户按下了F1，如果某个菜单是激活的，就发送此消息个此窗口关联的菜单，否则就发送给有焦点的窗口，如果//当前都没有焦点，就把此消息发送给//当前激活的窗口
        public const int WM_HELP = 0x53;
        //当用户已经登入或退出后发送此消息给所有的窗口，//当用户登入或退出时系统更新用户的具体设置信息，在用户更新设置时系统马上发送此消息
        public const int WM_USERCHANGED = 0x54;
        //公用控件，自定义控件和他们的父窗口通过此消息来判断控件是使用ANSI还是UNICODE结构
        public const int WM_NOTIFYFORMAT = 0x55;
        //当用户某个窗口中点击了一下右键就发送此消息给这个窗口
        public const int WM_CONTEXTMENU = 0x7B;//0x007B
        //当调用SETWINDOWLONG函数将要改变一个或多个 窗口的风格时发送此消息给那个窗口
        public const int WM_STYLECHANGING = 0x7C;
        //当调用SETWINDOWLONG函数一个或多个 窗口的风格后发送此消息给那个窗口
        public const int WM_STYLECHANGED = 0x7D;
        //当显示器的分辨率改变后发送此消息给所有的窗口
        public const int WM_DISPLAYCHANGE = 0x7E;
        //此消息发送给某个窗口来返回与某个窗口有关连的大图标或小图标的句柄
        public const int WM_GETICON = 0x7F;
        //程序发送此消息让一个新的大图标或小图标与某个窗口关联
        public const int WM_SETICON = 0x80;
        //当某个窗口第一次被创建时，此消息在WM_CREATE消息发送前发送
        public const int WM_NCCREATE = 0x81;
        //此消息通知某个窗口，非客户区正在销毁 
        public const int WM_NCDESTROY = 0x82;
        //当某个窗口的客户区域必须被核算时发送此消息
        public const int WM_NCCALCSIZE = 0x83;
        //移动鼠标，按住或释放鼠标时发生
        public const int WM_NCHITTEST = 0x84;
        //程序发送此消息给某个窗口当它（窗口）的框架必须被绘制时
        public const int WM_NCPAINT = 0x85;
        //此消息发送给某个窗口仅当它的非客户区需要被改变来显示是激活还是非激活状态
        public const int WM_NCACTIVATE = 0x86;
        //发送此消息给某个与对话框程序关联的控件，widdows控制方位键和TAB键使输入进入此控件通过应
        public const int WM_GETDLGCODE = 0x87;
        //当光标在一个窗口的非客户区内移动时发送此消息给这个窗口 非客户区为：窗体的标题栏及窗 的边框体
        public const int WM_NCMOUSEMOVE = 0xA0;
        //当光标在一个窗口的非客户区同时按下鼠标左键时提交此消息
        public const int WM_NCLBUTTONDOWN = 0xA1;
        //当用户释放鼠标左键同时光标某个窗口在非客户区十发送此消息 
        public const int WM_NCLBUTTONUP = 0xA2;
        //当用户双击鼠标左键同时光标某个窗口在非客户区十发送此消息
        public const int WM_NCLBUTTONDBLCLK = 0xA3;
        //当用户按下鼠标右键同时光标又在窗口的非客户区时发送此消息
        public const int WM_NCRBUTTONDOWN = 0xA4;
        //当用户释放鼠标右键同时光标又在窗口的非客户区时发送此消息
        public const int WM_NCRBUTTONUP = 0xA5;
        //当用户双击鼠标右键同时光标某个窗口在非客户区十发送此消息
        public const int WM_NCRBUTTONDBLCLK = 0xA6;
        //当用户按下鼠标中键同时光标又在窗口的非客户区时发送此消息
        public const int WM_NCMBUTTONDOWN = 0xA7;
        //当用户释放鼠标中键同时光标又在窗口的非客户区时发送此消息
        public const int WM_NCMBUTTONUP = 0xA8;
        //当用户双击鼠标中键同时光标又在窗口的非客户区时发送此消息
        public const int WM_NCMBUTTONDBLCLK = 0xA9;
        //WM_KEYDOWN 按下一个键
        public const int WM_KEYDOWN = 0x0100;
        //释放一个键
        public const int WM_KEYUP = 0x0101;
        //按下某键，并已发出WM_KEYDOWN， WM_KEYUP消息
        public const int WM_CHAR = 0x102;
        //当用translatemessage函数翻译WM_KEYUP消息时发送此消息给拥有焦点的窗口
        public const int WM_DEADCHAR = 0x103;
        //当用户按住ALT键同时按下其它键时提交此消息给拥有焦点的窗口
        public const int WM_SYSKEYDOWN = 0x104;
        //当用户释放一个键同时ALT 键还按着时提交此消息给拥有焦点的窗口
        public const int WM_SYSKEYUP = 0x105;
        //当WM_SYSKEYDOWN消息被TRANSLATEMESSAGE函数翻译后提交此消息给拥有焦点的窗口
        public const int WM_SYSCHAR = 0x106;
        //当WM_SYSKEYDOWN消息被TRANSLATEMESSAGE函数翻译后发送此消息给拥有焦点的窗口
        public const int WM_SYSDEADCHAR = 0x107;
        //在一个对话框程序被显示前发送此消息给它，通常用此消息初始化控件和执行其它任务
        public const int WM_INITDIALOG = 0x110;
        //当用户选择一条菜单命令项或当某个控件发送一条消息给它的父窗口，一个快捷键被翻译
        public const int WM_COMMAND = 0x111;
        //当用户选择窗口菜单的一条命令或//当用户选择最大化或最小化时那个窗口会收到此消息
        public const int WM_SYSCOMMAND = 0x112;
        //发生了定时器事件
        public const int WM_TIMER = 0x113;
        //当一个窗口标准水平滚动条产生一个滚动事件时发送此消息给那个窗口，也发送给拥有它的控件
        public const int WM_HSCROLL = 0x114;
        //当一个窗口标准垂直滚动条产生一个滚动事件时发送此消息给那个窗口也，发送给拥有它的控件
        public const int WM_VSCROLL = 0x115;
        //当一个菜单将要被激活时发送此消息，它发生在用户菜单条中的某项或按下某个菜单键，它允许程序在显示前更改菜单
        public const int WM_INITMENU = 0x116;
        //当一个下拉菜单或子菜单将要被激活时发送此消息，它允许程序在它显示前更改菜单，而不要改变全部
        public const int WM_INITMENUPOPUP = 0x117;
        //当用户选择一条菜单项时发送此消息给菜单的所有者（一般是窗口）
        public const int WM_MENUSELECT = 0x11F;
        //当菜单已被激活用户按下了某个键（不同于加速键），发送此消息给菜单的所有者
        public const int WM_MENUCHAR = 0x120;
        //当一个模态对话框或菜单进入空载状态时发送此消息给它的所有者，一个模态对话框或菜单进入空载状态就是在处理完一条或几条先前的消息后没有消息它的列队中等待
        public const int WM_ENTERIDLE = 0x121;
        //在windows绘制消息框前发送此消息给消息框的所有者窗口，通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置消息框的文本和背景颜色
        public const int WM_CTLCOLORMSGBOX = 0x132;
        //当一个编辑型控件将要被绘制时发送此消息给它的父窗口通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置编辑框的文本和背景颜色
        public const int WM_CTLCOLOREDIT = 0x133;
        //当一个列表框控件将要被绘制前发送此消息给它的父窗口通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置列表框的文本和背景颜色
        public const int WM_CTLCOLORLISTBOX = 0x134;
        //当一个按钮控件将要被绘制时发送此消息给它的父窗口通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置按纽的文本和背景颜色
        public const int WM_CTLCOLORBTN = 0x135;
        //当一个对话框控件将要被绘制前发送此消息给它的父窗口通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置对话框的文本背景颜色
        public const int WM_CTLCOLORDLG = 0x136;
        //当一个滚动条控件将要被绘制时发送此消息给它的父窗口通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置滚动条的背景颜色
        public const int WM_CTLCOLORSCROLLBAR = 0x137;
        //当一个静态控件将要被绘制时发送此消息给它的父窗口通过响应这条消息，所有者窗口可以 通过使用给定的相关显示设备的句柄来设置静态控件的文本和背景颜色
        public const int WM_CTLCOLORSTATIC = 0x138;
        //当鼠标轮子转动时发送此消息个当前有焦点的控件
        public const int WM_MOUSEWHEEL = 0x20A;
        //按下鼠标中键
        public const int WM_MBUTTONDOWN = 0x207;
        //释放鼠标中键
        public const int WM_MBUTTONUP = 0x208;
        //双击鼠标中键
        public const int WM_MBUTTONDBLCLK = 0x209;
        //移动鼠标时发生，同WM_MOUSEFIRST
        public const int WM_MOUSEMOVE = 0x200;
        //按下鼠标左键
        public const int WM_LBUTTONDOWN = 0x201;
        //释放鼠标左键
        public const int WM_LBUTTONUP = 0x202;
        //双击鼠标左键
        public const int WM_LBUTTONDBLCLK = 0x203;
        //按下鼠标右键
        public const int WM_RBUTTONDOWN = 0x204;
        //释放鼠标右键
        public const int WM_RBUTTONUP = 0x205;
        //双击鼠标右键
        public const int WM_RBUTTONDBLCLK = 0x206;
        //alt键
        public const int MOD_ALT = 0x0001;
        //ctrl键
        public const int MOD_CONTROl = 0x0002;
        //shift键
        public const int MOD_SHIFTl = 0x0004;
        //Windows键
        public const int MOD_WIN = 0x0008;
        //不重复 Windows Vista and Windows XP/2000:  This flag is not supported.
        public const int MOD_NOREPEAT = 0x4000;
        //热键
        public const int WM_HOTKEY = 0x312;

        #endregion

        #region Virtual-Key Codes表

        public const int KEYEVENTF_KEYUP = 0x2;

        //public const int VK_LBUTTON	=1;	//Left mouse button
        //public const int VK_RBUTTON	=2;	//Right mouse button
        //public const int VK_CANCEL	=3;	//Control-break processing
        //public const int VK_MBUTTON	=4;	//Middle mouse button (three-button mouse)
        //public const int VK_XBUTTON1	=5;	//Windows 2000/XP: X1 mouse button
        //public const int VK_XBUTTON2	=6;	//Windows 2000/XP: X2 mouse button
        ////—	07	Undefined
        //public const int VK_BACK	=8;	//BACKSPACE key
        //public const int VK_TAB	=9;	//TAB key
        ////—	0A–0B	Reserved
        //public const int VK_CLEAR	=0C;	//CLEAR key
        //public const int VK_RETURN	=0D;	//ENTER key
        ////—	0E–0F	Undefined
        //public const int VK_SHIFT	=10;	//SHIFT key
        //public const int VK_CONTROL	=11;	//CTRL key
        //public const int VK_MENU	=12;	//ALT key
        //public const int VK_PAUSE	=13;	//PAUSE key
        //public const int VK_CAPITAL	=14;	//CAPS LOCK key
        //public const int VK_KANA	=15;	//IME Kana mode
        //public const int VK_HANGUEL	=15;	//IME Hanguel mode (maintained for compatibility; use VK_HANGUL)
        //public const int VK_HANGUL	=15;	//IME Hangul mode
        ////—	16	Undefined
        //public const int VK_JUNJA	=17	IME Junja mode
        //public const int VK_FINAL	=18	IME final mode
        //public const int VK_HANJA	=19	IME Hanja mode
        //public const int VK_KANJI	=19	IME Kanji mode
        ////—	1A	Undefined
        //public const int VK_ESCAPE	=1B	ESC key
        //public const int VK_CONVERT	=1C	IME convert
        //public const int VK_NONCONVERT	=1D	IME nonconvert
        //public const int VK_ACCEPT	=1E	IME accept
        //public const int VK_MODECHANGE	1F	IME mode change request
        //public const int VK_SPACE	=20	SPACEBAR
        //public const int VK_PRIOR	=21	PAGE UP key
        //public const int VK_NEXT	=22	PAGE DOWN key
        //public const int VK_END	    =23	END key
        //public const int VK_HOME	=24	HOME key
        //public const int VK_LEFT	=25	LEFT ARROW key
        //public const int VK_UP	=26	UP ARROW key
        //public const int VK_RIGHT	=27	RIGHT ARROW key
        //public const int VK_DOWN	=28	DOWN ARROW key
        //public const int VK_SELECT	=29	SELECT key
        //public const int VK_PRINT	=2A	PRINT key
        //public const int VK_EXECUTE	=2B	EXECUTE key
        //public const int VK_SNAPSHOT	=2C	PRINT SCREEN key
        //public const int VK_INSERT	=2D	INS key
        //public const int VK_DELETE	=2E	DEL key
        //public const int VK_HELP	=2F	HELP key

        //30	0 key

        //31	1 key

        //32	2 key

        //33	3 key

        //34	4 key

        //35	5 key

        //36	6 key

        //37	7 key

        //38	8 key

        //39	9 key
        //—	3A–40	Undefined

        //41	A key

        //42	B key

        //43	C key

        //44	D key

        //45	E key

        //46	F key

        //47	G key

        //48	H key

        //49	I key

        //4A	J key

        //4B	K key

        //4C	L key

        //4D	M key

        //4E	N key

        //4F	O key

        //50	P key

        //51	Q key

        //52	R key

        //53	S key

        //54	T key

        //55	U key

        //56	V key

        //57	W key

        //58	X key

        //59	Y key

        //5A	Z key
        //VK_LWIN	5B	Left Windows key (Microsoft? Natural? keyboard)
        //VK_RWIN	5C	Right Windows key (Natural keyboard)
        //VK_APPS	5D	Applications key (Natural keyboard)
        //—	5E	Reserved
        //VK_SLEEP	5F	Computer Sleep key
        //VK_NUMPAD0	60	Numeric keypad 0 key
        //VK_NUMPAD1	61	Numeric keypad 1 key
        //VK_NUMPAD2	62	Numeric keypad 2 key
        //VK_NUMPAD3	63	Numeric keypad 3 key
        //VK_NUMPAD4	64	Numeric keypad 4 key
        //VK_NUMPAD5	65	Numeric keypad 5 key
        //VK_NUMPAD6	66	Numeric keypad 6 key
        //VK_NUMPAD7	67	Numeric keypad 7 key
        //VK_NUMPAD8	68	Numeric keypad 8 key
        //VK_NUMPAD9	69	Numeric keypad 9 key
        //VK_MULTIPLY	6A	Multiply key
        //VK_ADD	6B	Add key
        //VK_SEPARATOR	6C	Separator key
        //VK_SUBTRACT	6D	Subtract key
        //VK_DECIMAL	6E	Decimal key
        //VK_DIVIDE	6F	Divide key
        //VK_F1	70	F1 key
        //VK_F2	71	F2 key
        //VK_F3	72	F3 key
        //VK_F4	73	F4 key
        //VK_F5	74	F5 key
        //VK_F6	75	F6 key
        //VK_F7	76	F7 key
        //VK_F8	77	F8 key
        //VK_F9	78	F9 key
        //VK_F10	79	F10 key
        //VK_F11	7A	F11 key
        //VK_F12	7B	F12 key
        //VK_F13	7C	F13 key
        //VK_F14	7D	F14 key
        //VK_F15	7E	F15 key
        //VK_F16	7F	F16 key
        //VK_F17	80H	F17 key
        //VK_F18	81H	F18 key
        //VK_F19	82H	F19 key
        //VK_F20	83H	F20 key
        //VK_F21	84H	F21 key
        //VK_F22	85H	F22 key
        //VK_F23	86H	F23 key
        //VK_F24	87H	F24 key
        //—	88–8F	Unassigned
        //VK_NUMLOCK	90	NUM LOCK key
        //VK_SCROLL	91	SCROLL LOCK key

        //92–96	OEM specific
        //—	97–9F	Unassigned
        //VK_LSHIFT	A0	Left SHIFT key
        //VK_RSHIFT	A1	Right SHIFT key
        //VK_LCONTROL	A2	Left CONTROL key
        //VK_RCONTROL	A3	Right CONTROL key
        //VK_LMENU	A4	Left MENU key
        //VK_RMENU	A5	Right MENU key
        //VK_BROWSER_BACK	A6	Windows 2000/XP: Browser Back key
        //VK_BROWSER_FORWARD	A7	Windows 2000/XP: Browser Forward key
        //VK_BROWSER_REFRESH	A8	Windows 2000/XP: Browser Refresh key
        //VK_BROWSER_STOP	A9	Windows 2000/XP: Browser Stop key
        //VK_BROWSER_SEARCH	AA	Windows 2000/XP: Browser Search key
        //VK_BROWSER_FAVORITES	AB	Windows 2000/XP: Browser Favorites key
        //VK_BROWSER_HOME	AC	Windows 2000/XP: Browser Start and Home key
        //VK_VOLUME_MUTE	AD	Windows 2000/XP: Volume Mute key
        //VK_VOLUME_DOWN	AE	Windows 2000/XP: Volume Down key
        //VK_VOLUME_UP	AF	Windows 2000/XP: Volume Up key
        //VK_MEDIA_NEXT_TRACK	B0	Windows 2000/XP: Next Track key
        //VK_MEDIA_PREV_TRACK	B1	Windows 2000/XP: Previous Track key
        //VK_MEDIA_STOP	B2	Windows 2000/XP: Stop Media key
        //VK_MEDIA_PLAY_PAUSE	B3	Windows 2000/XP: Play/Pause Media key
        //VK_LAUNCH_MAIL	B4	Windows 2000/XP: Start Mail key
        //VK_LAUNCH_MEDIA_SELECT	B5	Windows 2000/XP: Select Media key
        //VK_LAUNCH_APP1	B6	Windows 2000/XP: Start Application 1 key
        //VK_LAUNCH_APP2	B7	Windows 2000/XP: Start Application 2 key
        //—	B8-B9	Reserved
        //VK_OEM_1	BA	Used for miscellaneous characters; it can vary by keyboard.
        //Windows 2000/XP: For the US standard keyboard, the ';:' key

        //VK_OEM_PLUS	BB	Windows 2000/XP: For any country/region, the '+' key
        //VK_OEM_COMMA	BC	Windows 2000/XP: For any country/region, the ',' key
        //VK_OEM_MINUS	BD	Windows 2000/XP: For any country/region, the '-' key
        //VK_OEM_PERIOD	BE	Windows 2000/XP: For any country/region, the '.' key
        //VK_OEM_2	BF	Used for miscellaneous characters; it can vary by keyboard.
        //Windows 2000/XP: For the US standard keyboard, the '/?' key

        //VK_OEM_3	C0	Used for miscellaneous characters; it can vary by keyboard.
        //Windows 2000/XP: For the US standard keyboard, the '`~' key

        //—	C1–D7	Reserved
        //—	D8–DA	Unassigned
        //VK_OEM_4	DB	Used for miscellaneous characters; it can vary by keyboard.
        //Windows 2000/XP: For the US standard keyboard, the '[{' key

        //VK_OEM_5	DC	Used for miscellaneous characters; it can vary by keyboard.
        //Windows 2000/XP: For the US standard keyboard, the '\|' key

        //VK_OEM_6	DD	Used for miscellaneous characters; it can vary by keyboard.
        //Windows 2000/XP: For the US standard keyboard, the ']}' key

        //VK_OEM_7	DE	Used for miscellaneous characters; it can vary by keyboard.
        //Windows 2000/XP: For the US standard keyboard, the 'single-quote/double-quote' key

        //VK_OEM_8	DF	Used for miscellaneous characters; it can vary by keyboard.
        //—	E0	Reserved

        //E1	OEM specific
        //VK_OEM_102	E2	Windows 2000/XP: Either the angle bracket key or the backslash key on the RT 102-key keyboard

        //E3–E4	OEM specific
        //VK_PROCESSKEY	E5	Windows 95/98/Me, Windows NT 4.0, Windows 2000/XP: IME PROCESS key

        //E6	OEM specific
        //VK_PACKET	E7	Windows 2000/XP: Used to pass Unicode characters as if they were keystrokes. The VK_PACKET key is the low word of a 32-bit Virtual Key value used for non-keyboard input methods. For more information, see Remark in KEYBDINPUT, SendInput, WM_KEYDOWN, and WM_KEYUP
        //—	E8	Unassigned

        //E9–F5	OEM specific
        //VK_ATTN	F6	Attn key
        //VK_CRSEL	F7	CrSel key
        //VK_EXSEL	F8	ExSel key
        //VK_EREOF	F9	Erase EOF key
        //VK_PLAY	FA	Play key
        //VK_ZOOM	FB	Zoom key
        //VK_NONAME	FC	Reserved for future use
        //VK_PA1	FD	PA1 key
        //VK_OEM_CLEAR	FE	Clear key

        #endregion
    }
}
