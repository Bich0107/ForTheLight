using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK47Gun : Gun
{
    [Header("AK47 settings")]
    [SerializeField] float fireCD;
    float timer;
    bool isShooting = false;

    void Update()
    {

    }

    public override void HoldTrigger(Quaternion _rotation)
    {
    }

    public override void ReleaseTrigger(Quaternion _rotation)
    {
    }

    public override void Reset()
    {
    }
}
