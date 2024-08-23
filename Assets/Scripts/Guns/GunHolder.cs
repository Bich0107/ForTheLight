using System.Collections.Generic;
using UnityEngine;

public class GunHolder : MonoBehaviour
{
    [SerializeField] List<GameObject> gunList;
    [SerializeField] GameObject gun;
    Gun gunScript;

    private void Start()
    {
        gunScript = gun.GetComponent<Gun>();
    }

    public void HoldTrigger()
    {
        gunScript.HoldTrigger(transform.rotation);
    }

    public void ReleaseTrigger()
    {
        gunScript.ReleaseTrigger(transform.rotation);
    }

    public void ChangeGun(GameObject _gun) => gun = _gun;

    public void Reset() {
        gunScript.Reset();
    }
}