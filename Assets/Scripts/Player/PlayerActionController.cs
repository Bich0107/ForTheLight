using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActionController : MonoBehaviour
{
    [SerializeField] GunHolder gunHolder;
    [SerializeField] float minChargeTime;
    [SerializeField] float maxChargeTime;
    [SerializeField] float minChargePercent;
    bool isCharging = false;
    float chargeTime;

    void Update()
    {
        if (isCharging)
        {
            chargeTime += Time.deltaTime;
        }
    }

    void OnFire(InputValue _value)
    {
        if (_value.isPressed)
        {
            isCharging = true;
            gunHolder.Shoot();
        }
        else
        {
            isCharging = false;
            if (chargeTime > minChargeTime)
            {
                chargeTime = Mathf.Min(chargeTime, maxChargeTime);
                float chargePercent = chargeTime / maxChargeTime;
                chargePercent = chargePercent > minChargePercent ? chargePercent : minChargePercent;
                gunHolder.ChargeShot(chargePercent);
            }
            chargeTime = 0;
        }
    }
}
