using System.Collections.Generic;
using UnityEngine;

public class GunHolder : MonoBehaviour
{
    [SerializeField] List<GameObject> gunList;
    [SerializeField] GameObject gun;
    Gun gunScript;
    bool isHold;

    private void Start()
    {
        gunScript = gun.GetComponent<Gun>();
    }

    public void HoldTrigger()
    {
        if (isHold) return;
        isHold = true;

        gunScript.HoldTrigger(transform.rotation);
    }

    public void ReleaseTrigger()
    {
        if (!isHold) return;
        isHold = false;
        
        gunScript.ReleaseTrigger(transform.rotation);
    }

    public void ChangeGun(GameObject _gun) => gun = _gun;

    public void Reset() {
        gunScript.Reset();
    }
}