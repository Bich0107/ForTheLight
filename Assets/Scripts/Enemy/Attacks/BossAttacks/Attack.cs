using System.Collections;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    protected Coroutine attackCoroutine;

    public abstract void Initialize(GameObject target);

    public abstract IEnumerator Start();
    public abstract void Reset();
    public virtual void Stop()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }

        attackCoroutine = null;
    }
}
