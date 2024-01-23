using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D rb;
    private readonly float speed = 400.0f;
    public static float yPositionUpwardsPlayer = 1.0f;
    public static float yPositionBackwardsPlayer = -1.0f;

    void FixedUpdate()
    {
        if (OutOfBounds())
        {
            Destroy(rb.gameObject);

            return;
        }

        Move();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.SetActive(false);
            Destroy(rb.gameObject);
        }

        if (collision.gameObject.name == "Projectile")
        {
            Destroy(collision.gameObject);
            Destroy(rb.gameObject);
        }

        if (collision.gameObject.name == "Flare")
        {
            Destroy(collision.gameObject);
            Destroy(rb.gameObject);
        }
    }

    void Move()
    {
        /* Position */
        rb.velocity = speed * Time.fixedDeltaTime * rb.transform.up;
    }

    bool OutOfBounds()
    {
        return
            GetScreenPosition(rb.position).x >= 1920
            || GetScreenPosition(rb.position).x <= -5
            || GetScreenPosition(rb.position).y >= 1080
            || GetScreenPosition(rb.position).y <= -5;
    }

    Vector2 GetScreenPosition(Vector2 position)
    {
        return Camera.main.WorldToScreenPoint(position);
    }
}
