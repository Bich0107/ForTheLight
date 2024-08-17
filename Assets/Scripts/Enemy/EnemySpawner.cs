using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    [SerializeField] List<EnemyWaveSO> waves;
    [SerializeField] bool loop = false;
    [SerializeField] int index;
    [SerializeField] EnemyWaveSO currentWave;
    Coroutine spawnCoroutine;

    void Start()
    {
        Reset();
    }

    public void SetWaves(List<EnemyWaveSO> _waves, bool _isLoop = false)
    {
        // stop current waves
        Stop();

        // set new wave
        waves = _waves;

        index = 0;
        currentWave = waves[index];

        loop = _isLoop;
    }

    public void StartSpawning()
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
        waves = null;
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
        } while (currentWave != null || loop);
    }

    EnemyWaveSO GetNextWave(ref int _index)
    {
        if (_index + 1 < waves.Count || loop)
        {
            index = (index + 1) % waves.Count;
            return waves[index];
        }
        else return null;
    }
}