using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MyClockApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 得到当前系统时间，并将其拼接成一个字符串
        /// </summary>
        /// <returns>数字时钟要显示的字符串</returns>
        public string GetTime() 
        {
            String TimeInString = "";
            int hour = DateTime.Now.Hour;
            int min = DateTime.Now.Minute;
            int sec = DateTime.Now.Second;
            //将时，分，秒连成一个字符串
            TimeInString = (hour < 10) ? "0" + hour.ToString() : hour.ToString();
            TimeInString += ":" + ((min < 10) ? "0" + min.ToString() : min.ToString());
            TimeInString += ":" + ((sec < 10) ? "0" + sec.ToString() : sec.ToString());
            
            return TimeInString;
        }
        /// <summary>
        /// 在窗体上画表盘时钟的图形
        /// </summary>
        /// <param name="h"></param>
        /// <param name="m"></param>
        /// <param name="s"></param>
        private void MyDrawClock(int h, int m, int s)
        { 
            Graphics g = this.CreateGraphics();
            Rectangle rect = this.ClientRectangle;

            g.Clear(Color.White);

            Pen myPen = new Pen(Color.Black, 1);
            g.DrawEllipse(myPen, this.ClientRectangle.Width / 2 - 75, this.ClientRectangle.Height / 2-75, 150, 150);//画表盘

            Point centerPoint = new Point(this.ClientRectangle.Width / 2, this.ClientRectangle.Height / 2);//表的中心点
            //计算出秒针，时针，分针的另外一个商点
            Point secPoint = new Point((int)(centerPoint.X + (Math.Sin(s * Math.PI / 30) * 50)), (int)(centerPoint.Y - (Math.Cos(s * Math.PI / 30) * 50)));
            Point minPoint = new Point((int)(centerPoint.X + (Math.Sin(m * Math.PI / 30) * 40)), (int)(centerPoint.Y - (Math.Cos(m * Math.PI / 30) * 40)));
            Point hourPoint = new Point((int)(centerPoint.X + (Math.Sin(h * Math.PI / 6) * 30) - m * Math.PI / 360), (int)(centerPoint.Y - (Math.Cos(h * Math.PI / 6) * 30) - m * Math.PI / 360));
            
            //以不同颜色和宽度绘制表针
            myPen = new Pen(Color.Red, 1);
            g.DrawLine(myPen, centerPoint, secPoint);
            myPen = new Pen(Color.Green, 2);
            g.DrawLine(myPen, centerPoint, minPoint);
            myPen = new Pen(Color.Orange, 4);
            g.DrawLine(myPen, centerPoint, hourPoint);
        }
        /// <summary>
        /// 定时刷新显示时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            int h = DateTime.Now.Hour;
            int s = DateTime.Now.Second;
            int m = DateTime.Now.Minute;

            MyDrawClock(h, m, s);
            toolStripStatusLabel1.Text = string.Format("{0}:{1}:{2}", h, m, s);
            lblTime.Text = GetTime();
        }
        /// <summary>
        /// 若无此方法，时钟也能显示，但要等窗体显示几秒以后表盘才会显示。有了此方法窗体和表盘同时显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            int h = DateTime.Now.Hour;
            int s = DateTime.Now.Second;
            int m = DateTime.Now.Minute;

            MyDrawClock(h, m, s);
            toolStripStatusLabel1.Text = string.Format("{0}:{1}:{2}", h, m, s);
            lblTime.Text = GetTime();
        }


    }
}