using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private readonly float thrust = 0.5f;
    private readonly float rotation = 150.0f;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.drag = 1.0f;
    }

    // Update is called once per frame
    void Update()
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
}