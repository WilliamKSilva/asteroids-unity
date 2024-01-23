using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb;
    private Player player = null;
    private Projectile projectilePrefab;
    public readonly float xSpeed = 2.0f;
    public readonly float ySpeed = 2.0f;
    public readonly float rightRotation = 90.0f;
    public readonly float leftRotation = -90.0f;
    public readonly float topRotation = 180.0f;
    public readonly float bottomRotation = 0.0f;
    private float timer;

    public Utils.Movement movement = new Utils.Movement();

    void Start()
    {
        projectilePrefab = Resources.Load("Prefabs/Projectiles/Projectile 2").GetComponent<Projectile>();
        GameObject playerGameObject = GameObject.Find("Player");
        if (playerGameObject)
        {
            player = playerGameObject.GetComponent<Player>();
        }
    }

    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;

        Move();
        LookAtPlayer();

        if (timer >= 2.5f)
        {
            Shot();
            timer = 0.0f;
        }
    }

    void Move()
    {
        if (movement.direction == Utils.Movement.Direction.RIGHT)
        {
            if (Utils.ObjectOutOfBounds(movement.direction, rb.position))
            {
                DestroyOutOfBounds();

                return;
            }

            Vector2 position = rb.position;
            position.x += xSpeed * Time.fixedDeltaTime;

            rb.MovePosition(position);

            return;
        }

        if (movement.direction == Utils.Movement.Direction.LEFT)
        {
            if (Utils.ObjectOutOfBounds(movement.direction, rb.position))
            {
                DestroyOutOfBounds();

                return;
            }

            Vector2 position = rb.position;
            position.x -= xSpeed * Time.fixedDeltaTime;

            rb.MovePosition(position);

            return;
        }

        if (movement.direction == Utils.Movement.Direction.UP)
        {
            if (Utils.ObjectOutOfBounds(movement.direction, rb.position))
            {
                DestroyOutOfBounds();

                return;
            }

            Vector2 position = rb.position;
            position.y += ySpeed * Time.fixedDeltaTime;

            rb.MovePosition(position);

            return;
        }

        if (movement.direction == Utils.Movement.Direction.DOWN)
        {
            if (Utils.ObjectOutOfBounds(movement.direction, rb.position))
            {
                DestroyOutOfBounds();

                return;
            }

            Vector2 position = rb.position;
            position.y -= ySpeed * Time.fixedDeltaTime;

            rb.MovePosition(position);

            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
    }

    public void RotateBasedOnMovement()
    {
        if (movement.direction == Utils.Movement.Direction.LEFT)
        {
            rb.MoveRotation(leftRotation);

            return;
        }

        if (movement.direction == Utils.Movement.Direction.RIGHT)
        {
            rb.MoveRotation(rightRotation);

            return;
        }

        if (movement.direction == Utils.Movement.Direction.UP)
        {
            rb.MoveRotation(topRotation);

            return;
        }

        if (movement.direction == Utils.Movement.Direction.DOWN)
        {
            rb.MoveRotation(bottomRotation);

            return;
        }
    }

    void DestroyOutOfBounds()
    {
        Destroy(rb.gameObject);
    }

    void LookAtPlayer()
    {
        if (player && player.isActiveAndEnabled)
        {
            rb.transform.up = rb.transform.position - player.transform.position;
        }
    }

    void Shot()
    {
        if (player && player.isActiveAndEnabled)
        {
            Vector3 projectilePosition = rb.transform.position;
            projectilePosition -= rb.transform.up;
            Projectile projectile = GameObject.Instantiate(projectilePrefab, projectilePosition, transform.rotation);
            projectile.rb.transform.up = player.rb.transform.position - rb.transform.position;
            projectile.name = "Projectile Enemy";
        }
    }
}
