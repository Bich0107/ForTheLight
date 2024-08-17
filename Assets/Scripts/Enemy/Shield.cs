using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour, IHitByPlayer
{
    [SerializeField] LightAnimation lightAnimation;
    [SerializeField] Collider2D shieldCollider;
    [SerializeField] Shielder shielder;
    [SerializeField] AudioClip shieldHitSFX;
    [SerializeField] float defense;
    [SerializeField] float restoreDelay;
    bool ready = true;
    Coroutine coroutine;

    void OnEnable() {
        shieldCollider = GetComponent<Collider2D>();
    }

    public void Hit(float _dmg) {
        AudioManager.Instance.PlaySound(shieldHitSFX);

        if (_dmg > defense) {
            // if player hit the shield with a strong enough bullet, disable the shield and make shielder vulnerable
            ready = false;
            shieldCollider.enabled = false;
            shielder.DefenseBreak();
            lightAnimation.Rewind();
            coroutine = StartCoroutine(CR_Restore());
        }
    }

    public void Defend() {
        if (!ready) return;
        if (coroutine != null) StopCoroutine(coroutine);
        lightAnimation.Play();
    }

    IEnumerator CR_Restore() {
        // restore shield after a certain time
        yield return new WaitForSeconds(restoreDelay);
        ready = true;
        shielder.Restore();
        lightAnimation.Play();
        shieldCollider.enabled = true;
    }

    private void OnDisable() {
        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = null;
        ready = true;
        shieldCollider.enabled = true;
    }
}
