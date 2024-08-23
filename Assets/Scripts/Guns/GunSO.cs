using UnityEngine;

[CreateAssetMenu(menuName = "Gun script", fileName = "New gun script")]
public class GunSO : ScriptableObject
{
    [SerializeField] string gunName;
    [SerializeField] float attack;
    [SerializeField] float speedMultiplier = 1f;

    public string GetGunName() => gunName;
    public float GetAttack() => attack;
    public float GetSpeedMultiplier() => speedMultiplier;
}