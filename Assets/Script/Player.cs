using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float Gravity = 10f;
    public float Accel = 10f;
    float v = 0;

    public AudioClip UpSound;
    public AudioClip DownSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        v = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            GetComponent<AudioSource>().PlayOneShot(UpSound);
        }
        if (Input.GetButtonUp("Jump"))
        {
            GetComponent<AudioSource>().PlayOneShot(DownSound);
        }

        if (Input.GetButton("Jump"))
        {
            v -= Accel * Time.deltaTime;
        }
        else
        {
            v += Gravity * Time.deltaTime;
        }

    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.down * v * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") )
        {
            int score = (int)GameManager.Instance.Score;

            PlayerPrefs.SetInt("Score", score);

            SceneManager.LoadScene("GameOverScene");
        }
    }
}
