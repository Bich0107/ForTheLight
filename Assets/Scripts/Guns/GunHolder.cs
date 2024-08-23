using System.Collections.Generic;
using UnityEngine;

public class GunHolder : MonoBehaviour
{
    [SerializeField] List<GameObject> gunList;
    [SerializeField] int gunIndex = 0;
    GameObject gun;
    List<Gun> gunScriptList;
    Gun gunScript;

    void OnEnable()
    {
        Initialize();
    }

    void Initialize()
    {
        gunScriptList = new List<Gun>();

        foreach (GameObject gun in gunList)
        {
            Gun script = gun.GetComponent<Gun>();
            if (script != null)
            {
                gunScriptList.Add(script);
            }
        }

        gunScript = gunScriptList[gunIndex];
        gun = gunList[gunIndex];
        gun.SetActive(true);
    }

    public void HoldTrigger()
    {
        gunScript.HoldTrigger(transform.rotation);
    }

    public void ReleaseTrigger()
    {
        gunScript.ReleaseTrigger(transform.rotation);
    }

    public void Next()
    {
        gunScript.Reset();
        gunIndex = (gunIndex + 1) % gunScriptList.Count;
        gunScript = gunScriptList[gunIndex];
        ChangeGun();
        gunScript.Reset();
    }

    public void Back()
    {
        gunScript.Reset();
        gunIndex = gunIndex + 1 >= gunScriptList.Count ? 0 : gunIndex + 1;
        gunScript = gunScriptList[gunIndex];
        ChangeGun();
        gunScript.Reset();
    }

    void ChangeGun()
    {
        
        gun.SetActive(false);
        gun = gunList[gunIndex];
        gun.SetActive(true);
    }

    public void Reset()
    {
        gunScript.Reset();
    }
}