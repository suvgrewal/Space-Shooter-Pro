using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;

    void Update()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            Enemy enemy = Instantiate(new Enemy(), transform.position, Quaternion.identity)

            yield return new WaitForSeconds(5.0f);
        }
    }
}
