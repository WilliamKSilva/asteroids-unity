using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Asteroid : MonoBehaviour
{
    public UnityEvent<Asteroid> destroyedEvent;
    public Rigidbody2D rb;
    public AsteroidType type;
    public Movement movement = new Movement();
    private float xSpeed = 0.5f;
    private float ySpeed = 0.5f;

    void Start()
    {
    }

    private void FixedUpdate()
    {
        if (movement.direction == Movement.Direction.UP)
        {
            if (GetScreenPosition(rb.position).y >= Screen.height || GetScreenPosition(rb.position).x >= Screen.width)
            {
                DestroyOutOfBounds();

                return;
            }

            Vector2 position = rb.transform.position;

            if (movement.diagonal)
            {
                position.x += xSpeed * Time.fixedDeltaTime;
            }

            position.y += ySpeed * Time.fixedDeltaTime;
            rb.MovePosition(position);
        }

        if (movement.direction == Movement.Direction.DOWN)
        {
            if (GetScreenPosition(rb.position).y <= 0 || GetScreenPosition(rb.position).x >= 0)
            {
                DestroyOutOfBounds();

                return;
            }

            Vector2 position = rb.transform.position;

            if (movement.diagonal)
            {
                position.x -= xSpeed * Time.fixedDeltaTime;
            }

            position.y -= ySpeed * Time.fixedDeltaTime;

            rb.MovePosition(position);
        }

        if (movement.direction == Movement.Direction.RIGHT)
        {
            if (GetScreenPosition(rb.position).x >= Screen.width || GetScreenPosition(rb.position).y >= Screen.height)
            {
                DestroyOutOfBounds();

                return;
            }

            Vector2 position = rb.transform.position;

            if (movement.diagonal)
            {
                position.y += ySpeed * Time.fixedDeltaTime;
            }

            position.x += xSpeed * Time.fixedDeltaTime;
            rb.MovePosition(position);
        }

        if (movement.direction == Movement.Direction.LEFT)
        {
            if (GetScreenPosition(rb.position).x <= 0 || GetScreenPosition(rb.position).y >= Screen.height)
            {
                DestroyOutOfBounds();

                return;
            }

            Vector2 position = rb.transform.position;

            if (movement.diagonal)
            {
                position.y += ySpeed * Time.fixedDeltaTime;
            }

            position.x -= xSpeed * Time.fixedDeltaTime;
            rb.MovePosition(position);
        }
    }

    void DestroyOutOfBounds()
    {
        destroyedEvent.Invoke(this);
    }

    Vector2 GetScreenPosition(Vector2 position)
    {
        return Camera.main.WorldToScreenPoint(position);
    }

    public enum AsteroidType
    {
        BIG,
        MEDIUM
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Projectile Collision");
        if (collision.gameObject.name == "Projectile")
        {
            Destroy(rb.gameObject);
            Destroy(collision.gameObject);
        }
    }

    public class Movement
    {
        public Direction direction;
        public bool diagonal;

        public static Direction GetDirection(AsteroidSpawner.PositionNames position)
        {
            if (position == AsteroidSpawner.PositionNames.LEFT_WITH_RANDOM_Y)
            {
                return Direction.RIGHT;
            }

            if (position == AsteroidSpawner.PositionNames.RIGHT_WITH_RANDOM_Y)
            {
                return Direction.LEFT;
            }

            if (position == AsteroidSpawner.PositionNames.TOP_WITH_RANDOM_X)
            {
                return Direction.DOWN;
            }

            // Bottom with random X
            return Direction.UP;
        }

        public static bool GetRandomDiagonal()
        {
            int random = UnityEngine.Random.Range(0, 2);

            return random == 1;
        }

        public enum Direction
        {
            UP,
            DOWN,
            RIGHT,
            LEFT,
        }
    }
}
