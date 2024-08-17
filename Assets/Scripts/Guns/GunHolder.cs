using UnityEngine;

public class GunHolder : MonoBehaviour
{
    [SerializeField] GameObject gun;
    [SerializeField] AudioClip shotSFX;
    Gun gunScript;

    private void Start() {
        gunScript = gun.GetComponent<Gun>();
    }

    public void Shoot() {
        if (gunScript == null) return;

        gunScript.Shoot(transform.rotation);
        AudioManager.Instance.PlaySound(shotSFX);
    }

    public void ChargeShot(float _chargePercent) {
        if (gunScript == null) return;
        
        gunScript.ChargeShot(transform.rotation, _chargePercent);
        AudioManager.Instance.PlaySound(shotSFX);
    }

    public void ChangeGun(GameObject _gun) => gun = _gun;
}