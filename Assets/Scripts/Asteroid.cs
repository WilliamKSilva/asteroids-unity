using UnityEngine;
using UnityEngine.Events;

public class Asteroid : MonoBehaviour
{
    public UnityEvent<Asteroid> destroyedEvent;
    public Rigidbody2D rb;
    public AsteroidType type;
    public Utils.Movement movement = new Utils.Movement();
    private readonly float xSpeed = 1.0f;
    private readonly float ySpeed = 1.0f;
    private readonly float childAsteroidSpeed = 70.0f;
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
                AsteroidSpawner.BuildChildAsteroid(AsteroidType.MEDIUM, Utils.Movement.Direction.LEFT, gameObject.GetComponent<Asteroid>(), projectile);
                AsteroidSpawner.BuildChildAsteroid(AsteroidType.MEDIUM, Utils.Movement.Direction.RIGHT, gameObject.GetComponent<Asteroid>(), projectile);
            }
        }
    }

    void Move()
    {
        if (movement.direction == Utils.Movement.Direction.UP)
        {
            if (Utils.ObjectOutOfBounds(movement.direction, rb.position))
            {
                DestroyOutOfBounds();

                return;
            }

            Vector2 position = rb.transform.position;

            position.y += ySpeed * Time.fixedDeltaTime;
            rb.MovePosition(position);
        }

        if (movement.direction == Utils.Movement.Direction.DOWN)
        {
            if (Utils.ObjectOutOfBounds(movement.direction, rb.position))
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

        if (movement.direction == Utils.Movement.Direction.RIGHT)
        {
            if (Utils.ObjectOutOfBounds(movement.direction, rb.position))
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

        if (movement.direction == Utils.Movement.Direction.LEFT)
        {
            if (Utils.ObjectOutOfBounds(movement.direction, rb.position))
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
        if (asteroid.movement.direction == Utils.Movement.Direction.RIGHT)
        {
            asteroid.rb.MoveRotation(-projectilRotation);
        }

        if (asteroid.movement.direction == Utils.Movement.Direction.LEFT)
        {
            asteroid.rb.MoveRotation(projectilRotation);
        }
    }
}
