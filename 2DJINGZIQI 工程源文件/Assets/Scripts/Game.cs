using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Game : MonoBehaviour
{
    public Button[] buttons; // 用于存储按钮
    public TextMeshProUGUI resultText; // 用于显示结果
    private string currentPlayer; // 当前玩家
    private string[,] board; // 游戏板
    public Sprite[] SP;//存储棋子贴图

    void Start()
    {
        // 初始化游戏
        board = new string[3, 3];
        currentPlayer = "X"; // 玩家 X 先手
        //Debug.Log(buttons.Length); 
        //Debug.Log(resultText); 
        resultText.text = "";


        // 为每个按钮添加点击事件
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => OnButtonClick(button));
        }
    }

    void OnButtonClick(Button button)
    {
        int index = System.Array.IndexOf(buttons, button);
        int row = index / 3;
        int col = index % 3;


        // 检查按钮是否已经被点击
        if (board[row, col] == null)
        {
            board[row, col] = currentPlayer;

            button.image.sprite = SP[1];

            //Debug.Log(currentPlayer);

            if (CheckForWin(currentPlayer))
            {
                resultText.text = currentPlayer + " win! ";
                DisableButtons();
            }
            else if (IsBoardFull())
            {
                resultText.text = "draw!";
            }
            else
            {
                currentPlayer = (currentPlayer == "X") ? "O" : "X"; // 切换玩家
                if (currentPlayer == "O") // 电脑的回合
                {
                    MakeComputerMove();
                }
            }
        }
    }

    void MakeComputerMove()
    {
        //优先让自己获胜
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == null)
                {
                    board[i, j] = currentPlayer; 
                    if (CheckForWin(currentPlayer))
                    {
                        buttons[i * 3 + j].image.sprite = SP[0];
                        DisableButtons();
                        resultText.text = currentPlayer + " win!";
                        return;
                    }
                    board[i, j] = null; 
                }
            }
        }

        // 阻止对手获胜
        string opponent = currentPlayer == "X" ? "O" : "X";
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == null)
                {
                    //Debug.Log(board[i, j]);
                    board[i, j] = opponent; 
                    if (CheckForWin(opponent))
                    {
                        board[i, j] = currentPlayer; // 放置当前玩家的棋子
                        buttons[i * 3 + j].image.sprite = SP[0];
                        currentPlayer = "X"; // 切换回玩家
                        return;
                    }
                    board[i, j] = null;
                }
            }
        }

        //随机选择一个空位
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == null)
                {
                    board[i, j] = currentPlayer;
                    buttons[i * 3 + j].image.sprite = SP[0];

                    if (CheckForWin(currentPlayer))
                    {
                        resultText.text = currentPlayer + " win!";
                        DisableButtons();
                    }
                    else if (IsBoardFull())
                    {
                        resultText.text = "draw!";
                    }
                    currentPlayer = "X"; // 切换回玩家
                    return;
                }
            }
        }
    }

    bool CheckForWin(string player)
    {
        // 检查行、列和对角线判断胜利
        for (int i = 0; i < 3; i++)
        {
            if ((board[i, 0] == player && board[i, 1] == player && board[i, 2] == player) ||
                (board[0, i] == player && board[1, i] == player && board[2, i] == player))
            {
                return true;
            }
        }
        if ((board[0, 0] == player && board[1, 1] == player && board[2, 2] == player) ||
            (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player))
        {
            return true;
        }
        return false;
    }

    bool IsBoardFull()
    {
        foreach (var cell in board)
        {
            if (cell == null) return false;
        }
        return true;
    }

    void DisableButtons()
    {
        foreach (Button button in buttons)
        {
            button.interactable = false; // 禁用所有按钮
        }
    }
}
