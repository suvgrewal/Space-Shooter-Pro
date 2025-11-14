using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _speed = 10f;

    private float initialZ = 0;

    private float x_bound = 4f;
    private float y_bound = 4f;

    void Start()
    {
        // take current object position = new position (0, 0, 0)
        transform.position = new Vector3(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, initialZ);

        transform.Translate(direction * _speed * Time.deltaTime);

        if (transform.position.x > x_bound)
        {
            transform.position = new Vector3(-x_bound, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -x_bound)
        {
            transform.position = new Vector3(x_bound, transform.position.y, transform.position.z);
        }

        if (transform.position.y > y_bound)
        {
            transform.position = new Vector3(transform.position.x, -y_bound, transform.position.z);
        }
        else if (transform.position.y < -y_bound)
        {
            transform.position = new Vector3(transform.position.x, y_bound, transform.position.z);
        }
    }
}
