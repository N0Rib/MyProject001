using TMPro;
using UnityEngine;

public class HighscorePopup : MonoBehaviour
{
    public TextMeshProUGUI ScoreLabel;

    private void OnEnable()
    {
        Debug.Log("HighscorePopup OnEnable called"); // 디버그 로그 추가
        string[] scores = PlayerPrefs.GetString("HighScores", "").Split(',');
        string result = "";

        for (int i = 0; i < scores.Length; i++)
        {
            if (float.TryParse(scores[i], out float score))
            {
                result += (i + 1) + ". " + Mathf.FloorToInt(score) + "\n"; // 소수점 제거하여 정수로 표시
            }
        }

        ScoreLabel.text = result;
    }

    public void ClosePressed()
    {
        gameObject.SetActive(false);
    }
}
