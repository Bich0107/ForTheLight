using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(HealthController), typeof(MovementController))]
public class Enemy : MonoBehaviour, IHitByPlayer
{
    [Header("General")]
    [SerializeField] protected GameObject player;
    protected MovementController moveController;
    protected HealthController healthController;
    [Header("Death effect")]
    [SerializeField] protected GameObject deathVFX;
    [SerializeField] protected float duration;
    bool isDead = false;
    public bool IsDead => isDead;
    [Header("Sfxs")]
    [SerializeField] AudioClip deathSFX;

    protected virtual void OnEnable()
    {
        isDead = false;

        deathVFX?.SetActive(false);

        player = FindObjectOfType<PlayerInput>().gameObject;
        moveController = GetComponent<MovementController>();
        healthController = GetComponent<HealthController>();

        healthController?.AddEventOnHealthReachZero((object _obj) => Die());
    }

    public void StopAllAction()
    {
        moveController.Stop();
        StopAllCoroutines();
    }

    public void Hit(float _dmg)
    {
        healthController?.DecreaseHealth(_dmg);
    }

    public void PlayDeathSFX()
    {
        if (deathSFX == null)
        {
            Debug.Log("deathsfx null", gameObject);
            return;
        }
        AudioManager.Instance.PlaySound(deathSFX);
    }

    protected virtual void Die()
    {
        if (isDead) return;

        moveController.Stop();
        PlayDeathSFX();
        StopAllCoroutines();
        StartCoroutine(CR_DeathAnimation());
    }

    protected IEnumerator CR_DeathAnimation()
    {
        isDead = true;
        deathVFX?.SetActive(true);

        float tick = 0f;
        Vector3 baseScale = transform.localScale;
        while (tick <= duration)
        {
            tick += Time.deltaTime;
            transform.localScale = Vector3.Lerp(baseScale, Vector3.zero, tick / duration);
            yield return null;
        }

        deathVFX?.SetActive(false);
        transform.localScale = baseScale;
        gameObject.SetActive(false);
    }

    protected void OnDisable()
    {
        Reset();
    }

    public virtual void Reset()
    {
        StopAllCoroutines();
        moveController?.Reset();
        healthController?.Reset();
        isDead = false;
    }

    public GameObject GetPlayer => player;
    public Vector2 GetDirectionToPlayer => (player.transform.position - transform.position).normalized;
    public float GetDistanceToPlayer => Vector2.Distance(transform.position, player.transform.position);
    public float GetHorizontalDistanceToPlayer => Mathf.Abs(transform.position.x - player.transform.position.x);
    public float GetVerticalDistanceToPlayer => Mathf.Abs(transform.position.y - player.transform.position.y);
}
