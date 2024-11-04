using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float JumpSpeed = 10;
    public Collider2D BottomCollider;
    public CompositeCollider2D TerrainCollider;

    public AudioClip UpSound;
    public AudioClip DownSound;

    private Rigidbody2D rb;
    private bool grounded;
    private int jumpCount = 0; // 점프 횟수를 추적하기 위한 변수

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

        if (Input.GetButtonDown("Jump") && jumpCount < 2) // 최대 점프 횟수 제한
        {
            if (jumpCount == 0)
            {
                Jump(); // 첫 번째 점프
                GetComponent<Animator>().SetTrigger("Jump");
            }
            else if (jumpCount == 1) // 2단 점프 조건
            {
                DoubleJump();
                GetComponent<Animator>().SetTrigger("DoubleJump"); // 2단 점프 시 애니메이션 트리거
            }
        }
        else if (!grounded && rb.linearVelocity.y <= 0)
        {
            GetComponent<Animator>().SetTrigger("Fall"); // 점프 후 낙하 애니메이션
        }
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpSpeed);
        jumpCount++;
        if (UpSound != null)
            AudioSource.PlayClipAtPoint(UpSound, transform.position);
    }

    private void DoubleJump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 1.2f*JumpSpeed); // 2단 점프 시 y 방향 속도 재설정
        jumpCount++; // 점프 횟수 증가
        if (UpSound != null)
            AudioSource.PlayClipAtPoint(UpSound, transform.position);
    }

    private void FixedUpdate()
    {
        grounded = BottomCollider.IsTouching(TerrainCollider);

        if (Input.GetKey(KeyCode.J) && grounded)
        {
            Debug.Log("slide");
            GetComponent<Animator>().SetTrigger("Slide");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 기타 충돌 처리 코드
    }
}
