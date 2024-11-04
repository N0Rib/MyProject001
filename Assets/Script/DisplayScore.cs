using TMPro;
using UnityEngine;

public class DisplayScore : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float score = GameManager.Instance.Score;

        GetComponent<TextMeshProUGUI>().text = ((int)score).ToString();
    }
}
