using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    [SerializeField] Vector3 spawnPoint;
    [SerializeField] Animator animator;
    public Vector3 SpawnPoint => spawnPoint;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Tags.Player))
        {
            RespawnHandler handler = other.GetComponent<RespawnHandler>();
            if (handler != null)
            {
                handler.SetRespawnPoint(spawnPoint);
                animator?.SetTrigger("activate");
            }
        }
    }
}
