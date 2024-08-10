using System.Collections;
using UnityEngine;

public class RespawnHandler : MonoBehaviour
{
    [SerializeField] HealthController healthController;
    [SerializeField] Player player;
    [SerializeField] Vector3 spawnOffset;
    [SerializeField] Vector3 respawnPoint;
    [SerializeField] GameObject deathVFX;
    [SerializeField] float delay = 3f;
    bool isRespawning = false;

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
        if (isRespawning) return;
        isRespawning = true;
        deathVFX?.SetActive(true);
        StartCoroutine(CR_Respawn());
    }

    IEnumerator CR_Respawn()
    {
        // disable player control;
        GameManager.Instance.SetPlayerControlStatus(false);

        transform.parent = null;
        
        // slowly return to respawn point in delay time
        float tick = 0;
        Vector3 targetPos = respawnPoint + spawnOffset;
        Vector3 startPos = transform.position;
        while (tick <= delay) {
            tick += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, targetPos, tick / delay);
            yield return null;
        }

        player.Reset();

        GameManager.Instance.PlayerRespawn();
        GameManager.Instance.SetPlayerControlStatus(true);

        isRespawning = false;

        // wait until all particles die
        yield return new WaitForSeconds(delay);
        deathVFX?.SetActive(false);
    }

    void MoveToRespawnPos(float _tick) {

        transform.position =  respawnPoint + spawnOffset;
    }

    public void SetRespawnPoint(Vector3 _pos)
    {
        respawnPoint = _pos;
    }
}
