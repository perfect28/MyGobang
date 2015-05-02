using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGobang
{
    class AI
    {
        public int best_x, best_y;
        
        public AI(){}


        /*
            //设置本方棋型分值
        	chessModeSelfCent[(int)ChessMode.Five] = 10000;//已胜出
            chessModeSelfCent[(int)ChessMode.LiveFour] = 8000;//落子胜出
            chessModeSelfCent[(int)ChessMode.SideFour] = 8000;//落子胜出
			chessModeSelfCent[(int)ChessMode.LiveThree] = 1400;//落子必胜
			chessModeSelfCent[(int)ChessMode.SideThree] = 700;//双边三落子必胜
			chessModeSelfCent[(int)ChessMode.LiveTwo] = 300;//双活二落子必胜
			chessModeSelfCent[(int)ChessMode.SideTwo] = 150;
			chessModeSelfCent[(int)ChessMode.LiveOne] = 50;
			chessModeSelfCent[(int)ChessMode.SideOne] = 25;
			chessModeSelfCent[(int)ChessMode.LiveZero] = 10;
			chessModeSelfCent[(int)ChessMode.SideZero] = 5;
			//设置对方棋型分值
			chessModeRivalCent = new int[16];
			chessModeRivalCent[(int)ChessMode.Five] = 10000;
            chessModeRivalCent[(int)ChessMode.LiveFour] = 8000;//落子胜出
            chessModeRivalCent[(int)ChessMode.SideFour] = 8000;//落子胜出
            chessModeRivalCent[(int)ChessMode.LiveThree] = 1300;//落子必胜
            chessModeRivalCent[(int)ChessMode.SideThree] = 500;//双边三落子必胜
            chessModeRivalCent[(int)ChessMode.LiveTwo] = 260;//
            chessModeRivalCent[(int)ChessMode.SideTwo] = 100;
            chessModeRivalCent[(int)ChessMode.LiveOne] = 20;
            chessModeRivalCent[(int)ChessMode.SideOne] = 10;
            chessModeRivalCent[(int)ChessMode.LiveZero] = 5;
            chessModeRivalCent[(int)ChessMode.SideZero] = 1;

        */

        public bool arround(int [,] array,int x,int y)
        {
            bool flag = false;

            if (y > 0 && array[x, y - 1] != 0) flag = true;
            if (y < 15 && array[x, y + 1] != 0) flag = true;
            if (x > 0 && array[x - 1, y] != 0) flag = true;
            if (x < 15 && array[x + 1, y] != 0) flag = true;

            if (x > 0 && y > 0 && array[x - 1, y - 1] != 0) flag = true;
            if (x > 0 && y < 15 && array[x - 1, y + 1] != 0) flag = true;
            if (x < 15 && y > 0 && array[x + 1, y - 1] != 0) flag = true;
            if (x < 15 && y < 15 && array[x + 1, y + 1] != 0) flag = true;
            return flag;
        }
        public void find_best(int [,] array,bool isBlack)
        {
            int x, y;
            int best=0;
            for (x = 0; x < 16; x++)
                for (y = 0; y < 16; y++)
                    if (array[x, y] == 0 && arround(array,x,y))
                    {
                        int tmp=rank(array,x, y, isBlack);
                        if (best < tmp)
                        {
                            best = tmp;
                            best_x = x;
                            best_y = y;
                        }
                    }
                        
        }

        public int count(int cnt,bool flag)
        {
            int grade = 0;
            switch (cnt)
            {
                case 2:
                    if (flag) grade += 300;
                    else grade += 150;
                    break;
                case 3:
                    if (flag) grade += 2400;
                    else grade += 700;
                    break;
                case 4:
                    if (flag) grade += 20000;
                    else grade += 8000;
                    break;
                case 5:
                    grade += 100000;
                    break;
                default:
                    break;
            }
            return grade;
        }

        public int rank(int [,] arr,int x,int y,bool isBlack)
        {
            int i, j;
            int cnt;
            int grade = 0;
            bool flag;
            //1.寻找黑子
            
            //横线
            cnt = 1;
            flag = true;
            for (i = x - 1, j = y; i >= 0; i--)
            {
                if (i >=0  && arr[i, j] == 1) cnt++;
                else break;
            }
            if (i < 0 || arr[i, j] == 2) flag = false;
                            
            for (i = x + 1, j = y; i < 16; i++)
            {
                if (i < 16 && arr[i, j] == 1) cnt++;
                else break;
            }
            if (i > 15 || arr[i, j] == 2) flag = false;

            grade += count(cnt,flag);           
                                
            //竖线
            cnt = 1;
            flag = true;
            for (i = x, j = y - 1; j >= 0; j--) 
            {
                if (j >= 0 && arr[i, j] == 1) cnt++;
                else break;
            }
            if (j < 0 || arr[i, j] == 2) flag = false;

            for (i = x, j = y + 1; j < 16; j++)
            {
                if (j < 16 && arr[i, j] == 1) cnt++;
                else break;
            }
            if (j > 15 || arr[i, j] == 2) flag = false;

            grade += count(cnt, flag);       

            //左上斜线
            cnt = 1;
            flag = true;
            for (i = x - 1, j = y - 1; i >= 0 && j >= 0; i--, j--)
            {
                if (i >= 0 && j >= 0 && arr[i, j] == 1) cnt++;
                else break;
            }
            if (i >= 0 && j >= 0 && arr[i, j] == 2) flag = false;
            if (i < 0 || j < 0) flag = false;

            for (i = x + 1, j = y + 1; i < 16 && j < 16; i++, j++)
            {
                if ( i < 16 && j < 16 && arr[i, j] == 1) cnt++;
                else break;
            }
            if (i <= 15 && j <= 15 && arr[i, j] == 2) flag = false;
            if (i > 15 || j > 15) flag = false;

            grade += count(cnt, flag);     

            //右上斜线
            cnt = 1;
            flag = true;
            for (i = x - 1, j = y + 1; i >= 0 && j < 16; i--, j++)
            {
                if (i >= 0 && j < 16 && arr[i, j] == 1) cnt++;
                else break;
            }
            if (i >= 0 && j <= 15 && arr[i, j] == 2) flag = false;
            if (i < 0 || j > 15) flag = false;

            for (i = x + 1, j = y - 1; i < 16 && j >= 0; i++, j--)
            {
                if (i < 16 && j >= 0 && arr[i, j] == 1) cnt++;
                else break;
            }
            if (i <= 15 && j >= 0 && arr[i, j] == 2) flag = false;
            if (i > 15 || j < 0) flag = false;

            grade += count(cnt, flag);      
            
            /********************************************/
            
            //2.寻找白子
            //横线
            cnt = 1;
            flag = true;
            for (i = x - 1, j = y; i >= 0; i--)
            {
                if (i >= 0 && arr[i, j] == 2) cnt++;
                else break;
            }
            if (i < 0 || arr[i, j] == 1) flag = false;

            for (i = x + 1, j = y; i < 16; i++)
            {
                if (i < 16 && arr[i, j] == 2) cnt++;
                else break;
            }
            if (i > 15 || arr[i, j] == 1) flag = false;

            grade += count(cnt, flag);     

            //竖线
            cnt = 1;
            flag = true;
            for (i = x, j = y - 1; j >= 0; j--)
            {
                if (j >= 0 && arr[i, j] == 2) cnt++;
                else break;
            }
            if (j < 0 || arr[i, j] ==1) flag = false;

            for (i = x, j = y + 1; j < 16; j++)
            {
                if (j < 16 && arr[i, j] == 2) cnt++;
                else break;
            }
            if (j >15 || arr[i, j] == 1) flag = false;

            grade += count(cnt, flag);     

            //左上斜线
            cnt = 1;
            flag = true;
            for (i = x - 1, j = y - 1; i >= 0 && j >= 0; i--, j--)
            {
                if (i >= 0 && j >= 0 && arr[i, j] == 2) cnt++;
                else break;
            }
            if (i >= 0 && j >= 0 && arr[i, j] == 1) flag = false;
            if (i < 0 || j < 0) flag = false;

            for (i = x + 1, j = y + 1; i < 16 && j < 16; i++, j++)
            {
                if (i < 16 && j < 16 && arr[i, j] == 2) cnt++;
                else break;
            }
            if (i <= 15 && j <= 15 && arr[i, j] == 1) flag = false;
            if (i > 15 || j > 15) flag = false;

            grade += count(cnt, flag);     

            //右上斜线
            cnt = 1;
            flag = true;
            for (i = x - 1, j = y + 1; i >= 0 && j < 16; i--, j++)
            {
                if (i >= 0 && j < 16 && arr[i, j] == 2) cnt++;
                else break;
            }
            if (i >= 0 && j <= 15 && arr[i, j] == 1) flag = false;
            if (i < 0 || j > 15) flag = false;

            for (i = x + 1, j = y - 1; i < 16 && j >= 0; i++, j--)
            {
                if (i < 16 && j >= 0 && arr[i, j] == 2) cnt++;
                else break;
            }
            if (i <= 15 && j >= 0 && arr[i, j] == 1) flag = false;
            if (i > 15 || j < 0) flag = false;

            grade += count(cnt, flag);     

            return grade;
            
        }
    }
}
