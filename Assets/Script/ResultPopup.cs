using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ResultPopup : MonoBehaviour
{
    [SerializeField]
    private TMP_Text resultTitle;
    [SerializeField]
    private TMP_Text scoreLabel;
    [SerializeField]
    GameObject highScoreObject;
    [SerializeField]
    GameObject highscorePopup;

    private void OnEnable()
    {
        Debug.Log("ResultPopup OnEnable called"); // 디버그 로그 추가
        Time.timeScale = 0;

        // 게임 오버일 경우
        resultTitle.text = "Game Over...";
        float score = GameManager.Instance.Score; // 현재 점수 가져오기
        scoreLabel.text = ((int)score).ToString(); // 점수를 텍스트로 설정
        SaveHighScore(score); // 점수를 저장하는 메서드 호출
    }

    void SaveHighScore(float score)
    {
        float highscore = PlayerPrefs.GetFloat("highscore", 0);

        if (score > highscore)
        {
            highScoreObject.SetActive(true);
            PlayerPrefs.SetFloat("highscore", score);
        }
        else
        {
            highScoreObject.SetActive(false);
        }

        string currentScoreString = score.ToString("#.###");
        string savedScoreString = PlayerPrefs.GetString("HighScores", "");
        Debug.Log("Saved Scores: " + savedScoreString); // 저장된 점수 로그

        if (savedScoreString == "")
        {
            PlayerPrefs.SetString("HighScores", currentScoreString);
        }
        else
        {
            string[] scoreArray = savedScoreString.Split(','); // 저장된 스코어를 배열로 분리
            List<string> scoreList = new List<string>(scoreArray);

            for (int i = 0; i < scoreList.Count; i++) // 적절한 위치에 새 스코어 넣기
            {
                float savedScore = float.Parse(scoreList[i]);
                if (savedScore < score) // 새로운 점수가 더 높으면
                {
                    scoreList.Insert(i, currentScoreString); // 리스트에 추가
                    break;
                }
            }
            if (scoreArray.Length == scoreList.Count) // 적절한 위치를 못찾았다면 맨 뒤에 추가
            {
                scoreList.Add(currentScoreString);
            }

            if (scoreList.Count > 10) // 10개 넘게 저장됐다면 맨 끝은 뺍니다.
            {
                scoreList.RemoveAt(10);
            }

            string result = string.Join(",", scoreList); // 리스트를 하나의 스트링으로 합치기
            Debug.Log("Updated High Scores: " + result); // 업데이트된 점수 로그
            PlayerPrefs.SetString("HighScores", result);
        }

        PlayerPrefs.Save();
    }

    public void TryAgainPressed()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene"); // 현재 게임 씬으로 돌아가기
    }

    public void OnMainPressed()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainScene"); // 메인 화면으로 돌아가기
    }

    public void ShowHighscoresPressed()
    {
        highscorePopup.SetActive(true);
    }
}
