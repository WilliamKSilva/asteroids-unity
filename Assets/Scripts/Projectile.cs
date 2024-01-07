using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D rb;
    private readonly float speed = 400.0f;
    public static float yPositionUpwardsPlayer = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("Player");
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (OutOfBounds())
        {
            Destroy(rb.gameObject);

            return;
        }

        Move();
    }

    void Move()
    {
        // Position
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