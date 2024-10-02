using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Game : MonoBehaviour
{
    public Button[] buttons; // ���ڴ洢��ť
    public TextMeshProUGUI resultText; // ������ʾ���
    private string currentPlayer; // ��ǰ���
    private string[,] board; // ��Ϸ��

    void Start()
    {
        // ��ʼ����Ϸ
        board = new string[3, 3];
        currentPlayer = "X"; // ��� X ����
        Debug.Log(buttons.Length); // ��鰴ť����ĳ���
        Debug.Log(resultText); // ��� resultText �Ƿ���ȷ��ֵ
        resultText.text = "";


        // Ϊÿ����ť��ӵ���¼�
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


        // ��鰴ť�Ƿ��Ѿ������
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
                currentPlayer = (currentPlayer == "X") ? "O" : "X"; // �л����
                if (currentPlayer == "O") // ���ԵĻغ�
                {
                    MakeComputerMove();
                }
            }
        }
    }

    void MakeComputerMove()
    {
        // �򵥵����ѡ��ո�
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
                    currentPlayer = "X"; // �л������
                    return;
                }
            }
        }
    }

    bool CheckForWin(string player)
    {
        // ����С��кͶԽ���
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
            button.interactable = false; // �������а�ť
        }
    }
}
