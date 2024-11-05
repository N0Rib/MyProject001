using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float JumpSpeed = 10;
    public Collider2D BottomCollider;
    public CompositeCollider2D TerrainCollider;

    public AudioClip JumpSound;
    public AudioClip SlideSound;

    private Rigidbody2D rb;
    private bool grounded;
    private int jumpCount = 0;

    private bool isSliding = false;

    private BoxCollider2D boxCollider;
    private Vector2 originalSize;
    private Vector2 originalOffset;
    private Vector2 slideSize = new Vector2(2.0f, 0.7735f); 
    private Vector2 slideOffset = new Vector2(0.0f, -1f); 

    private Animator animator;


    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        originalSize = boxCollider.size;
        originalOffset = boxCollider.offset;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 착지 여부 확인
        bool wasGrounded = grounded;
        grounded = BottomCollider.IsTouching(TerrainCollider);

        // 착지 시 점프 횟수 초기화
        if (grounded && !wasGrounded)
        {
            jumpCount = 0;
        }

        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.F)) && jumpCount < 2) 
        {
            if (jumpCount == 0)
            {
                Jump(); // 첫 번째 점프
                GetComponent<Animator>().SetTrigger("Jump");
            }
            else if (jumpCount == 1) // 2단 점프 조건
            {
                DoubleJump();
                GetComponent<Animator>().SetTrigger("DoubleJump"); 
            }
        }
        else if (!grounded && rb.linearVelocity.y <= 0)
        {
            GetComponent<Animator>().SetTrigger("Fall"); 
        }

        if (Input.GetKey(KeyCode.J) && grounded)
        {
            boxCollider.size = slideSize;
            boxCollider.offset = slideOffset;

            if (!isSliding) // 슬라이드 시작 시에만 사운드 재생
            {
                if (SlideSound != null)
                    AudioSource.PlayClipAtPoint(SlideSound, transform.position);
                isSliding = true;
            }

            animator.SetBool("Slide", true);
        }
        else if (Input.GetKeyUp(KeyCode.J))
        {
            animator.SetBool("Slide", false);
            boxCollider.size = originalSize;
            boxCollider.offset = originalOffset;
            isSliding = false; // 슬라이드 종료 시 다시 초기화
        }

        if (Input.GetKey(KeyCode.K) && grounded)
        {
            boxCollider.size = slideSize;
            boxCollider.offset = slideOffset;

            if (!isSliding) // 슬라이드 시작 시에만 사운드 재생
            {
                if (SlideSound != null)
                    AudioSource.PlayClipAtPoint(SlideSound, transform.position);
                isSliding = true;
            }

            animator.SetBool("Slide", true);
        }
        else if (Input.GetKeyUp(KeyCode.K))
        {
            animator.SetBool("Slide", false);
            boxCollider.size = originalSize;
            boxCollider.offset = originalOffset;
            isSliding = false; // 슬라이드 종료 시 다시 초기화
        }
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpSpeed);
        jumpCount++;
        if (JumpSound != null)
            AudioSource.PlayClipAtPoint(JumpSound, transform.position);
    }

    private void DoubleJump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 1.2f*JumpSpeed); 
        jumpCount++; 
        if (JumpSound != null)
            AudioSource.PlayClipAtPoint(JumpSound, transform.position);
    }

    private void FixedUpdate()
    {

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Wall"))
        {
            Debug.Log("dead");
            GameManager.Instance.GameOver();
        }
        else if (collider.CompareTag("Score"))
        {
            // GameManager 인스턴스를 통해 점수 증가
            GameManager.Instance.IncreaseScore(100); // 1점 증가
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            Debug.Log("dead");
            GameManager.Instance.GameOver();
        }
        //else if (collision.collider.CompareTag("Score"))
        //{
        //    GameManager.Instance.IncreaseScore(100);
        //}
    }

}
