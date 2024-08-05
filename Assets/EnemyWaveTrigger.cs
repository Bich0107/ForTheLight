using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveTrigger : MonoBehaviour
{
    [SerializeField] EnemySpawner spawner;
    [SerializeField] List<EnemyWaveSO> enemyWaves;
    [SerializeField] bool loop;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Tags.Player))
        {
            spawner.SetWaves(enemyWaves, loop);
            spawner.StartSpawning();
            Destroy(gameObject);
        }
    }
}
