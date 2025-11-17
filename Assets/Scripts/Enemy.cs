using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _lowerXBound = -8f;
    private float _upperXBound = 8f;
    private float _upperYBound = 7f;
    private float _lowerYBound = -5f;

    [SerializeField]
    private float _speed = 4.0f;

    void Update()
    {
        MoveDown();

        if (transform.position.y < _lowerYBound)
        {
            RespawnAtTop();
        }
        // Random.range(_lowerXBound, _upperXBound);
    }

    void MoveDown()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }

    void RespawnAtTop()
    {
        transform.position = new Vector3(Random.Range(_lowerXBound, _upperXBound), _upperYBound, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit: " + other.transform.name);

        if (other.transform.name == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player)
            {
                player.Damage();
            }

            Destroy(this.gameObject);
        }

        if (other.transform.name == "Laser(Clone)")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
