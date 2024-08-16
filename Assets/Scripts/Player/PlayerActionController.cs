using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActionController : MonoBehaviour
{
    [SerializeField] GunHolder gunHolder;
    [SerializeField] float minChargeTime;
    [SerializeField] float maxChargeTime;
    [SerializeField] float minChargePercent;
    [SerializeField] GameObject chargeVFX;
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
        if (!GameManager.Instance.PlayerControlStatus())
        {
            ResetChargeStatus();
            return;
        }

        if (_value.isPressed)
        {
            isCharging = true;
            chargeVFX.SetActive(true);
            gunHolder.Shoot();
        }
        else
        {
            // calculate charge time to increase gun power
            if (chargeTime > minChargeTime)
            {
                chargeTime = Mathf.Min(chargeTime, maxChargeTime);
                float chargePercent = chargeTime / maxChargeTime;
                chargePercent = chargePercent > minChargePercent ? chargePercent : minChargePercent;
                gunHolder.ChargeShot(chargePercent);
            }
            ResetChargeStatus();
        }
    }

    void OnTalk(InputValue _value)
    {
        if (_value.isPressed)
        {
            DialogueSystem.Instance.NextDialogue();
        }
    }

    void OnDisable()
    {
        ResetChargeStatus();
    }

    public void ResetChargeStatus()
    {
        isCharging = false;
        chargeTime = 0;
        chargeVFX.SetActive(false);
    }
}
