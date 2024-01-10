using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    private Projectile projectilePrefab;

    private readonly float thrust = 4.0f;
    private readonly float rotation = 150.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb.drag = 1.0f;
        projectilePrefab = Resources.Load("Prefabs/Projectiles/Projectile 1").GetComponent<Projectile>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Shot();
        }
    }

    // Physics related stuff goes here apparently
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.up * thrust);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.MoveRotation(rb.rotation + -rotation * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.MoveRotation(rb.rotation + rotation * Time.deltaTime);
        }
    }

    void Shot()
    {
        Vector3 projectilePosition = this.transform.position;
        projectilePosition += this.transform.up * Projectile.yPositionUpwardsPlayer;
        Projectile projectile = GameObject.Instantiate(projectilePrefab, projectilePosition, this.transform.rotation);
        projectile.name = "Projectile";
    }
}