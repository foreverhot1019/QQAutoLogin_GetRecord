using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;

using System.Diagnostics;
using System.Windows.Automation.Provider;
using System.Windows.Automation.Text;
using System.Windows.Automation;
using System.Threading;
using WindowsAPI;

namespace QQHockTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 读取正在 聊天记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTXGuiFoundation_Click(object sender, EventArgs e)
        {
            //根目录
            AutomationElement aeDeskTop = AutomationElement.RootElement;
            //所有 QQ相关的窗口
            var AllChildrenElement = aeDeskTop.FindAll(TreeScope.Descendants, new PropertyCondition(AutomationElement.ClassNameProperty, "TXGuiFoundation"));
            foreach (AutomationElement item in AllChildrenElement)
            {
                int WindowHandle = item.Current.NativeWindowHandle;
                string Name = item.Current.Name;
                if (!string.IsNullOrEmpty(Name))
                {
                    if (Name != "QQ")
                    {
                        AndCondition pcTarget = null;
                        pcTarget = new AndCondition(
                            new PropertyCondition(AutomationElement.NameProperty, "消息"),
                            new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit));
                        AutomationElementCollection AutoEleCll = item.FindAll(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, "消息"));
                        if (AutoEleCll.Count > 0)
                        {
                            foreach (AutomationElement Edititem in AutoEleCll)
                            {
                                //激活聊天主窗体
                                WinApi.ShowWindow(new IntPtr(WindowHandle), 1);
                                WinApi.SetActiveWindow(new IntPtr(WindowHandle));
                                WinApi.SetForegroundWindow(new IntPtr(WindowHandle));

                                #region 获取 元素位置
                                //位置
                                System.Windows.Rect rect = Edititem.Current.BoundingRectangle;
                                //元素 左上角位置
                                System.Drawing.Point tPoint = new System.Drawing.Point();
                                tPoint.X = Convert.ToInt32(rect.Left);
                                tPoint.Y = Convert.ToInt32(rect.Top);
                                //元素正中间位置
                                System.Drawing.Point CenterPoint = new System.Drawing.Point();
                                CenterPoint.X = Convert.ToInt32(rect.Left + rect.Width / 2);
                                CenterPoint.Y = Convert.ToInt32(rect.Top + rect.Height / 2);
                                //设置鼠标到指定位置
                                WinApi.SetCursorPos(CenterPoint);
                                //模拟鼠标点击
                                WinApi.mouse_event(WinApi.MOUSEEVENTF_LEFTDOWN | WinApi.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                                #endregion

                                //设置窗体内容(windows 消息方式)
                                //WinApi.SendMessage(new IntPtr(WindowHandle), WinApi.EM_SETSEL, -1, -1);//			
                                //WinApi.SendMessage(new IntPtr(WindowHandle), WinApi.WM_SETTEXT, 0, "Text");

                                #region 写入 数据 如果不是readonly的话
                                //object ClipboadData = Clipboard.GetDataObject();
                                //Clipboard.SetDataObject("模拟键盘事件 消息方式");
                                ////设置窗体内容(模拟键盘事件 消息方式)
                                //WinApi.keybd_event(Keys.ControlKey, 0, 0, 0);
                                //WinApi.keybd_event(Keys.V, 0, 0, 0);
                                //WinApi.keybd_event(Keys.V, 0, WinApi.KEYEVENTF_KEYUP, 0);
                                //WinApi.keybd_event(Keys.ControlKey, 0, WinApi.KEYEVENTF_KEYUP, 0);
                                //Thread.Sleep(10);
                                ////设置窗体内容(模拟键盘事件 消息方式)
                                //WinApi.keybd_event(Keys.CapsLock, 0, 0, 0);
                                //WinApi.keybd_event(Keys.CapsLock, 0, WinApi.KEYEVENTF_KEYUP, 0);
                                //Thread.Sleep(10);
                                //WinApi.keybd_event(Keys.T, 0, 0, 0);
                                //WinApi.keybd_event(Keys.T, 0, WinApi.KEYEVENTF_KEYUP, 0);
                                //Thread.Sleep(10);
                                //WinApi.keybd_event(Keys.CapsLock, 0, 0, 0);
                                //WinApi.keybd_event(Keys.CapsLock, 0, WinApi.KEYEVENTF_KEYUP, 0);

                                //WinApi.keybd_event(Keys.E, 0, 0, 0);
                                //WinApi.keybd_event(Keys.E, 0, WinApi.KEYEVENTF_KEYUP, 0);
                                //Thread.Sleep(10);
                                //WinApi.keybd_event(Keys.S, 0, 0, 0);
                                //WinApi.keybd_event(Keys.S, 0, WinApi.KEYEVENTF_KEYUP, 0);
                                //Thread.Sleep(10);
                                //WinApi.keybd_event(Keys.T, 0, 0, 0);
                                //WinApi.keybd_event(Keys.T, 0, WinApi.KEYEVENTF_KEYUP, 0);
                                //Thread.Sleep(10);
                                #endregion

                                //获取 UI-Automation 元素 值
                                ValuePattern vpTextBox1 = (ValuePattern)Edititem.GetCurrentPattern(ValuePattern.Pattern);
                                string valStr = vpTextBox1.Current.Value;

                                //vpTextBox1.SetValue(valStr);
                                //SendKeys sendkeys =new SendKeys();
                                //List<string> keys = new List<string>();
                                //keys.Add(sendkeys.Ctrl);
                                //keys.Add(sendkeys.Enter);
                                //sendkeys.Sendkeys(Edititem, keys.ToArray());

                                if (tabCtrlMessage.TabPages.ContainsKey(WindowHandle.ToString()))
                                {
                                    int tbpageNum = tabCtrlMessage.TabPages.IndexOfKey(WindowHandle.ToString());
                                    ListView lstv = new ListView();
                                    if (tabCtrlMessage.TabPages[tbpageNum].Controls[0] is ListView)
                                    {
                                        lstv = (ListView)tabCtrlMessage.TabPages[tbpageNum].Controls[0];

                                        var StrLines = valStr.Split('\r');
                                        foreach (var Stritem in StrLines)
                                        {
                                            ListViewItem lstitem = new ListViewItem(Edititem.Current.Name + ":" + Stritem);
                                            lstv.Items.Add(lstitem);
                                        }
                                    }
                                    else
                                    {
                                        tabCtrlMessage.TabPages[tbpageNum].Controls.Clear();

                                        var StrLines = valStr.Split('\r');
                                        foreach (var Stritem in StrLines)
                                        {
                                            ListViewItem lstitem = new ListViewItem(Edititem.Current.Name + ":" + Stritem);
                                            lstv.Items.Add(lstitem);
                                        }
                                        lstv.Width = tabCtrlMessage.Width;
                                        lstv.Height = tabCtrlMessage.Height;
                                        lstv.DrawItem += lstv_DrawItem;
                                        tabCtrlMessage.TabPages[tabCtrlMessage.TabPages.Count - 1].Controls.Add(lstv);
                                    }
                                }
                                else
                                {
                                    tabCtrlMessage.TabPages.Add(WindowHandle.ToString(), Name);
                                    ListView lstv = new ListView();
                                    lstv.Width = tabCtrlMessage.Width;
                                    lstv.Height = tabCtrlMessage.Height;

                                    var StrLines = valStr.Split('\r');
                                    foreach (var Stritem in StrLines)
                                    {
                                        ListViewItem lstitem = new ListViewItem(Edititem.Current.Name + ":" + Stritem);
                                        lstv.Items.Add(lstitem);
                                    }

                                    lstv.DrawItem += lstv_DrawItem;
                                    tabCtrlMessage.TabPages[tabCtrlMessage.TabPages.Count - 1].Controls.Add(lstv);
                                }
                            }
                        }
                    }
                    else
                    {
                        
                    }
                }
            }
        }

        private void lstv_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            Rectangle rect = e.Bounds;
            rect.Width = rect.Width - 3;
            if ((e.State & ListViewItemStates.Selected) != 0)
            {
                //更改选中的背景颜色
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(215, 232, 252)), rect);
                //绘制边框
                e.Graphics.DrawRectangle(new Pen(Color.FromArgb(125, 162, 206)), rect);
            }
            else
            {
                //默认的背景颜色
                using (SolidBrush bBrush = new SolidBrush(Color.White))
                {
                    e.Graphics.FillRectangle(bBrush, rect);
                    //绘制边框
                    e.Graphics.DrawRectangle(new Pen(Color.FromArgb(125, 162, 206)), rect);
                }
            }
            if (((ListView)sender).View != View.Details)
            {
                Font ft = new System.Drawing.Font(FontFamily.GenericSansSerif, 9);
                SolidBrush fontbrush = new SolidBrush(Color.Blue);
                StringFormat Format = new StringFormat();
                Format.Alignment = StringAlignment.Center;
                e.Graphics.DrawString(e.Item.Text, ft, fontbrush, new RectangleF(rect.Left, rect.Top + 10, rect.Width, rect.Height), Format);
                ft.Dispose();
                fontbrush.Dispose();
            }

        }

        //循环获取 无限子集
        public void GetEditContrlIntPtr(AutomationElementCollection AutoEleCll, out IntPtr EditContrl)
        {
            EditContrl = IntPtr.Zero;
            foreach (AutomationElement item in AutoEleCll)
            {
                if (EditContrl != IntPtr.Zero)
                {
                    break;
                }
                else
                {
                    AutomationElementCollection _AutoEleCll = item.FindAll(TreeScope.Children, Condition.TrueCondition);
                    if (_AutoEleCll.Count > 0)
                    {
                        GetEditContrlIntPtr(_AutoEleCll, out EditContrl);
                    }
                }
            }
        }

        /// <summary>
        /// 模拟发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSendMsg_Click(object sender, EventArgs e)
        {
            //根目录
            AutomationElement aeDeskTop = AutomationElement.RootElement;
            //所有 QQ相关的窗口
            var AllChildrenElement = aeDeskTop.FindAll(TreeScope.Descendants, new PropertyCondition(AutomationElement.ClassNameProperty, "TXGuiFoundation"));
            foreach (AutomationElement item in AllChildrenElement)
            {
                int WindowHandle = item.Current.NativeWindowHandle;
                string Name = item.Current.Name;
                if (!string.IsNullOrEmpty(Name))
                {
                    if (Name != "QQ")
                    {
                        #region 模拟输入消息
                        AndCondition pcTarget = null;
                        pcTarget = new AndCondition(
                            new PropertyCondition(AutomationElement.NameProperty, "输入"),
                            new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit));
                        AutomationElementCollection AutoEleCll = item.FindAll(TreeScope.Descendants, pcTarget);
                        if (AutoEleCll.Count > 0)
                        {
                            foreach (AutomationElement Edititem in AutoEleCll)
                            {
                                //激活聊天主窗体
                                WinApi.ShowWindow(new IntPtr(WindowHandle), 1);
                                WinApi.SetActiveWindow(new IntPtr(WindowHandle));
                                WinApi.SetForegroundWindow(new IntPtr(WindowHandle));

                                #region 获取 元素位置
                                //位置
                                System.Windows.Rect rect = Edititem.Current.BoundingRectangle;
                                //元素 左上角位置
                                System.Drawing.Point tPoint = new System.Drawing.Point();
                                tPoint.X = Convert.ToInt32(rect.Left);
                                tPoint.Y = Convert.ToInt32(rect.Top);
                                //元素正中间位置
                                System.Drawing.Point CenterPoint = new System.Drawing.Point();
                                CenterPoint.X = Convert.ToInt32(rect.Left + rect.Width / 2);
                                CenterPoint.Y = Convert.ToInt32(rect.Top + rect.Height / 2);
                                //设置鼠标到指定位置
                                WinApi.SetCursorPos(CenterPoint);
                                //模拟鼠标点击
                                WinApi.mouse_event(WinApi.MOUSEEVENTF_LEFTDOWN | WinApi.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                                #endregion

                                //设置窗体内容(windows 消息方式)
                                //WinApi.SendMessage(new IntPtr(WindowHandle), WinApi.EM_SETSEL, -1, -1);//			
                                //WinApi.SendMessage(new IntPtr(WindowHandle), WinApi.WM_SETTEXT, 0, "Text");

                                #region 写入 数据 如果不是readonly的话
                                object ClipboadData = Clipboard.GetDataObject();
                                Clipboard.SetDataObject("模拟键盘事件,消息方式-");
                                //设置窗体内容(模拟键盘事件 消息方式)
                                WinApi.keybd_event(Keys.ControlKey, 0, 0, 0);
                                WinApi.keybd_event(Keys.V, 0, 0, 0);
                                WinApi.keybd_event(Keys.V, 0, WinApi.KEYEVENTF_KEYUP, 0);
                                WinApi.keybd_event(Keys.ControlKey, 0, WinApi.KEYEVENTF_KEYUP, 0);
                                Thread.Sleep(10);
                                //设置窗体内容(模拟键盘事件 消息方式)
                                WinApi.keybd_event(Keys.CapsLock, 0, 0, 0);
                                WinApi.keybd_event(Keys.CapsLock, 0, WinApi.KEYEVENTF_KEYUP, 0);
                                Thread.Sleep(10);
                                WinApi.keybd_event(Keys.T, 0, 0, 0);
                                WinApi.keybd_event(Keys.T, 0, WinApi.KEYEVENTF_KEYUP, 0);
                                Thread.Sleep(10);
                                WinApi.keybd_event(Keys.CapsLock, 0, 0, 0);
                                WinApi.keybd_event(Keys.CapsLock, 0, WinApi.KEYEVENTF_KEYUP, 0);

                                WinApi.keybd_event(Keys.E, 0, 0, 0);
                                WinApi.keybd_event(Keys.E, 0, WinApi.KEYEVENTF_KEYUP, 0);
                                Thread.Sleep(10);
                                WinApi.keybd_event(Keys.S, 0, 0, 0);
                                WinApi.keybd_event(Keys.S, 0, WinApi.KEYEVENTF_KEYUP, 0);
                                Thread.Sleep(10);
                                WinApi.keybd_event(Keys.T, 0, 0, 0);
                                WinApi.keybd_event(Keys.T, 0, WinApi.KEYEVENTF_KEYUP, 0);
                                Thread.Sleep(10);
                                #endregion
                            }
                        }
                        #endregion

                        #region 点击 发送 按钮
                        pcTarget = null;
                        pcTarget = new AndCondition(
                            new PropertyCondition(AutomationElement.NameProperty, "发送(&S)"),
                            new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Button));

                        AutoEleCll = item.FindAll(TreeScope.Descendants, pcTarget);

                        if (AutoEleCll.Count > 0)
                        {
                            foreach (AutomationElement Edititem in AutoEleCll)
                            {
                                //激活聊天主窗体
                                WinApi.ShowWindow(new IntPtr(WindowHandle), 1);
                                WinApi.SetActiveWindow(new IntPtr(WindowHandle));
                                WinApi.SetForegroundWindow(new IntPtr(WindowHandle));

                                System.Windows.Rect rect = Edititem.Current.BoundingRectangle;

                                System.Drawing.Point tPoint = new System.Drawing.Point();
                                tPoint.X = Convert.ToInt32(rect.Left);
                                tPoint.Y = Convert.ToInt32(rect.Top);

                                System.Drawing.Point CenterPoint = new System.Drawing.Point();
                                CenterPoint.X = Convert.ToInt32(rect.Left + rect.Width / 2);
                                CenterPoint.Y = Convert.ToInt32(rect.Top + rect.Height / 2);

                                //移动鼠标至登录Button并模拟点击
                                System.Drawing.Point MousePoint = new System.Drawing.Point();
                                WinApi.GetCursorPos(out MousePoint);
                                Rectangle rectgle = System.Windows.Forms.Screen.PrimaryScreen.Bounds;

                                WinApi.SetCursorPos(CenterPoint);
                                Thread.Sleep(1000);
                                //WinApi.SetCursorPos(new System.Drawing.Point(1182,586));
                                //Thread.Sleep(1000);
                                //WinApi.SetCursorPos(new System.Drawing.Point(10,10));

                                int dx = MousePoint.X - CenterPoint.X;
                                int dy = MousePoint.Y - CenterPoint.Y;

                                //读取色位
                                //int ss = System.Windows.Forms.Screen.PrimaryScreen.BitsPerPixel;
                                //Math.Pow(2, ss);//2的ss次方
                                //WinApi.mouse_event(WinApi.MOUSEEVENTF_ABSOLUTE | WinApi.MOUSEEVENTF_MOVE, dx, dy, 0, 0);
                                WinApi.mouse_event(WinApi.MOUSEEVENTF_LEFTDOWN | WinApi.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);

                                //ValuePattern vpTextBox1 = (ValuePattern)Edititem.GetCurrentPattern(ValuePattern.Pattern);
                                //string valStr = vpTextBox1.Current.Value + "Text";
                                //vpTextBox1.SetValue(valStr);
                                //InvokePattern ipClickButton1 = (InvokePattern)Edititem.GetCurrentPattern(InvokePattern.Pattern);
                                //ipClickButton1.Invoke();
                                //Thread.Sleep(1000);
                            }
                        }
                        #endregion
                    }
                }
            }
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            //根目录
            AutomationElement aeDeskTop = AutomationElement.RootElement;
            //所有 QQ相关的窗口
            var AllChildrenElement = aeDeskTop.FindAll(TreeScope.Descendants, new PropertyCondition(AutomationElement.ClassNameProperty, "TXGuiFoundation"));
            foreach (AutomationElement item in AllChildrenElement)
            {
                int WindowHandle = item.Current.NativeWindowHandle;
                string Name = item.Current.Name;
                if (!string.IsNullOrEmpty(Name))
                {
                    if (Name == "QQ")
                    {
                        AutomationElementCollection AutoEleCll = item.FindAll(TreeScope.Children, Condition.TrueCondition);
                        if (AutoEleCll.Count > 0)
                        {
                            if (AutoEleCll.Count > 3)
                            {
                                //已登陆的QQ
                            }
                            else
                            {
                                #region QQ登陆框
                                //激活聊天主窗体
                                WinApi.ShowWindow(new IntPtr(WindowHandle), 1);
                                WinApi.SetActiveWindow(new IntPtr(WindowHandle));
                                WinApi.SetForegroundWindow(new IntPtr(WindowHandle));
                                IntPtr QQpswIntPtr = WinApi.FindWindow("TXGuiFoundation", "QQ");
                                if (QQpswIntPtr != IntPtr.Zero)
                                {
                                    IntPtr PswEditIntPtr = WinApi.FindWindowEx(QQpswIntPtr, IntPtr.Zero, "Edit", "QQEdit");
                                    if (PswEditIntPtr != IntPtr.Zero)
                                    {
                                        WinApi.SendMessage(PswEditIntPtr, WinApi.EM_SETSEL, -1, -1);//			
                                        WinApi.SendMessage(PswEditIntPtr, WinApi.WM_SETTEXT, 0, "~foreverhot1019~");
                                    }
                                }

                                AndCondition pcTarget = null;
                                pcTarget = new AndCondition(
                                    new PropertyCondition(AutomationElement.NameProperty, "密码"),
                                    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Pane));
                                AutomationElementCollection _AutoEleCll = item.FindAll(TreeScope.Descendants, pcTarget);
                                if (_AutoEleCll.Count > 0)
                                {
                                    foreach (AutomationElement Passworditem in _AutoEleCll)
                                    {
                                        //激活聊天主窗体
                                        WinApi.ShowWindow(new IntPtr(WindowHandle), 1);
                                        WinApi.SetActiveWindow(new IntPtr(WindowHandle));
                                        WinApi.SetForegroundWindow(new IntPtr(WindowHandle));
                                        //整个 密码pane 框的位置
                                        System.Windows.Rect rect = Passworditem.Current.BoundingRectangle;
                                        AndCondition _pcTarget = new AndCondition(
                                            new PropertyCondition(AutomationElement.IsEnabledProperty, true),
                                            new PropertyCondition(AutomationElement.IsPasswordProperty, false),
                                            new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Pane));
                                        AutomationElementCollection pswPaneElementCollection = Passworditem.FindAll(TreeScope.Descendants, _pcTarget);
                                        if (pswPaneElementCollection.Count > 0)
                                        {
                                            foreach (AutomationElement pswitem in _AutoEleCll)
                                            {
                                                AutomationElementCollection _pswitemcoll = pswitem.FindAll(TreeScope.Children, Condition.TrueCondition);
                                                if (_pswitemcoll.Count <= 0)
                                                {
                                                    rect = pswitem.Current.BoundingRectangle;
                                                }
                                            }
                                        }
                                        //元素 左上角位置
                                        System.Drawing.Point tPoint = new System.Drawing.Point();
                                        tPoint.X = Convert.ToInt32(rect.Left);
                                        tPoint.Y = Convert.ToInt32(rect.Top);
                                        //元素正中间位置
                                        System.Drawing.Point CenterPoint = new System.Drawing.Point();
                                        CenterPoint.X = Convert.ToInt32(rect.Left + rect.Width / 2);
                                        CenterPoint.Y = Convert.ToInt32(rect.Top + rect.Height / 2);
                                        //设置鼠标到指定位置
                                        WinApi.SetCursorPos(CenterPoint);
                                        //模拟鼠标点击
                                        WinApi.mouse_event(WinApi.MOUSEEVENTF_LEFTDOWN | WinApi.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                                        Thread.Sleep(10);

                                        //设置窗体内容(模拟键盘事件 消息方式)
                                        WinApi.keybd_event(Keys.ShiftKey, 0, 0, 0);
                                        WinApi.keybd_event(192, 0, 0, 0);
                                        WinApi.keybd_event(192, 0, WinApi.KEYEVENTF_KEYUP, 0);
                                        WinApi.keybd_event(Keys.ShiftKey, 0, WinApi.KEYEVENTF_KEYUP, 0);
                                        Thread.Sleep(10);

                                        WinApi.keybd_event(Keys.F, 0, 0, 0);
                                        WinApi.keybd_event(Keys.F, 0, WinApi.KEYEVENTF_KEYUP, 0);

                                        WinApi.keybd_event(Keys.O, 0, 0, 0);
                                        WinApi.keybd_event(Keys.O, 0, WinApi.KEYEVENTF_KEYUP, 0);

                                        WinApi.keybd_event(Keys.R, 0, 0, 0);
                                        WinApi.keybd_event(Keys.R, 0, WinApi.KEYEVENTF_KEYUP, 0);

                                        WinApi.keybd_event(Keys.E, 0, 0, 0);
                                        WinApi.keybd_event(Keys.E, 0, WinApi.KEYEVENTF_KEYUP, 0);

                                        WinApi.keybd_event(Keys.V, 0, 0, 0);
                                        WinApi.keybd_event(Keys.V, 0, WinApi.KEYEVENTF_KEYUP, 0);

                                        WinApi.keybd_event(Keys.E, 0, 0, 0);
                                        WinApi.keybd_event(Keys.E, 0, WinApi.KEYEVENTF_KEYUP, 0);

                                        WinApi.keybd_event(Keys.R, 0, 0, 0);
                                        WinApi.keybd_event(Keys.R, 0, WinApi.KEYEVENTF_KEYUP, 0);

                                        WinApi.keybd_event(Keys.H, 0, 0, 0);
                                        WinApi.keybd_event(Keys.H, 0, WinApi.KEYEVENTF_KEYUP, 0);

                                        WinApi.keybd_event(Keys.O, 0, 0, 0);
                                        WinApi.keybd_event(Keys.O, 0, WinApi.KEYEVENTF_KEYUP, 0);

                                        WinApi.keybd_event(Keys.T, 0, 0, 0);
                                        WinApi.keybd_event(Keys.T, 0, WinApi.KEYEVENTF_KEYUP, 0);

                                        WinApi.keybd_event(Keys.D1, 0, 0, 0);
                                        WinApi.keybd_event(Keys.D1, 0, WinApi.KEYEVENTF_KEYUP, 0);

                                        WinApi.keybd_event(Keys.D0, 0, 0, 0);
                                        WinApi.keybd_event(Keys.D0, 0, WinApi.KEYEVENTF_KEYUP, 0);

                                        WinApi.keybd_event(Keys.D1, 0, 0, 0);
                                        WinApi.keybd_event(Keys.D1, 0, WinApi.KEYEVENTF_KEYUP, 0);

                                        WinApi.keybd_event(Keys.D9, 0, 0, 0);
                                        WinApi.keybd_event(Keys.D9, 0, WinApi.KEYEVENTF_KEYUP, 0);
                                        Thread.Sleep(10);

                                        AndCondition __pcTarget = new AndCondition(
                                            new PropertyCondition(AutomationElement.NameProperty, "登   录"),
                                            new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Button));
                                        AutomationElementCollection __AutoEleCll = item.FindAll(TreeScope.Descendants, __pcTarget);
                                        if (__AutoEleCll.Count > 0)
                                        {
                                            foreach (AutomationElement Loginitem in __AutoEleCll)
                                            {
                                                //登陆框的位置
                                                System.Windows.Rect LoginRect = Loginitem.Current.BoundingRectangle;

                                                //元素 左上角位置
                                                System.Drawing.Point _tPoint = new System.Drawing.Point();
                                                _tPoint.X = Convert.ToInt32(LoginRect.Left);
                                                _tPoint.Y = Convert.ToInt32(LoginRect.Top);
                                                //元素正中间位置
                                                System.Drawing.Point _CenterPoint = new System.Drawing.Point();
                                                _CenterPoint.X = Convert.ToInt32(LoginRect.Left + LoginRect.Width / 2);
                                                _CenterPoint.Y = Convert.ToInt32(LoginRect.Top + LoginRect.Height / 2);
                                                //设置鼠标到指定位置
                                                WinApi.SetCursorPos(_CenterPoint);
                                                //模拟鼠标点击
                                                WinApi.mouse_event(WinApi.MOUSEEVENTF_LEFTDOWN | WinApi.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                                            }
                                        }
                                    }
                                }
                                #endregion
                            }
                        }
                    }
                }
            }
        }

    }

    public class SendKeys
    {
        StringBuilder builder = new StringBuilder();

        public string Alt = "%";
        public string ContextMenu = "+{F10}";
        public string Ctrl = "^";
        public string Shift = "+";
        public string Enter = "{Enter}";
        public string Delete = "{Del}";
        public string Save = "^S";
        public string SaveAll = "^+S";
        public string Copy = "^C";
        public string Cut = "^X";
        public string Paste = "^V";
        public string Undo = "^Z";
        public string Redo = "^Y";
        public string Print = "^P";
        public string Help = "{F1}";
        public string New = "^N";

        public string[] Keys { get; set; }

        public void Sendkeys(AutomationElement element, string[] keys)
        {
            this.Keys = keys;
            try
            {
                element.SetFocus();
            }
            catch (Exception exception)
            {
                throw new Exception("Cannot set focus to this element.", exception);
            }
            string myKeys = "";
            foreach (string str2 in this.Keys)
            {
                myKeys = myKeys + str2;
            }
            Thread.Sleep(200);
            if ((this.ContainsUnescapedKey(myKeys, '^') || this.ContainsUnescapedKey(myKeys, '%')) || this.ContainsUnescapedKey(myKeys, '+'))
            {
                myKeys = myKeys.ToLower();
            }
            System.Windows.Forms.SendKeys.SendWait(myKeys);
            Thread.Sleep(0x3e8);
        }

        public void Sendkeys(AutomationElement element, string myKeys)
        {
            this.Keys = new string[1];
            this.Keys[0] = myKeys;
            try
            {
                element.SetFocus();
            }
            catch (Exception exception)
            {
                throw new Exception("Cannot set focus to this element.", exception);
            }
            Thread.Sleep(200);
            if ((this.ContainsUnescapedKey(myKeys, '^') || this.ContainsUnescapedKey(myKeys, '%')) || this.ContainsUnescapedKey(myKeys, '+'))
            {
                myKeys = myKeys.ToLower();
            }
            System.Windows.Forms.SendKeys.SendWait(myKeys);
            Thread.Sleep(0x3e8);
        }

        private bool ContainsUnescapedKey(string keys, char key)
        {
            for (int i = 0; i < keys.Length; i++)
            {
                if (keys[i] == key)
                {
                    if ((i == 0) || (i == (keys.Length - 1)))
                    {
                        return true;
                    }
                    if ((keys[i - 1] != '{') || (keys[i + 1] != '}'))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private string KeysToString(string[] keys)
        {

            if (keys != null)
            {
                for (int i = 0; i < keys.Length; i++)
                {
                    string str = keys[i];
                    if (str == null)
                    {
                        builder.Append(keys[i]);
                    }
                    int length = keys.Length - 1;
                    switch (str)
                    {
                        case "^":
                            builder.Append("Ctrl");
                            IsEquals(i, length, builder);
                            break;
                        case "+{F10}":
                            builder.Append("Open Context Menu");
                            IsEquals(i, length, builder);
                            break;
                        case "%":
                            builder.Append("Alt");
                            IsEquals(i, length, builder);
                            break;
                        case "+":
                            builder.Append("Shift");
                            IsEquals(i, length, builder);
                            break;
                        case "^S":
                            builder.Append("Save");
                            IsEquals(i, length, builder);
                            break;
                        case "^X":
                            builder.Append("Cut");
                            IsEquals(i, length, builder);
                            break;
                        case "^C":
                            builder.Append("Copy");
                            IsEquals(i, length, builder);
                            break;
                        case "^V":
                            builder.Append("Paste");
                            IsEquals(i, length, builder);
                            break;
                        case "^+S":
                            builder.Append("Save All");
                            IsEquals(i, length, builder);
                            break;
                        case "^P":
                            builder.Append("Print");
                            IsEquals(i, length, builder);
                            break;
                        case "^Z":
                            builder.Append("Undo");
                            IsEquals(i, length, builder);
                            break;
                        case "^Y":
                            builder.Append("Redo");
                            IsEquals(i, length, builder);
                            break;
                        case "^N":
                            builder.Append("New");
                            IsEquals(i, length, builder);
                            break;
                        default:
                            builder.Append(keys[i]);
                            IsEquals(i, length, builder);
                            break;
                    }
                }
            }
            return builder.ToString();
        }

        void IsEquals(int i, int length, StringBuilder builder)
        {
            if (i < length)
                builder.Append("+");
        }

        #region Public Method

        public override string ToString()
        {
            return string.Format("Sendkeys to input data or operator with keys = '{0}'",
                this.KeysToString(Keys));
        }
        #endregion
    }
}
