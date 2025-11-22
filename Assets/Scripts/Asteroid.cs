using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 20.0f;

    [SerializeField]
    private float _speed = 0.0f;

    private float _xBound = 10f;

    private float _yBound = 8f;

    [SerializeField]
    private GameObject _explosionPrefab;

    [SerializeField]
    SpawnManager _spawnManager;

    [SerializeField]
    private float _destructionDelay = 0.25f;

    private void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime, Space.World);

        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            GameObject Explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

            Destroy(other.gameObject);

            if (_spawnManager)
            {
                Debug.Log("Asteroid: Calling StartSpawning on SpawnManager");

                _spawnManager.StartSpawning();
            }

            Destroy(this.gameObject, _destructionDelay);
        }
    }
}
