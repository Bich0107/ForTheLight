using System.Collections;
using UnityEngine;

public class RespawnHandler : MonoBehaviour
{
    [SerializeField] SaveManager saveManager;
    [SerializeField] HealthController healthController;
    [SerializeField] Player player;
    [SerializeField] Vector3 spawnOffset;
    [SerializeField] Vector3 respawnPoint;
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject respawnVFX;
    [SerializeField] float delay = 3f;
    [Header("Return to spawn on hit animation settings")]
    [SerializeField] float normalSpeedDuration;
    [SerializeField] float normalSpeed;
    [SerializeField] float fastSpeedDuration;
    [SerializeField] float fastSpeed;
    bool isRespawning = false;

    void Awake()
    {
        saveManager = FindObjectOfType<SaveManager>();
        healthController = GetComponent<HealthController>();
        player = GetComponent<Player>();
    }

    void OnEnable()
    {
        SetOnHealthReachZeroEvent();
    }

    void SetOnHealthReachZeroEvent()
    {
        if (saveManager == null) return;

        healthController.ResetOnHealthReachZeroEvent();
        healthController.AddEventOnHealthReachZero(_ =>
            {
                if (saveManager.CurrentSaveFile.CurrentLife > 1)
                {
                    Respawn();
                }
                else
                {
                    deathVFX?.SetActive(true);
                    healthController.ResetOnHealthReachZeroEvent();
                    StartCoroutine(CR_GameOver());
                }
            });
    }

    IEnumerator CR_GameOver()
    {
        // disable player control and collision
        GameManager.Instance.SetPlayerControlStatus(false);
        player.ToggleCollision();

        // wait for the animation to finish
        yield return new WaitForSeconds(delay);

        GameManager.Instance.SetPlayerControlStatus(true);
        GameManager.Instance.GameOver();
    }

    void Respawn()
    {
        if (isRespawning) return;
        isRespawning = true;

        // play effect
        respawnVFX?.SetActive(true);

        // turn off player collision
        player.ToggleCollision();

        // respawn while deathVFX is playing
        // deathVFX?.SetActive(true);
        StartCoroutine(CR_Respawn());
    }

    IEnumerator CR_Respawn()
    {
        // disable player control and remove platform parent (if any)
        GameManager.Instance.SetPlayerControlStatus(false);
        transform.parent = null;

        // return to respawn point in delay time
        Vector3 targetPos = respawnPoint + spawnOffset;
        Vector3 startPos = transform.position;

        // return with normal speed
        float tick = 0;
        while (tick <= normalSpeedDuration)
        {
            tick += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, targetPos, tick * normalSpeed);
            yield return null;
        }

        // return with fast speed
        startPos = transform.position;
        tick = 0f;
        while (tick <= fastSpeedDuration)
        {
            tick += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, targetPos, tick * fastSpeed);
            yield return null;
        }

        // ensure player is at the spawn position
        transform.position = targetPos;

        // update lives ui
        FindObjectOfType<LifeDisplayer>().DecreaseLife(1);

        // reset player status and enbale player control
        player.Reset();
        GameManager.Instance.SetPlayerControlStatus(true);

        // setup game state accordingly
        GameManager.Instance.PlayerRespawn();

        // turn off flag
        isRespawning = false;

        // turn off effect
        respawnVFX?.SetActive(false);

        // turn on collision and update on hit event
        player.ToggleCollision();
        SetOnHealthReachZeroEvent();
    }

    public void SetRespawnPoint(Vector3 _pos)
    {
        respawnPoint = _pos;
    }
}
