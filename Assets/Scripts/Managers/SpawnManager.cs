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
    private GameObject[] _powerUps;

    [SerializeField]
    private float _xBound = 8f;
    [SerializeField]
    private float _ySpawnLoc = 7f;

    [SerializeField]
    private bool _keepSpawning = true;

    [SerializeField]
    private float _tripleShotSpawnMinTime = 3f;
    [SerializeField]
    private float _tripleShotSpawnMaxTime = 7f;

    [SerializeField]
    private float _speedSpawnTimeMin = 5f;
    [SerializeField]
    private float _speedSpawnTimeMax = 10f;

    [SerializeField]
    private float _smoothSpawnStartDelay = 3f;

    [SerializeField]
    private float _enemySpawnDelay = 5.0f;

    void Start()
    {
        if (!_enemyContainer)
        {
            _enemyContainer = new GameObject("EnemyContainer");
            _enemyContainer.transform.parent = transform;
        }
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnRoutine());
    }

    public void OnPlayerDeath()
    {
        _keepSpawning = false;
    }

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(_smoothSpawnStartDelay);

        StartCoroutine(SpawnEnemyRoutine());

        StartCoroutine(SpawnPowerUpRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (_keepSpawning)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-_xBound, _xBound), _ySpawnLoc, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(_enemySpawnDelay);
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        while (_keepSpawning)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-_xBound, _xBound), _ySpawnLoc, 0);
            Instantiate(_powerUps[Random.Range(0, _powerUps.Length)], posToSpawn, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(_tripleShotSpawnMinTime, _tripleShotSpawnMaxTime));

        }
    }
}
