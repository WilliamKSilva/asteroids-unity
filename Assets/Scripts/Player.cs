using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    // TODO: add UI for the game start and basic features
    public Rigidbody2D rb;
    private Projectile shotProjectilePrefab;
    private Projectile flareProjectilePrefab;
    private readonly float thrust = 4.0f;
    private readonly float rotation = 150.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb.drag = 1.0f;
        shotProjectilePrefab = Resources.Load("Prefabs/Projectiles/Projectile 1").GetComponent<Projectile>();
        flareProjectilePrefab = Resources.Load("Prefabs/Projectiles/Flare").GetComponent<Projectile>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Shot();
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            ShotFlare();
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
        Projectile projectile = GameObject.Instantiate(shotProjectilePrefab, projectilePosition, this.transform.rotation);
        projectile.name = "Projectile";
    }

    // Flares collide only with enemies projectiles
    void ShotFlare()
    {

        /*
            Vector3 projectilePosition = this.transform.position;
            projectilePosition += -this.transform.up * 1.0f;
            Projectile projectile = GameObject.Instantiate(flareProjectilePrefab, projectilePosition, this.transform.rotation);
            projectile.rb.rotation += 180;
            projectile.name = "Flare";
        */

        SpawnFlares();
    }

    void SpawnFlares()
    {
        float flareRotation = this.transform.rotation.z;
        Vector3 projectilePosition = this.transform.position;
        projectilePosition += -this.transform.up * 1.0f;


        /* One on the middle */ 
        Projectile projectile = GameObject.Instantiate(flareProjectilePrefab, projectilePosition, this.transform.rotation);
        projectile.rb.transform.Rotate(new Vector3(0, 0, 180), Space.Self);
        projectile.name = "Flare";
        
        /* Two to the right */
        for (int i = 0; i < 2; i++)
        {
            flareRotation += 20.0f;
            
            projectile = GameObject.Instantiate(flareProjectilePrefab, projectilePosition, this.transform.rotation);
            projectile.rb.transform.RotateAround(transform.position, Vector3.forward, flareRotation);
            projectile.rb.transform.Rotate(new Vector3(0, 0, 180), Space.Self);
            projectile.name = "Flare";
        }

        flareRotation = this.transform.rotation.z;

        /* Two to the left */
        for (int i = 0; i < 2; i++)
        {
            flareRotation -= 20.0f;
            
            projectile = GameObject.Instantiate(flareProjectilePrefab, projectilePosition, this.transform.rotation);
            projectile.rb.transform.RotateAround(transform.position, Vector3.forward, flareRotation);
            projectile.rb.transform.Rotate(new Vector3(0, 0, 180), Space.Self);
            projectile.name = "Flare";
        }
    }
}