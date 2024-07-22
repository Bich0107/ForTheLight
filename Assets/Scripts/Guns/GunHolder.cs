using UnityEngine;

public class GunHolder : MonoBehaviour
{
    [SerializeField] GameObject gun;
    Gun gunScript;

    private void Start() {
        gunScript = gun.GetComponent<Gun>();
    }

    public void Shoot() {
        gunScript.Shoot(transform.rotation);
    }

    public void ChargeShot(float _chargePercent) {
        gunScript.ChargeShot(transform.rotation, _chargePercent);
    }

    public void ChangeGun(GameObject _gun) => gun = _gun;
}