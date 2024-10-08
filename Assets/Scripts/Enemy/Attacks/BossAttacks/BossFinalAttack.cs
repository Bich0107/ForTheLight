using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFinalAttack : Attack
{
    [Header("GO & components")]
    Transform trans;
    [SerializeField] GameObject body;
    [SerializeField] HealthController healthController;
    [SerializeField] MovementController moveController;
    [SerializeField] GameObject player;
    [Header("Settings")]
    [SerializeField] float jumpSpeed;
    [SerializeField] float jumpHeight = 30f;
    [SerializeField] Vector2 attackPos;
    [SerializeField] List<EnemyWaveSO> minionWaves;
    [SerializeField] GameObject chargeVFX;
    [SerializeField] GameObject beamVFX;
    [SerializeField] float chargeTime;
    [SerializeField] float healthRestorePercent = 35f;
    [Header("Sfxs")]
    [SerializeField] float chargeSFXInterval;
    [SerializeField] AudioClip beamFireSFX;
    [SerializeField] AudioClip chargeSFX;
    float maxHeight;
    float minHeight;
    EnemySpawner spawner;

    Coroutine chargeSFXCoroutine;

    public override void Initialize(GameObject _target)
    {
        player = _target;

        trans = body.transform;
        minHeight = trans.position.y;
        maxHeight = minHeight + jumpHeight;

        spawner = FindObjectOfType<EnemySpawner>();
    }

    public override IEnumerator StartAttack()
    {
        EnterFinalStage();
        yield return StartCoroutine(CR_FinalAttackSequence());
    }

    void EnterFinalStage()
    {
        // restore health
        healthController.IncreaseHealth(healthRestorePercent / 100 * healthController.GetMaxHealth);

        Flip(player.transform.position.x > transform.position.x);
    }

    void StartSpawningMinions()
    {
        spawner.SetWaves(minionWaves, true);
        spawner.StartSpawning();
    }

    IEnumerator CR_FinalAttackSequence()
    {
        moveController.MoveSpeed = jumpSpeed;
        moveController.Move(Vector2.up);

        // wait until reaching maximum height
        while (trans.position.y <= maxHeight) yield return null;
        moveController.Stop();

        trans.position = new Vector3(attackPos.x, trans.position.y);

        moveController.MoveSpeed = jumpSpeed;
        moveController.Move(Vector2.down);

        // wait until reaching minimum height
        while (trans.position.y >= minHeight) yield return null;
        moveController.Stop();

        chargeSFXCoroutine = StartCoroutine(CR_PlayChargeSFX());

        StartSpawningMinions();
        StartCoroutine(CR_ChargeBeamAttack());
    }

    IEnumerator CR_PlayChargeSFX()
    {
        do
        {
            AudioManager.Instance.PlaySound(chargeSFX);
            yield return new WaitForSeconds(chargeSFXInterval);
        } while (true);
    }

    IEnumerator CR_ChargeBeamAttack()
    {
        chargeVFX.SetActive(true);
        yield return new WaitForSeconds(chargeTime);
        StopCoroutine(chargeSFXCoroutine);
        Flip(player.transform.position.x < transform.position.x);

        AudioManager.Instance.PlaySound(beamFireSFX);
        chargeVFX.SetActive(false);
        beamVFX.SetActive(true);
        Debug.Log("attack end");
    }

    void Flip(bool _left)
    {
        transform.localScale = new Vector3(_left ? 1f : -1f, transform.localScale.y, transform.localScale.z);
    }

    public override void Reset()
    {
        StopAllCoroutines();
        spawner.Reset();
        chargeVFX.SetActive(false);
        beamVFX.SetActive(false);
        moveController.Stop();
    }
}
