using System.Collections;
using UnityEngine;

public class ShielderAnimationParams {
    public static string TriggerBreak = "triggerBreak";
    public static string TriggerRestore = "triggerRestore";
    public static string TriggerDefend = "triggerDefend";
}

public class Shield : MonoBehaviour, IHitByPlayer
{
    [SerializeField] Collider2D shieldCollider;
    [SerializeField] Animator animator;
    [SerializeField] Shielder shielder;
    [SerializeField] float defense;
    [SerializeField] float restoreDelay;
    bool ready = true;
    Coroutine coroutine;

    private void OnEnable() {
        shieldCollider = GetComponent<Collider2D>();
    }

    public void Hit(float _dmg) {
        if (_dmg > defense) {
            // if player hit the shield with a strong enough bullet, disable the shield and make shielder vulnerable
            animator.SetTrigger(ShielderAnimationParams.TriggerBreak);
            ready = false;
            shieldCollider.enabled = false;
            shielder.DefenseBreak();
            coroutine = StartCoroutine(CR_Restore());
        }
    }

    public void Defend() {
        if (!ready) return;
        if (coroutine != null) StopCoroutine(coroutine);
        animator.SetTrigger(ShielderAnimationParams.TriggerDefend);
    }

    IEnumerator CR_Restore() {
        // restore shield after a certain time
        yield return new WaitForSeconds(restoreDelay);
        animator.SetTrigger(ShielderAnimationParams.TriggerRestore);
        ready = true;
        shielder.Restore();
        shieldCollider.enabled = true;
    }

    private void OnDisable() {
        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = null;
        ready = true;
        shieldCollider.enabled = true;
    }
}
