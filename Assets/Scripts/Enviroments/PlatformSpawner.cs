using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [Space]
    [SerializeField] Vector3 lastSpawnPos;
    [SerializeField] float spawnCD;
    [SerializeField] float minDistanceX;
    [SerializeField] float minDistanceY;
    [SerializeField] float maxDistanceY;
    [SerializeField] bool isSpawning = true;
    [SerializeField] bool autoSpawn = true;
    Coroutine spawnCoroutine;
    WaitForSeconds waitTime;

    void Start()
    {
        waitTime = new WaitForSeconds(spawnCD);

        if (autoSpawn) spawnCoroutine = StartCoroutine(CR_Spawn());
    }

    public void Spawn() {
        isSpawning = true;
        spawnCoroutine = StartCoroutine(CR_Spawn());
    }

    public void Stop() {
        isSpawning = false;
        StopCoroutine(spawnCoroutine);
    }

    IEnumerator CR_Spawn()
    {
        do
        {
            Vector3 spawnPos = new Vector3();
            spawnPos.x = lastSpawnPos.x + Random.Range(-minDistanceX, minDistanceX);
            spawnPos.y = lastSpawnPos.y + Random.Range(minDistanceY, maxDistanceY);
            GameObject g = ObjectPool.Instance.Spawn(Tags.Platform);
            g.transform.position = spawnPos;
            g.SetActive(true);
           
            lastSpawnPos = spawnPos;
            yield return waitTime;
        } while (isSpawning);
    }
}
