using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;

    private float destroyDelay = 0.1f;
    private float yBound = 8f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -yBound)
        {
            Destroy(this.gameObject, destroyDelay);
        }
    }

    // check OnTriggerCollision
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.transform.GetComponent<Player>();

            if (player)
            {
                player.TripleShotPowerUp();
            }

            Destroy(this.gameObject);
        }
    }
}
