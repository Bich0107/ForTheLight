using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveTrigger : MonoBehaviour
{
    [SerializeField] EnemySpawner spawner;
    [SerializeField] List<EnemyWaveSO> enemyWaves;
    [SerializeField] bool loop;
    [SerializeField] int areaIndex;
    public int AreaIndex => areaIndex;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Tags.Player))
        {
            spawner.SetWaves(enemyWaves, loop);
            spawner.StartSpawning();
            gameObject.SetActive(false);
        }
    }
}
