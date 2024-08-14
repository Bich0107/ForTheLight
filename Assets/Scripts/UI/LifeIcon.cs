using UnityEngine;

public class LifeIcon : MonoBehaviour
{
    private class LifeIconAnimationParams
    {
        public static string TurnOn = "turnOn";
        public static string TurnOff = "turnOff";
    }

    [SerializeField] Animator animator;
    bool isOn;

    public void TurnOn()
    {
        if (isOn) return;
        isOn = true;
        animator.SetTrigger(LifeIconAnimationParams.TurnOn);
    }

    public void TurnOff()
    {
        if (!isOn) return;
        isOn = false;
        animator.SetTrigger(LifeIconAnimationParams.TurnOff);
    }
}
