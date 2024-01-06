using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D rb;
    float speed = 2f;
    public static float yPositionUpwardsPlayer = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Instantiated");
        GameObject player = GameObject.Find("Player");
        rb.velocity = player.transform.up;
    }

    // Update is called once per frame
    void Update()
    {
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
        rb.velocity *= speed;
    }

    bool OutOfBounds()
    {
        bool xPositiveAxis = rb.position.x >= Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;
        bool xNegativeAxis = rb.position.x <= Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x;

        bool yPositiveAxis = rb.position.y >= Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y;
        bool yNegativeAxis = rb.position.y <= Camera.main.ScreenToWorldPoint(new Vector2(Screen.height, 0)).y;

        return xPositiveAxis || xNegativeAxis || yPositiveAxis || yNegativeAxis;
    }
}
