using UnityEngine;

public class Gun : MonoBehaviour
{
    protected Camera cam;
    [SerializeField] protected GunSO gunScript;
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected GameObject bulletSpawnPos;
    [SerializeField] protected int currentAmmo;
    [SerializeField] protected AudioClip shotSFX;

    private void Awake()
    {
        cam = Camera.main;
    }

    public virtual void HoldTrigger(Quaternion _rotation) { }
    public virtual void ReleaseTrigger(Quaternion _rotation) { }

    public void Shoot(Quaternion _rotation)
    {
        GameObject bullet = SpawnBullet(_rotation);

        bullet.SetActive(true);
        bullet.GetComponent<Bullet>().Shoot(GetDirection(), gunScript.GetSpeedMultiplier(), gunScript.GetAttack());
        AudioManager.Instance.PlaySound(shotSFX);
    }

    protected Vector2 GetDirection()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        return (mousePos - (Vector2)transform.position).normalized;
    }

    public void ChangeBullet(GameObject _bullet) => bullet = _bullet;

    protected GameObject SpawnBullet(Quaternion _rotation)
    {
        GameObject instance = ObjectPool.Instance.Spawn(bullet.tag);
        instance.transform.position = bulletSpawnPos.transform.position;
        instance.transform.rotation = _rotation;
        return instance;
    }

    public virtual void Reset() { }
}
