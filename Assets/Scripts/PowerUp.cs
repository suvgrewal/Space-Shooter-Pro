using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType
{
    TripleShot = 0,
    Speed      = 1,
    Shield     = 2
}
 
public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;

    [SerializeField]
    private PowerUpType powerUpType;

    private float destroyDelay = 0.1f;
    private float yBound = 8f;

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -yBound)
        {
            Destroy(this.gameObject, destroyDelay);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.transform.GetComponent<Player>();

            if (player)
            {
                if (powerUpType == PowerUpType.TripleShot)
                {
                    player.TripleShotPowerUp();
                }
                else if (powerUpType == PowerUpType.Speed)
                {
                    player.SpeedPowerUp();
                }

                else if (powerUpType == PowerUpType.Shield)
                {
                    player.ShieldPowerUp();
                }

                switch (powerUpType)
                {
                    case PowerUpType.TripleShot:
                        player.TripleShotPowerUp();
                        break;
                    case PowerUpType.Speed:
                        player.SpeedPowerUp();
                        break;
                    case PowerUpType.Shield:
                        player.ShieldPowerUp();
                        break;
                    default:
                        break;
                }
            }

            Destroy(this.gameObject);
        }
    }
}
