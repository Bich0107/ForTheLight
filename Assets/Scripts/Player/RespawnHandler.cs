using System.Collections;
using UnityEngine;

public class RespawnHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] SaveManager saveManager;
    [SerializeField] HealthController healthController;
    [SerializeField] TrailRenderer trail;
    [SerializeField] Player player;
    [Header("Spawn settings")]
    [SerializeField] Vector3 respawnPoint;
    [Header("VFXs")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject respawnVFX;
    [SerializeField] float delay = 2f;
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

        yield return new WaitForSeconds(delay);

        // change player position to spawn pos and clear the trail
        transform.position = respawnPoint;
        trail.Clear();

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
