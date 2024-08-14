using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(HealthController), typeof(MovementController))]
public class Enemy : MonoBehaviour, IHitByPlayer, IProtectedByShielder
{
    [SerializeField] protected GameObject player;
    protected MovementController moveController;
    protected HealthController healthController;
    [Header("Death effect")]
    [SerializeField] protected GameObject deathVFX;
    [SerializeField] protected float duration;
    bool isDead = false;
    public bool IsDead => isDead;

    protected void OnEnable()
    {
        isDead = false;

        deathVFX?.SetActive(false);

        player = FindObjectOfType<PlayerInput>().gameObject;
        moveController = GetComponent<MovementController>();
        healthController = GetComponent<HealthController>();

        healthController?.AddEventOnHealthReachZero((object _obj) => Die());
    }

    public void StopAllAction() {
        moveController.Stop();
        StopAllCoroutines();
    }

    public void Hit(float _dmg)
    {
        healthController?.DecreaseHealth(_dmg);
    }

    protected virtual void Die()
    {
        if (isDead) return;

        moveController.Stop();
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
    protected Vector2 GetDirectionToPlayer => (player.transform.position - transform.position).normalized;
    protected float GetDistanceToPlayer => Vector2.Distance(transform.position, player.transform.position);
    protected float GetHorizontalDistanceToPlayer => Mathf.Abs(transform.position.x - player.transform.position.x);
    protected float GetVerticalDistanceToPlayer => Mathf.Abs(transform.position.y - player.transform.position.y);
}
