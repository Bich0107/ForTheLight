using System.Collections;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    protected Coroutine attackCoroutine;

    public abstract void Initialize();

    public abstract IEnumerator Start();
    public abstract void Reset();
}
