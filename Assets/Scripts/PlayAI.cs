using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAI : Player
{
    SortedList<string, int> toScore;
    private void Start()
    {
        toScore = new SortedList<string, int>();
        toScore.Add("_*_", 20);
        toScore.Add("_**_",120);
        toScore.Add("_**", 50);
        toScore.Add("**_", 50);
        toScore.Add("_***_",720);
        toScore.Add("_***",600);
        toScore.Add("***_",600);
        toScore.Add("****_",720);
        toScore.Add("**_**_", 720);
        toScore.Add("**_*_", 720);
        toScore.Add("_*_**", 720);
        toScore.Add("_****_",5000);
        toScore.Add("*****",100000);
        
    }
    private void Update()
    {
        if (ChessBoard.Instance.timer > 0.3f)
            PlayChess();
    }
    public int CheckOneLine(int[] pos, int[] offest , int chessColor )
    {
        string temp = "*";
    
        for (int x = pos[0] + offest[0], y = pos[1] + offest[1], rem=0; (x < 15 && x > 0) && (y > 0 && y < 15)&&rem<=5; x=x+ offest[0], y += offest[1],rem++)
        {

            if (ChessBoard.Instance.grid[x, y] == (int)chessColor)
            {
                temp += "*";

            }
            else if (ChessBoard.Instance.grid[x, y] == 0)
            {
                temp += "_";
               
            }
            else  break;
        }
        
        for (int x = pos[0] - offest[0], y = pos[1] - offest[1],rem=0; (x < 15 && x > 0) && (y > 0 && y < 15)&&rem<=5; x -= offest[0], y -= offest[1],rem++)
        {
            if (ChessBoard.Instance.grid[x, y] == (int)chessColor)
            {
                temp = "*" + temp;
             

            }
            else if (ChessBoard.Instance.grid[x, y] == 0)
            {
                temp = "_" + temp;
                break;
            }
            else break;
        }
        int MaxScore = 0;
        foreach (var item in toScore)//返回一个最大值
        {
            if (temp.Contains((item.Key)) && toScore[item.Key] > MaxScore)
            {
                MaxScore = toScore[item.Key];
            }

        }
        return MaxScore;
    }
    int SetScore(int[]pos)
    {
        int score = 0;
        score += CheckOneLine(pos, new int[] { 1, 0 },1);
        score += CheckOneLine(pos, new int[] { 0, 1 },1);
        score += CheckOneLine(pos, new int[] { 1, 1 },1);
        score += CheckOneLine(pos, new int[] { 1, -1 },1);
        score += CheckOneLine(pos, new int[] { 1, 0 }, 2);
        score += CheckOneLine(pos, new int[] { 0, 1 }, 2);
        score += CheckOneLine(pos, new int[] { 1, 1 }, 2);
        score += CheckOneLine(pos, new int[] { 1, -1 }, 2);
        return score;
       
    }

    public override void PlayChess()
    {
        if (chessColor != ChessBoard.Instance.turn)
            return;
        int MaxX = 7, MaxY = 7, MaxScore = 80;
        if (ChessBoard.Instance.grid[MaxX,MaxY] != 0) MaxScore = 0;
        for(int x = 0; x <=14; x++)
        {
            for(int y = 0; y <= 14; y++)
            {
                if (ChessBoard.Instance.grid[x, y] != 0) continue;
                int score = SetScore(new int[] { x, y });
                if(score>MaxScore)
              {
                    MaxX = x;
                    MaxY = y;
                    MaxScore = score ;  

                }
            }
        }
        ChessBoard.Instance.PlayChess(new int[] { MaxX, MaxY });
       
    }

}
