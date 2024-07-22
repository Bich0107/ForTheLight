using UnityEngine;

public class HandGun : Gun
{
    public override void Shoot(Quaternion _rotation)
    {
        GameObject instance = SpawnBullet(_rotation);

        instance.SetActive(true);
        instance.GetComponent<Bullet>().Shoot(GetDirection(), gunScript.GetSpeedMultiplier(), gunScript.GetAttack());
    }

    public override void ChargeShot(Quaternion _rotation, float _chargePercent)
    {
        GameObject instance = SpawnBullet(_rotation);

        instance.SetActive(true);
        instance.GetComponent<Bullet>().Shoot(GetDirection(), gunScript.GetSpeedMultiplier(), gunScript.GetAttack(), _chargePercent);
    }

    GameObject SpawnBullet(Quaternion _rotation)
    {
        GameObject instance = ObjectPool.Instance.Spawn(bullet.tag);
        instance.transform.position = bulletSpawnPos.transform.position;
        instance.transform.rotation = _rotation;
        return instance;
    }

    public override void Reload()
    {

    }
}
