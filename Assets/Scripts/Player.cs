using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private readonly float thrust = 4.0f;
    private readonly float rotation = 150.0f;

    public Rigidbody2D rb;
    public Projectile projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        rb.drag = 1.0f;
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
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.up * thrust);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, 0, -rotation * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 0, rotation * Time.deltaTime);
        }
    }

    void Shot()
    {
        Vector3 projectilePosition = this.transform.position;
        projectilePosition += this.transform.up * Projectile.yPositionUpwardsPlayer;
        GameObject.Instantiate(projectilePrefab, projectilePosition, this.transform.rotation);
    }
}