using UnityEngine;

[CreateAssetMenu(menuName = "Gun script", fileName = "New gun script")]
public class GunSO : ScriptableObject
{
    [SerializeField] GameObject bullet;
    [SerializeField] string gunName;
    [SerializeField] float attack;
    [SerializeField] float speedMultiplier = 1f;
    [SerializeField] float fireCD;
    [SerializeField] int maxAmmo;

    public string GetGunName() => gunName;
    public int GetMaxAmmo() => maxAmmo;
    public float GetAttack() => attack;
    public float GetSpeedMultiplier() => speedMultiplier;
    public float GetFireCD() => fireCD;
}