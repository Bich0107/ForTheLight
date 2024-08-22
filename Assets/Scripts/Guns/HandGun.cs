using UnityEngine;

public class HandGun : Gun
{
    [Header("Handgun settings")]
    [SerializeField] float minChargeTime;
    [SerializeField] float maxChargeTime;
    [SerializeField] float minChargePercent;
    [SerializeField] GameObject chargeVFX;
    float chargeTime = 0f;
    bool isCharging = false;

    void Update()
    {
        if (isCharging)
        {
            chargeTime += Time.deltaTime;
            if (chargeTime > minChargeTime && !chargeVFX.activeSelf) {
                chargeVFX.SetActive(true);
            }
        }
    }

    public override void HoldTrigger(Quaternion _rotation)
    {
        Shoot(_rotation);
        isCharging = true;
    }

    public override void ReleaseTrigger(Quaternion _rotation)
    {
        if (chargeTime > minChargeTime)
        {
            chargeTime = Mathf.Min(chargeTime, maxChargeTime);
            float chargePercent = chargeTime / maxChargeTime * 100f;
            chargePercent = chargePercent > minChargePercent ? chargePercent : minChargePercent;
            ChargeShot(transform.rotation, chargePercent);
        }

        ResetChargeStatus();
    }

    public void ChargeShot(Quaternion _rotation, float _chargePercent)
    {
        GameObject instance = SpawnBullet(_rotation);

        instance.SetActive(true);
        instance.GetComponent<Bullet>().Shoot(GetDirection(), gunScript.GetSpeedMultiplier(), gunScript.GetAttack(), _chargePercent);
        AudioManager.Instance.PlaySound(shotSFX);
    }

    void OnDisable()
    {
        Reset();
    }

    public override void Reset() {
        ResetChargeStatus();
    }

    public void ResetChargeStatus()
    {
        isCharging = false;
        chargeTime = 0;
        chargeVFX.SetActive(false);
    }
}
