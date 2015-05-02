using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyGobang
{
    public partial class Form1 : Form
    {

        private bool isBlack = true;
        public static int[,] panel = new int[16, 17];//对棋子进行判断：0表示没有棋子，1表示黑子，2表示白子。
        private float last_x = -1, last_y = -1;
        private int xx, yy;
        private float last_white_x, last_white_y;
        private float last_black_x, last_black_y;
        public Form1()
        {
            InitializeComponent();
            panel1.Enabled = false;
            panel1.MouseClick += new MouseEventHandler(Form1_MouseClick);
            panel1.MouseMove += new MouseEventHandler(Form1_MouseMove);
        }

        public void init_Panel()//初始化棋盘
        {
            int i, j;
            for (i = 0; i < 16; i++)
            {
                for (j = 0; j < 16; j++)
                {
                    panel[i, j] = 0;
                }
            }
        }
        
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen myPen = new Pen(Color.Black);
            float x = 10, y = 10;
            while (x <= 310)
            {              
                g.DrawLine(myPen, x, 10, x, 310);
                x = x + 20;
            }
            while (y <= 310)
            {
                g.DrawLine(myPen, 10, y, 310, y);
                y = y + 20;
            }
        }



        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            SolidBrush whiteBrush = new SolidBrush(Color.White);
            Graphics g = panel1.CreateGraphics();
            float temp_x = e.X;
            float temp_y = e.Y;
            float paint_x;
            float paint_y;
            float x, y;
            int xx, yy;
            AI ai = new AI();
            Win win = new Win();
            bool flag = true;

            if ((temp_x + 10) % 20 <= 10)//取X的坐标
            {
                x = temp_x - (temp_x + 10) % 20;
            }
            else
            {
                x = temp_x + 20 - (temp_x + 10) % 20;
            }

            if ((temp_y + 10) % 20 <= 10)//取Y的坐标
            {
                y = temp_y - (temp_y + 10) % 20;
            }
            else
            {
                y = temp_y + 20 - (temp_y + 10) % 20;
            }

            xx = (int)(x - 10) / 20;
            yy = (int)(y - 10) / 20;
            if (panel[xx, yy] != 0)
            {
                MessageBox.Show("此处已被棋子所占!", "温馨提示");
            }
            else
            {
                if (isBlack)//判断走黑子还是白子
                {
                    last_black_x = x;
                    last_black_y = y;
                    g.FillEllipse(blackBrush, x - 10, y - 10, 20, 20);
                    isBlack = false;
                    panel[xx, yy] = 1;
                    label1.Text=Convert.ToString(xx) + " " + Convert.ToString(yy);                    
                    if (win.judge(panel, xx, yy))
                    {
                        MessageBox.Show("黑棋获胜！");
                        panel1.Enabled = false;
                        PlayerradioButton.Enabled = true;
                        ComputerradioButton.Enabled = true;
                        flag = false;
                    }

                    if (flag)
                    {
                        ai.find_best(panel, isBlack);
                        xx = ai.best_x;
                        yy = ai.best_y;
                        paint_x = xx * 20 + 10;
                        paint_y = yy * 20 + 10;
                        last_white_x = paint_x;
                        last_white_y = paint_y;
                        g.FillEllipse(whiteBrush, paint_x - 10, paint_y - 10, 20, 20);
                        isBlack = true;
                        panel[xx, yy] = 2;
                        label2.Text = Convert.ToString(xx) + " " + Convert.ToString(yy);
                        if (win.judge(panel, xx, yy))
                        {
                            MessageBox.Show("白棋获胜！");
                            panel1.Enabled = false;
                            PlayerradioButton.Enabled = true;
                            ComputerradioButton.Enabled = true;
                        }
                    }                 
                }
                else
                {
                    last_white_x = x;
                    last_white_y = y;
                    g.FillEllipse(whiteBrush, x - 10, y - 10, 20, 20);
                    isBlack = true;
                    panel[xx, yy] = 2;
                    label1.Text=Convert.ToString(xx) + " " + Convert.ToString(yy);
                    if (win.judge(panel, xx, yy))
                    {
                        MessageBox.Show("白棋获胜！");
                        panel1.Enabled = false;
                        PlayerradioButton.Enabled = true;
                        ComputerradioButton.Enabled = true;
                        flag = false;
                    }
                    if (flag)
                    {
                        ai.find_best(panel, isBlack);
                        xx = ai.best_x;
                        yy = ai.best_y;
                        paint_x = xx * 20 + 10;
                        paint_y = yy * 20 + 10;
                        last_black_x = paint_x;
                        last_black_y = paint_y;
                        g.FillEllipse(blackBrush, paint_x - 10, paint_y - 10, 20, 20);
                        isBlack = false;
                        panel[xx, yy] = 1;
                        label2.Text = Convert.ToString(xx) + " " + Convert.ToString(yy);
                        if (win.judge(panel, xx, yy))
                        {
                            MessageBox.Show("黑棋获胜！");
                            panel1.Enabled = false;
                            PlayerradioButton.Enabled = true;
                            ComputerradioButton.Enabled = true;
                        }
                    }                   
                }
            }
            //throw new NotImplementedException();
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {   // e.X和e.Y为MouseEventArgs类返回的当前鼠标位置坐标
           // base.OnMouseMove(e);
            Graphics graphics = panel1.CreateGraphics();
            Pen pen = new Pen(Color.Red);
            Pen last_pen = new Pen(Form.DefaultBackColor);
            float temp_x = e.X;
            float temp_y = e.Y;
            float x, y;

            xx = (int)(last_x - 10) / 20;
            yy = (int)(last_y - 10) / 20;
            if (last_x != -1 && panel[xx, yy] == 0)
            {
                graphics.DrawLine(last_pen, last_x - 4, last_y - 4, last_x - 4, last_y - 8);
                graphics.DrawLine(last_pen, last_x - 4, last_y - 4, last_x - 8, last_y - 4);
                graphics.DrawLine(last_pen, last_x + 4, last_y - 4, last_x + 4, last_y - 8);
                graphics.DrawLine(last_pen, last_x + 4, last_y - 4, last_x + 8, last_y - 4);
                graphics.DrawLine(last_pen, last_x - 4, last_y + 4, last_x - 8, last_y + 4);
                graphics.DrawLine(last_pen, last_x - 4, last_y + 4, last_x - 4, last_y + 8);
                graphics.DrawLine(last_pen, last_x + 4, last_y + 4, last_x + 4, last_y + 8);
                graphics.DrawLine(last_pen, last_x + 4, last_y + 4, last_x + 8, last_y + 4);
            }
            
            if ((temp_x + 10) % 20 <= 10)//取X的坐标
            {
                x = temp_x - (temp_x + 10) % 20;
            }
            else
            {
                x = temp_x + 20 - (temp_x + 10) % 20;
            }

            if ((temp_y + 10) % 20 <= 10)//取Y的坐标
            {
                y = temp_y - (temp_y + 10) % 20;
            }
            else
            {
                y = temp_y + 20 - (temp_y + 10) % 20;
            }
            xx = (int)(x - 10) / 20;
            yy = (int)(y - 10) / 20;
            if (x > 0 && x < 320 && y > 0 && y < 320 && panel[xx, yy] == 0)
            {
                graphics.DrawLine(pen, x - 4, y - 4, x - 4, y - 8);
                graphics.DrawLine(pen, x - 4, y - 4, x - 8, y - 4);
                graphics.DrawLine(pen, x + 4, y - 4, x + 4, y - 8);
                graphics.DrawLine(pen, x + 4, y - 4, x + 8, y - 4);
                graphics.DrawLine(pen, x - 4, y + 4, x - 8, y + 4);
                graphics.DrawLine(pen, x - 4, y + 4, x - 4, y + 8);
                graphics.DrawLine(pen, x + 4, y + 4, x + 4, y + 8);
                graphics.DrawLine(pen, x + 4, y + 4, x + 8, y + 4);

                last_x = x;
                last_y = y;
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            SolidBrush whiteBrush = new SolidBrush(Color.White);
            Graphics g = panel1.CreateGraphics();
            panel1.Enabled = true;
            PlayerradioButton.Enabled = false;
            ComputerradioButton.Enabled = false;
            init_Panel();
            if (ComputerradioButton.Checked)
            {
                g.FillEllipse(blackBrush, 140, 140, 20, 20);
                panel[7 ,7] = 1;
                isBlack = false;
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定退出游戏？", "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                this.Close();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            SolidBrush backBrush = new SolidBrush(Form.DefaultBackColor);
            Graphics graphics = panel1.CreateGraphics();
            Pen myPen = new Pen(Color.Black);
            //label1.Text = Convert.ToString(last_white_x) + " " + Convert.ToString(last_white_y);
            graphics.FillEllipse(backBrush, last_white_x - 10, last_white_y - 10, 20, 20);
            graphics.FillEllipse(backBrush, last_black_x - 10, last_black_y - 10, 20, 20);
            int x = (int)(last_white_x - 10) / 20;
            int y = (int)(last_white_y - 10) / 20;
            panel[x, y] = 0;
            x = (int)(last_black_x - 10) / 20;
            y = (int)(last_black_y - 10) / 20;
            panel[x, y] = 0;

            graphics.DrawLine(myPen, last_white_x - 10, last_white_y, last_white_x + 10, last_white_y);
            graphics.DrawLine(myPen, last_white_x, last_white_y - 10, last_white_x, last_white_y + 10);

            graphics.DrawLine(myPen, last_black_x - 10, last_black_y, last_black_x + 10, last_black_y);
            graphics.DrawLine(myPen, last_black_x, last_black_y - 10, last_black_x, last_black_y + 10);
        }

    }
}
