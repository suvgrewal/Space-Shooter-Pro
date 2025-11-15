using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _speed = 10f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;

    private float _canFire = -1f;

    private float _initialZ = 0;

    private float _xBound = 4f;
    private float _yBound = 4f;

    private Vector3 offset = new Vector3(0f, 0.8f, 0f);

    void Start()
    {
        // take current object position = new position (0, 0, 0)
        transform.position = new Vector3(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        // hit space key, spawn game object
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }  
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, _initialZ);

        transform.Translate(direction * _speed * Time.deltaTime);

        if (transform.position.x > _xBound)
        {
            transform.position = new Vector3(-_xBound, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -_xBound)
        {
            transform.position = new Vector3(_xBound, transform.position.y, transform.position.z);
        }

        if (transform.position.y > _yBound)
        {
            transform.position = new Vector3(transform.position.x, -_yBound, transform.position.z);
        }
        else if (transform.position.y < -_yBound)
        {
            transform.position = new Vector3(transform.position.x, _yBound, transform.position.z);
        }
    }
    void FireLaser()
    {
        _canFire = Time.time + _fireRate;

        Instantiate(_laserPrefab, transform.position + offset, Quaternion.identity);
    }
}
