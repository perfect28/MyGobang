using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyGobang
{
    class Win
    {
        private int[,] win = new int[16, 16];
        public bool flag;
        public Win() { }

        public Win(int[,] a)
        {
            this.win = a;
        }

        public bool judge(int[,] win,int x, int y)
        {
            if (isWiner_Across(win, x, y) || isWiner_Erect(win, x, y) || isWiner_LeftTop(win, x, y) || isWiner_RightTop(win, x, y))
                return true;
            else return false;
        }

        public bool isWiner_Across(int[,] win, int x, int y)//判断输赢，横方向。
        {
            int st = (x - 4 >= 0) ? x - 4 : 0;
            int ed = (x + 4 < 16) ? x : 11;
            int i, j;
            for (j = st; j <= ed; j++)
            {
                flag = true;
                for (i = j; i <= j + 3; i++)
                    if (win[i, y] == win[i + 1, y]) continue;
                    else
                    {
                        flag = false;
                        break;
                    }
                if (i == j + 4) break;
            }

            if (flag) return true;
            else return false;
        }

        public bool isWiner_Erect(int[,] win, int x, int y)//判断输赢，竖方向。
        {          
            int st = (y - 4 >= 0) ? y - 4 : 0;
            int ed = (y + 4 < 16) ? y : 11;
            int i, j;
            for (i = st; i <= ed; i++)
            {
                flag = true;
                for (j = i; j <= i + 3; j++)
                    if (win[x, j] == win[x, j + 1]) continue;
                    else
                    {
                        flag = false;
                        break;
                    }
                if (j == i + 4) break;
            }

            if (flag) return true;
            else return false;
        }

        public bool isWiner_LeftTop(int[,] win, int x, int y)//判断输赢，左上方向。
        {
            int i, j;
            int cnt = 1;
            for (i = x, j = y; i >= 0 && j >= 0; i--, j--)
            {
                if (i - 1 >= 0 && j - 1 >= 0 && win[i, j] == win[i - 1, j - 1])
                {
                    cnt++;
                }
                else break;
            }

            for (i = x, j = y; i < 16 && j < 16; i++, j++)
            {
                if (i + 1 < 16 && j + 1 <16 && win[i, j] == win[i + 1, j + 1])
                {
                    cnt++;
                }
                else break;
            }

            if (cnt >= 5) return true;
            else return false;
        }

        public bool isWiner_RightTop(int[,] win, int x, int y)//判断输赢，右上方向
        {
            int i, j;
            int cnt = 1;

            for (i = x, j = y; i >= 0 && j < 16; i--, j++)
            {
                if (i - 1 >= 0 && j + 1 <= 16 && win[i, j] == win[i - 1, j + 1])
                {
                    cnt++;
                }
                else break;
            }

            for (i = x, j = y; i < 16 && j >= 0; i++, j--)
            {
                if (i + 1 < 16 && j - 1 >= 0 && win[i, j] == win[i + 1, j - 1])
                {
                    cnt++;
                }
                else break;
            }

            if (cnt >= 5) return true;
            else return false;
        }

    }
}
