using TMPro;
using UnityEngine;

public class HighscorePopup : MonoBehaviour
{
    public TextMeshProUGUI ScoreLabel;

    private void OnEnable()
    {
        Debug.Log("HighscorePopup OnEnable called"); // ����� �α� �߰�
        string[] scores = PlayerPrefs.GetString("HighScores", "").Split(',');
        string result = "";

        for (int i = 0; i < scores.Length; i++)
        {
            if (float.TryParse(scores[i], out float score))
            {
                result += (i + 1) + ". " + Mathf.FloorToInt(score) + "\n"; // �Ҽ��� �����Ͽ� ������ ǥ��
            }
        }

        ScoreLabel.text = result;
    }

    public void ClosePressed()
    {
        gameObject.SetActive(false);
    }
}
