using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject WallPrefab;
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

    private void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnTimer = 0;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        score += Time.deltaTime;

        if (spawnTimer > SpawnTerm)
        {
            spawnTimer -= SpawnTerm;

            GameObject obj = Instantiate(WallPrefab);
            obj.transform.position = new Vector2(10, Random.Range(-2f, 2f));
        }
    }
}
