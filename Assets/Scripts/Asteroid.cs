using UnityEngine;
using UnityEngine.Events;

public class Asteroid : MonoBehaviour
{
    public UnityEvent<Asteroid> destroyedEvent;
    public Rigidbody2D rb;
    public AsteroidType type;
    public Movement movement = new Movement();
    private readonly float xSpeed = 1.0f;
    private readonly float ySpeed = 1.0f;
    private readonly float childAsteroidSpeed = 70.0f;
    private static readonly float diagonalPositionPossibility = 300;
    public bool childAsteroid = false;

    void FixedUpdate()
    {
        if (childAsteroid)
        {
            MoveChild();

            return;
        }

        // Regular movement
        Move();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Destroy(collision.gameObject);

            return;
        }

        if (collision.gameObject.name == "Projectile")
        {
            Destroy(rb.gameObject);
            Destroy(collision.gameObject);

            if (type == AsteroidType.BIG)
            {
                Projectile projectile = collision.GetComponent<Projectile>();
                AsteroidSpawner.BuildChildAsteroid(AsteroidType.MEDIUM, Movement.Direction.LEFT, gameObject.GetComponent<Asteroid>(), projectile);
                AsteroidSpawner.BuildChildAsteroid(AsteroidType.MEDIUM, Movement.Direction.RIGHT, gameObject.GetComponent<Asteroid>(), projectile);
            }
        }
    }

    void Move()
    {
        if (movement.direction == Movement.Direction.UP)
        {
            if (GetScreenPosition(rb.position).y >= Screen.height || GetScreenPosition(rb.position).x >= Screen.width)
            {
                DestroyOutOfBounds();

                return;
            }

            Vector2 position = rb.transform.position;

            position.y += ySpeed * Time.fixedDeltaTime;
            rb.MovePosition(position);
        }

        if (movement.direction == Movement.Direction.DOWN)
        {
            if (GetScreenPosition(rb.position).y <= 0 || GetScreenPosition(rb.position).x >= Screen.width)
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

    void MoveChild()
    {
        rb.velocity = Time.deltaTime * childAsteroidSpeed * transform.up;
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
        SMALL,
        MEDIUM,
        BIG,
    }

    public static Vector2 GetChildAsteroidPosition(Vector3 projectileDirection, Vector3 fatherAsteroidPosition)
    {
        Vector3 position = fatherAsteroidPosition;
        position += projectileDirection;

        return position;
    }

    public static void RotateChildAsteroid(Asteroid asteroid, float projectilRotation)
    {
        if (asteroid.movement.direction == Movement.Direction.RIGHT)
        {
            asteroid.rb.MoveRotation(-projectilRotation);
        }

        if (asteroid.movement.direction == Movement.Direction.LEFT)
        {
            asteroid.rb.MoveRotation(projectilRotation);
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

        public static bool GetDiagonal(Vector2 screenPosition)
        {
            bool diagonalPossible = false;
            if (screenPosition.x >= diagonalPositionPossibility && screenPosition.x <= Screen.width - diagonalPositionPossibility)
            {
                diagonalPossible = true;
            }

            if (screenPosition.y >= diagonalPositionPossibility && screenPosition.y <= Screen.height - diagonalPositionPossibility)
            {
                diagonalPossible = true;
            }

            if (diagonalPossible)
            {
                int random = Random.Range(0, 2);

                return random == 1;
            }

            return false;
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
