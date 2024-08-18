using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] protected Camera cam;
    [SerializeField] protected GunSO gunScript;
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected GameObject bulletSpawnPos;
    [SerializeField] protected int currentAmmo;

    private void Awake()
    {
        cam = Camera.main;
    }

    public virtual void Shoot(Quaternion _rotation) { }
    public virtual void ChargeShot(Quaternion _rotation, float _chargePercent) { }

    public virtual void Reload() { }

    protected Vector2 GetDirection()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        return (mousePos - (Vector2)transform.position).normalized;
    }

    public void ChangeBullet(GameObject _bullet) => bullet = _bullet;
}
