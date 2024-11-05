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
            return; // ���� ���� ���¿����� �� �̻� �������� ����

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
        Debug.Log("Game Over triggered"); // ����� �α� �߰�
        Time.timeScale = 0;
        IsGameOver = true;
        // ��� �˾� ǥ��
        AudioSource.PlayClipAtPoint(popUpSound, transform.position);
        popupCanvas.SetActive(true);

    }
}
