using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public AudioClip popUpSound;

    [SerializeField]
    private GameObject popupCanvas;

    public static GameManager Instance;

    public List<GameObject> WallPrefabs;
    public float SpawnTerm = 4;

    float spawnTimer;
    private float score;
    public float Score
    {
        get
        {
            return score;
        }
    }

    public bool IsGameOver { get; private set; } = false;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        spawnTimer = 0;
        score = 0;
    }

    void Update()
    {
        if (IsGameOver)
            return; // 게임 오버 상태에서는 더 이상 실행하지 않음

        spawnTimer += Time.deltaTime;
        score += Time.deltaTime;

        if (spawnTimer > SpawnTerm)
        {
            spawnTimer -= SpawnTerm;

            if (WallPrefabs.Count > 0)
            {
                GameObject wallPrefab = WallPrefabs[Random.Range(0, WallPrefabs.Count)];
                GameObject obj = Instantiate(wallPrefab);
                obj.transform.position = new Vector2(10, obj.transform.position.y);
            }
        }
    }

    public void IncreaseScore(float amount)
    {
        score += amount;
    }

    public void GameOver()
    {
        Debug.Log("Game Over triggered"); // 디버그 로그 추가
        Time.timeScale = 0;
        IsGameOver = true;
        // 결과 팝업 표시
        AudioSource.PlayClipAtPoint(popUpSound, transform.position);
        popupCanvas.SetActive(true);

    }
}
