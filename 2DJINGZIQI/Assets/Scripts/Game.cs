using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Game : MonoBehaviour
{
    public Button[] buttons; // 用于存储按钮
    public TextMeshProUGUI resultText; // 用于显示结果
    private string currentPlayer; // 当前玩家
    private string[,] board; // 游戏板

    void Start()
    {
        // 初始化游戏
        board = new string[3, 3];
        currentPlayer = "X"; // 玩家 X 先手
        Debug.Log(buttons.Length); // 检查按钮数组的长度
        Debug.Log(resultText); // 检查 resultText 是否被正确赋值
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
   
            button.GetComponentInChildren<TextMeshProUGUI>().text = currentPlayer;

            if (CheckForWin(currentPlayer))
            {
                resultText.text = currentPlayer + " win! ";
                DisableButtons();
            }
            else if (IsBoardFull())
            {
                resultText.text = "PING!";
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
        // 简单的随机选择空格
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == null)
                {
                    board[i, j] = currentPlayer;
                    buttons[i * 3 + j].GetComponentInChildren<TextMeshProUGUI>().text = currentPlayer;

                    if (CheckForWin(currentPlayer))
                    {
                        resultText.text = currentPlayer + " win!";
                        DisableButtons();
                    }
                    else if (IsBoardFull())
                    {
                        resultText.text = "PING!";
                    }
                    currentPlayer = "X"; // 切换回玩家
                    return;
                }
            }
        }
    }

    bool CheckForWin(string player)
    {
        // 检查行、列和对角线
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
