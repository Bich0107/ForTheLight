using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    [SerializeField] Vector3 spawnPoint;
    public Vector3 SpawnPoint => spawnPoint;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Tags.Player))
        {
            RespawnHandler handler = other.GetComponent<RespawnHandler>();
            if (handler != null)
            {
                handler.SetRespawnPoint(spawnPoint);
            }
        }
    }
}
