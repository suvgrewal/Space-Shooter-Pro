using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8f;

    
    private float yBound = 8f;
    private float destroyDelay = 0.1f;

    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        CheckBounds();
    }

    void CheckBounds()
    {
        if (transform.position.y > yBound || transform.position.y < -yBound)
        {
            Destroy(this.gameObject, destroyDelay);
        }
    }
}
