using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float speed = 2f;
    private Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        move();
    }

    public void move()
    {
        moveHorizontally();
        /*
       if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position = transform.right * speed;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.position = transform.right * -speed;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.position = transform.up * speed;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            transform.position = transform.up * -speed;
        }*/
    }
    void moveHorizontally()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float moveBy = x * speed;
        rb2d.velocity = new Vector2(moveBy, rb2d.velocity.y);
    }
}
