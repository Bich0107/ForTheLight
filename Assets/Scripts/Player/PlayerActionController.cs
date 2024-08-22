using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActionController : MonoBehaviour
{
    [SerializeField] GunHolder gunHolder;

    void OnFire(InputValue _value)
    {
        if (!GameManager.Instance.PlayerControlStatus()) return;

        if (_value.isPressed)
        {
            gunHolder.HoldTrigger();
        }
        else
        {
            gunHolder.ReleaseTrigger();
        }
    }

    void OnTalk(InputValue _value)
    {
        if (_value.isPressed)
        {
            DialogueSystem.Instance.NextDialogue();
        }
    }

    public void Reset() {
        gunHolder.Reset();
    }
}
