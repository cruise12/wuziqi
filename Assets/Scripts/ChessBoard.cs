
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChessBoard : MonoBehaviour
{
    private static ChessBoard _instance;
    public Stack<GameObject> ChessPath;
    public Image gameEnd;
    public Button Start;
    public static ChessBoard Instance
    {
        get
        {
            return _instance;
        }
    }
    public ChessType turn;
    public int[,] grid;
    public float timer = 0;
    public GameObject[] prefabs;
    public bool GameStart;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        grid = new int[15, 15];
        ChessPath = new Stack<GameObject>();
    }
    private void Update()
    {
        timer += Time.deltaTime;
    }
    public void gameStart()
    {
        
        GameStart = true;
        gameEnd.gameObject.SetActive(false);
        Start. gameObject.SetActive(false);


    }
    public void LoadScene()
    {
        SceneManager.LoadScene(0);
       
    }
    public bool PlayChess(int[] pos)
    {

        if (!GameStart) return false;

        if (grid[pos[0], pos[1]] != 0 )//判断是否重复
        {
            return false;
        }
        if (turn == ChessType.Black)//自己下棋
        {
          ChessPath.Push ( Instantiate(prefabs[0], new Vector3(pos[0] , pos[1] , 0), Quaternion.identity));
            grid[pos[0], pos[1]] = 1;

            if (CheckWinner(pos)) { GameEnd(); }
            turn = ChessType.White;
        }
        else if (turn == ChessType.White)
        {
          ChessPath.Push(  Instantiate(prefabs[1], new Vector3(pos[0] , pos[1] , 0), Quaternion.identity));
            grid[pos[0], pos[1]] = 2;
            if (CheckWinner(pos))
            { GameEnd(); }
            turn = ChessType.Black;
        }
        return true;
    }
    public void GameEnd()
    {
        GameStart = false;
        Debug.Log("游戏结束");
        gameEnd.gameObject.SetActive(true);
       
        
           

    }
    public bool CheckWinner(int[] pos)
    {
        if (CheckOneLine(pos, new int[2] { 1, 0 })) return true;
        if (CheckOneLine(pos, new int[2] { 0, 1 })) return true;
        if (CheckOneLine(pos, new int[2] { 1, 1 })) return true;
        if (CheckOneLine(pos, new int[2] { 1, -1 })) return true;
        return false;
    }
    public bool CheckOneLine(int[] pos, int[] offest)
    {
        int LinkNum = 1;//右边计算
        for (int x=pos[0]+offest[0], y= pos[1]+offest[1]; (x<15&&x>0)&&(y>0&&y<15); x += offest[0], y+=offest[1])
        {
            if (grid[x,y] == (int)turn)
            {
                LinkNum++;

            }
            else break;
        }
        for (int x = pos[0] - offest[0], y = pos[1] - offest[1]; (x < 15 && x > 0) && (y > 0 && y < 15); x -= offest[0], y -= offest[1])
        {
            if (grid[x, y] == (int)turn)
            {
                LinkNum++;

            }
            else break;
        }


        if (LinkNum > 4) { return true; }
        return false;

    }//判断旁边是否有棋子
public void ReSetChess()
    {
        if (ChessPath.Count > 0)
        {
            GameObject temp = (GameObject)ChessPath.Pop();
            grid[(int)(temp.transform.position.x), (int)(temp.transform.position.y)] = 0;
            Destroy(temp);
          
        }
    }


    public enum ChessType
    {
        Watch,
        Black,
        White,
    }

}