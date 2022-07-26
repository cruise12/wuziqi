using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ChessBoard;

public class Player : MonoBehaviour
{
    public ChessType chessColor;

    private void  Update()
    {
        if (ChessBoard.Instance.timer > 0.3f)

        { PlayChess(); }
    

    }
       
    
    public virtual void PlayChess()
    {//下棋
       
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);//将鼠标转化为屏幕坐标
            
            if (pos[0] <0  || pos[0] > 14 || pos[1] < 0 || pos[1] > 14||chessColor!=ChessBoard.Instance.turn)
                return;
            ChessBoard.Instance.PlayChess(new int[2] { (int)(pos.x+0.5 ), (int)(pos.y+0.5) });
            Debug.Log((int)(pos.x+0.5) + " " + (int)(pos.y+0.5));
            ChessBoard.Instance.timer = 0;
            
        }
    }
}
