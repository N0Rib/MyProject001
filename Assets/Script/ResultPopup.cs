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
        Debug.Log("ResultPopup OnEnable called"); // ����� �α� �߰�
        Time.timeScale = 0;

        // ���� ������ ���
        resultTitle.text = "Game Over...";
        float score = GameManager.Instance.Score; // ���� ���� ��������
        scoreLabel.text = ((int)score).ToString(); // ������ �ؽ�Ʈ�� ����
        SaveHighScore(score); // ������ �����ϴ� �޼��� ȣ��
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
        Debug.Log("Saved Scores: " + savedScoreString); // ����� ���� �α�

        if (savedScoreString == "")
        {
            PlayerPrefs.SetString("HighScores", currentScoreString);
        }
        else
        {
            string[] scoreArray = savedScoreString.Split(','); // ����� ���ھ �迭�� �и�
            List<string> scoreList = new List<string>(scoreArray);

            for (int i = 0; i < scoreList.Count; i++) // ������ ��ġ�� �� ���ھ� �ֱ�
            {
                float savedScore = float.Parse(scoreList[i]);
                if (savedScore < score) // ���ο� ������ �� ������
                {
                    scoreList.Insert(i, currentScoreString); // ����Ʈ�� �߰�
                    break;
                }
            }
            if (scoreArray.Length == scoreList.Count) // ������ ��ġ�� ��ã�Ҵٸ� �� �ڿ� �߰�
            {
                scoreList.Add(currentScoreString);
            }

            if (scoreList.Count > 10) // 10�� �Ѱ� ����ƴٸ� �� ���� ���ϴ�.
            {
                scoreList.RemoveAt(10);
            }

            string result = string.Join(",", scoreList); // ����Ʈ�� �ϳ��� ��Ʈ������ ��ġ��
            Debug.Log("Updated High Scores: " + result); // ������Ʈ�� ���� �α�
            PlayerPrefs.SetString("HighScores", result);
        }

        PlayerPrefs.Save();
    }

    public void TryAgainPressed()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene"); // ���� ���� ������ ���ư���
    }

    public void OnMainPressed()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainScene"); // ���� ȭ������ ���ư���
    }

    public void ShowHighscoresPressed()
    {
        highscorePopup.SetActive(true);
    }
}
