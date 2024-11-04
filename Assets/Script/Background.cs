using UnityEngine;

public class Background : MonoBehaviour
{
    public float Speed = 2;
    public float Width = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate( Vector2.left * Time.deltaTime * Speed );   // Vector2.left = new Vector(-1,0) 라고 생각해주세용

        if (transform.position.x < -Width)
        {
            transform.Translate(new Vector2(Width * 2, 0));
        }
    }
}
