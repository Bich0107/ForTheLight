using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Vector2 basePos;
    [SerializeField] List<EnemyWaveSO> waves;
    [SerializeField] Transform enemyParent;

    int index;
    EnemyWaveSO currentWave;
    Coroutine spawnCoroutine;

    private void Start()
    {
        basePos = transform.position;
        Reset();
    }

    public void SpawnAll()
    {
        spawnCoroutine = StartCoroutine(CR_Spawn());
    }

    public void Stop()
    {
        if (spawnCoroutine != null) StopCoroutine(spawnCoroutine);
        spawnCoroutine = null;
    }

    public void Reset()
    {
        Stop();
        if (waves != null)
        {
            index = 0;
            currentWave = waves[0];
            transform.position = basePos;
        }
    }

    IEnumerator CR_Spawn()
    {
        if (waves == null) yield break;

        do
        {
            List<WaveSectionSO> sectionList = currentWave.GetSectionList;

            // spawn each section of current wave
            for (int i = 0; i < sectionList.Count; i++)
            {
                WaveSectionSO currentSection = sectionList[i];
                //GameObject currentEnemy = currentSection.GetEnemy;
                string currentEnemyTag = currentSection.GetEnemy.tag;

                // spawn enemy base on currentSection's enemy and amount
                for (int j = 0; j < currentSection.GetAmount; j++)
                {
                    Vector3 spawnPos = transform.position + (Vector3)currentSection.GetOffsetList[j];

                    // should consider using object pool or not
                    GameObject g = ObjectPool.Instance.Spawn(currentEnemyTag);
                    if (g == null)
                    {
                        Debug.LogWarning("null game object tag: " + currentEnemyTag);
                        yield break;
                    }
                    g.transform.position = spawnPos;
                    g.SetActive(true);

                    yield return new WaitForSeconds(currentSection.GetSpawnDelay);
                }

                yield return new WaitForSeconds(currentSection.GetEndSectionDelay);
            }

            // move to next wave
            currentWave = GetNextWave(ref index);
            if (currentWave != null)
            {
                yield return new WaitForSeconds(currentWave.GetDelay);
            }
            else yield break; // all waves clear
        } while (true);
    }

    EnemyWaveSO GetNextWave(ref int _index)
    {
        if (_index + 1 < waves.Count)
        {
            index++;
            return waves[index];
        }
        else return null;
    }
}