using System.Collections;
using UnityEngine;

public class RespawnHandler : MonoBehaviour
{
    [SerializeField] HealthController healthController;
    [SerializeField] Player player;
    [SerializeField] Vector3 spawnOffset;
    [SerializeField] Vector3 respawnPoint;
    [SerializeField] float delay = 3f;

    void Awake()
    {
        healthController = GetComponent<HealthController>();
        player = GetComponent<Player>();
    }

    void OnEnable()
    {
        healthController.AddEventOnHealthReachZero(_ => Respawn());
    }

    void Respawn()
    {
        StartCoroutine(CR_Respawn());
    }

    IEnumerator CR_Respawn()
    {
        yield return new WaitForSeconds(delay);

        transform.parent = null;

        transform.position = respawnPoint + spawnOffset;
        player.Reset();

        // reset map here
        GameManager.Instance.PlayerRespawn();
    }

    public void SetRespawnPoint(Vector3 _pos)
    {
        respawnPoint = _pos;
    }
}
