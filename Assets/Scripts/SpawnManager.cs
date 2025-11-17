using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _powerUpPrefab;

    [SerializeField]
    private float _xBound = 8f;
    [SerializeField]
    private float _ySpawnLoc = 7f;

    [SerializeField]
    private bool _keepSpawning = true;

    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
    }

    public void OnPlayerDeath()
    {
        _keepSpawning = false;
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (_keepSpawning)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-_xBound, _xBound), _ySpawnLoc, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        while (_keepSpawning)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-_xBound, _xBound), _ySpawnLoc, 0);
            // instantiate powerup object
        }

        yield return new WaitForSeconds(Random.Range(3.0f, 7.0f));
    }
}
