using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int score = PlayerPrefs.GetInt("Score",0);
        ScoreText.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTryAgainPressed()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnQuitPressed()
    {
        Application.Quit();
    }
}
