using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TicTacToeController : MonoBehaviour
{
    // O, X 입력 버튼
    [SerializeField] private List<Button> buttons = new List<Button>();
    
    // 게임 종료 패널
    public GameObject gameOverPanel;
    // 무승부 패널
    public GameObject gameDrawPanel;
    
    // 턴 오버 변수
    private bool turn;
    // 남은 버튼의 수 
    private int fieldsLeft;
    // 게임 종료 체크
    private bool isGameOver = true;
    
    private void Start()
    {
        GameStart();
    }

    // 게임 시작
    private void GameStart()
    {
        Reset();
    }

    // 게임 시작시 초기화
    private void Reset()
    {
        foreach (Button button in buttons)
        {
            Text text = GetText(button);
            text.color = Color.white;
            text.text = "";
            button.interactable = true;
        }

        fieldsLeft = 9;
        isGameOver = false;
        gameOverPanel.SetActive(false);
        gameDrawPanel.SetActive(false);
    }

    // 선택 영역 텍스트 가져오기
    private Text GetText(Button button)
    {
        return button.GetComponentInChildren<Text>();
    }
    
    //선택 영역 이벤트 처리
    public void OnButtonClick(Button button)
    {
        if (isGameOver)
        {
            Reset();
            return;
        }

        if (fieldsLeft <= 0)
            return;
        
        if (SetMarkAndCheckForWin(button, true))
        {
            Win();
        }

        button.interactable = false;

        if (fieldsLeft <= 0)
        {
            gameDrawPanel.SetActive(true);
            isGameOver = true;
        }

        turn = !turn;
    }

    // 선택 영역 표시 및 승리 조건 확인
    private bool SetMarkAndCheckForWin(Button button, bool colorate = false)
    {
        Text text = GetText(button);
        if (text.text != "")
        {
            return false;
        }

        text.text = turn ? "X" : "O";
        fieldsLeft--;
        return CheckForWin(text.text, colorate);
    }
    
    // 승리시
    private void Win()
    {
        isGameOver = true;
        EnableButtons(false);
        gameOverPanel.SetActive(true);
    }
    
    // 승리 조건 확인
    private bool CheckForWin(string mark, bool colorate = false)
    {
        if (fieldsLeft > 6)
        {
            return false;
        }
        
        // Horizontal
        if (CompareButtons(0, 1, 2, mark, colorate)
            || CompareButtons(3, 4, 5, mark, colorate)
            || CompareButtons(6, 7, 8, mark, colorate)
            // Vertical
            || CompareButtons(0, 3, 6, mark, colorate)
            || CompareButtons(1, 4, 7, mark, colorate)
            || CompareButtons(2, 5, 8, mark, colorate)
            // Diagonal
            || CompareButtons(0, 4, 8, mark, colorate)
            || CompareButtons(6, 4, 2, mark, colorate))
        {
            return true;
        }

        return false;
    }
    
    // 정렬 상태 확인
    private bool CompareButtons(int ind1, int ind2, int ind3, string mark, bool colorate = false)
    {
        Text text1 = GetText(buttons[ind1]);
        Text text2 = GetText(buttons[ind2]);
        Text text3 = GetText(buttons[ind3]);

        bool equal = text1.text == mark 
                     && text2.text == mark 
                     && text3.text == mark;

        if (colorate && equal)
        {
            Color color = turn ? Color.green : Color.red;
            text1.color = color;
            text2.color = color;
            text3.color = color;
        }

        return equal;
    }
    
    // 선택 영역 활성/비활성화
    private void EnableButtons(bool enabled, bool ignoreEmpty = false)
    {
        foreach (Button button in buttons)
        {
            if (!enabled || ignoreEmpty || IsFieldEmpty(button))
            {
                button.interactable = enabled;
            }
        }
    }
    
    //선택 영역 사용 가능여부 확인
    private bool IsFieldEmpty(Button button)
    {
        return GetText(button).text == "";
    }
}

/*
 
 1. 게임 시작
 2. 게임 조건 초기화
 3. 선택 영역 텍스트 가져오기
 4. 선택 영역 이벤트 처리
 5. 선택 영역 표시 및 승리 조건 확인
 6. 승리 조건 확인
 7. 정렬 상태 확인
 8. 선택 영역 활성/비활성화 
 9. 선택 영역 사용 가능여부 확인
 10. 게임 재시작
 
*/


